using ESRI.ArcGIS;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MapControlAppDemo
{
    public sealed partial class MainForm : Form
    {
        #region class private members
        private IMapControl3 m_mapControl = null;
        private string m_mapDocumentName = string.Empty;
        private ILayer m_selectedlayer = null;
        private short op = -1;
        private IPolyline drawnLine = null;
        #endregion

        #region class constructor
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            //get the MapControl
            m_mapControl = (IMapControl3)axMapControl1.Object;

            //disable the Save menu (since there is no document yet)
            menuSaveDoc.Enabled = false;
        }

        #region Main Menu event handlers
        private void menuNewDoc_Click(object sender, EventArgs e)
        {
            //execute New Document command
            ICommand command = new CreateNewDocument();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuOpenDoc_Click(object sender, EventArgs e)
        {
            //execute Open Document command
            ICommand command = new ControlsOpenDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuSaveDoc_Click(object sender, EventArgs e)
        {
            //execute Save Document command
            if (m_mapControl.CheckMxFile(m_mapDocumentName))
            {
                //create a new instance of a MapDocument
                IMapDocument mapDoc = new MapDocumentClass();
                mapDoc.Open(m_mapDocumentName, string.Empty);

                //Make sure that the MapDocument is not readonly
                if (mapDoc.get_IsReadOnly(m_mapDocumentName))
                {
                    MessageBox.Show("Map document is read only!");
                    mapDoc.Close();
                    return;
                }

                //Replace its contents with the current map
                mapDoc.ReplaceContents((IMxdContents)m_mapControl.Map);

                //save the MapDocument in order to persist it
                mapDoc.Save(mapDoc.UsesRelativePaths, false);

                //close the MapDocument
                mapDoc.Close();
            }
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            //execute SaveAs Document command
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuExitApp_Click(object sender, EventArgs e)
        {
            //exit the application
            Application.Exit();
        }
        #endregion

        //listen to MapReplaced evant in order to update the statusbar and the Save menu
        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            //get the current document name from the MapControl
            m_mapDocumentName = m_mapControl.DocumentFilename;

            //if there is no MapDocument, diable the Save menu and clear the statusbar
            if (m_mapDocumentName == string.Empty)
            {
                menuSaveDoc.Enabled = false;
                statusBarXY.Text = string.Empty;
            }
            else
            {
                //enable the Save manu and write the doc name to the statusbar
                menuSaveDoc.Enabled = true;
                statusBarXY.Text = System.IO.Path.GetFileName(m_mapDocumentName);
            }
        }

        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            statusBarXY.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));
        }

        private void axLicenseControl1_Enter(object sender, EventArgs e)
        {

        }

        private void addLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IWorkspace ws = null;
            IWorkspaceFactory wsf = new ShapefileWorkspaceFactory();
            ws = wsf.OpenFromFile(@"d:\csu", 0);
            IFeatureWorkspace fws = (IFeatureWorkspace)ws;
            IFeatureClass fc = fws.OpenFeatureClass("jmd.shp");
            IFeatureLayer layer = new FeatureLayer();
            layer.FeatureClass = fc;
            layer.Name = fc.AliasName;
            this.axMapControl1.AddLayer(layer);
        }

        private void axTOCControl1_OnDoubleClick(object sender, ITOCControlEvents_OnDoubleClickEvent e)
        {
            esriTOCControlItem type = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap basicMap = null;
            ILayer layer = null;
            object unk = null, data = null;
            axTOCControl1.HitTest(e.x, e.y, ref type, ref basicMap, ref layer, ref unk, ref data);
            if (type == esriTOCControlItem.esriTOCControlItemLayer
               && layer != null
               && e.button == 1)
            {
                MessageBox.Show(layer.Name);
            }
        }

        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int layerCount = axMapControl1.LayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                if (axMapControl1.get_Layer(i).Name == m_selectedlayer.Name
                     && i - 1 >= 0)
                {
                    axMapControl1.MoveLayerTo(i, i - 1);
                    break;
                }
            }
        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int layerCount = axMapControl1.LayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                if (axMapControl1.get_Layer(i).Name == m_selectedlayer.Name
                     && i + 1 < layerCount)
                {
                    axMapControl1.MoveLayerTo(i, i + 1);
                    break;
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = -1;
            for (int i = 0; i < this.axMapControl1.LayerCount; i++)
                if (axMapControl1.get_Layer(i).Name == m_selectedlayer.Name)
                {
                    index = i;
                    break;
                }
            if (index >= 0)
                this.axMapControl1.DeleteLayer(index);
        }

        private void axTOCControl1_OnMouseUp(object sender, ITOCControlEvents_OnMouseUpEvent e)
        {
            esriTOCControlItem type = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap basicMap = null;
            object unk = null, data = null;
            axTOCControl1.GetSelectedItem(ref type, ref basicMap, ref m_selectedlayer, ref unk, ref data);
            if (type == esriTOCControlItem.esriTOCControlItemLayer
                 && m_selectedlayer != null && e.button == 2)
                contextMenuStrip1.Show(axTOCControl1, e.x, e.y);

        }

        private void enableSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int layerCount = axMapControl1.LayerCount;

            for (int i = 0; i < layerCount; i++)
            {
                IFeatureLayer layer = axMapControl1.get_Layer(i) as IFeatureLayer;
                layer.Selectable = false;
                if (axMapControl1.get_Layer(i).Name == m_selectedlayer.Name)
                {
                    layer.Selectable = true;
                }
            }
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (op == 1)
            {
                IRubberBand rubber = new RubberLine();
                IPolyline line = rubber.TrackNew(this.axMapControl1.ActiveView.ScreenDisplay, null) as IPolyline;
                ISimpleLineSymbol pLineSym = new SimpleLineSymbol();
                IRgbColor pColor = new RgbColor();
                pColor.Red = 11;
                pColor.Green = 120;
                pColor.Blue = 233;
                pLineSym.Color = pColor;
                pLineSym.Style = esriSimpleLineStyle.esriSLSSolid;
                pLineSym.Width = 2;
                object symbol = pLineSym as object;
                this.axMapControl1.DrawShape(line, ref symbol);
                this.drawnLine = line;
            }
        }

        private void drawLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.op != 1)
                this.op = 1;
            else
                this.op = -1;
        }

        private void bufferSelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.drawnLine == null)
                return;
            ITopologicalOperator topo = (ITopologicalOperator)this.drawnLine;
            IPolygon buffer = topo.Buffer(10) as IPolygon;

            this.axMapControl1.Map.SelectByShape(buffer, null, false);
            this.axMapControl1.ActiveView.Refresh();
            if (this.axMapControl1.Map.SelectionCount == 0)
                return;
            //IEnumFeature features = this.axMapControl1.Map.FeatureSelection as IEnumFeature;
            //features.Reset();
            //IFeature feature = features.Next();
            if (this.m_selectedlayer == null)
                return;
            IFeatureSelection fs = m_selectedlayer as IFeatureSelection;
            ICursor cur = null;
            fs.SelectionSet.Refresh();
            fs.SelectionSet.Search(null, true, out cur);
            IFeature feature = cur.NextRow() as IFeature;
            DataTable dt = new DataTable();
            IFields fields = feature.Fields;
            for (int i = 0; i < fields.FieldCount; i++)
                dt.Columns.Add(fields.Field[i].Name);
            DataRow row;
            while (feature != null)
            {
                row = dt.NewRow();
                for (int i = fields.FieldCount-1 ; i >=0 ; i--)
                {
                    if (fields.Field[i].Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        row[i] = "Shape Object";
                        IGeometry geo = feature.Value[i];
                    }                        
                    else if (feature.get_Value(i) != null)
                    {
                        row[i] = Convert.ToString(feature.Value[i]);
                    }                        
                }
                dt.Rows.Add(row);
                //feature = features.Next();
                feature = cur.NextRow() as IFeature;
            }
            Form1 frm = new Form1();
            frm.setDataTable(dt);
            frm.ShowDialog();
        }

        private void listFeatureClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IWorkspace ws = null;
            IWorkspaceFactory wsf = new ShapefileWorkspaceFactory();
            ws = wsf.OpenFromFile(@"d:\csu", 0);
            IFeatureWorkspace fws = (IFeatureWorkspace)ws;
            List<IFeatureClass> list = new List<IFeatureClass>();
            IEnumDatasetName datasetName = ws.DatasetNames[esriDatasetType.esriDTFeatureClass]; //筛选出shp
            IDatasetName dn = datasetName.Next(); //获取的数据集名称无后缀名
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("GeometryType");
            DataRow row;            
            while (dn != null)
            {
                row = dt.NewRow();
                IFeatureClass fc = fws.OpenFeatureClass(dn.Name);
                list.Add(fc);
                row[0] = dn.Name;
                row[1] = Convert.ToString(fc.ShapeType);
                dt.Rows.Add(row);                
                dn = datasetName.Next();
            }

            Form1 frm = new Form1();
            frm.setDataTable(dt);
            frm.ShowDialog();
        }
        /// <summary>
        /// 使用要素类描述对象创建最简单的要素类，只包含2个必要字段“SHAPE”和“OBJECTID”，无空间参考
        /// </summary>
        /// <param name="featureWorkspace">目标工作空间</param>
        public void CreatSimpleFeatureClass(IFeatureWorkspace featureWorkspace)
        {
            // 检查要素类是否已经存在
            IWorkspace2 ws2 = featureWorkspace as IWorkspace2;
            if (ws2.get_NameExists(esriDatasetType.esriDTFeatureClass, "Simple"))
            {
                // 如果要素类已经存在，可以选择删除它
                IFeatureClass existingFClass = featureWorkspace.OpenFeatureClass("Simple");
                IDataset ds = existingFClass as IDataset;
                ds.Delete();
            }            
            //ESRI 要素类描述对象
            IFeatureClassDescription fcDesc = new FeatureClassDescription() as IFeatureClassDescription;
            IObjectClassDescription ocDesc = (IObjectClassDescription)fcDesc;
            IFeatureClass targetFClass = featureWorkspace.CreateFeatureClass("Simple",
                  ocDesc.RequiredFields,
                  null, null, esriFeatureType.esriFTSimple, fcDesc.ShapeFieldName, "");
            IFeatureLayer layer = new FeatureLayer();
            layer.FeatureClass = targetFClass;
            layer.Name = targetFClass.AliasName;
            this.axMapControl1.AddLayer(layer);
        }
        public void CreatCustomerizeFeatureClass(IFeatureWorkspace featureWorkspace)
        {
            IWorkspace2 ws2 = featureWorkspace as IWorkspace2;
            // 检查要素类是否已经存在
            if (ws2.get_NameExists(esriDatasetType.esriDTFeatureClass, "points"))
            {
                // 如果要素类已经存在，可以选择删除它
                IFeatureClass existingFClass = featureWorkspace.OpenFeatureClass("points");
                IDataset ds = existingFClass as IDataset;
                ds.Delete();
            }
            //ESRI 要素类描述对象
            IFeatureClassDescription fcDesc = new FeatureClassDescription() as IFeatureClassDescription;
            IObjectClassDescription ocDesc = (IObjectClassDescription)fcDesc;
            IFields fields = ocDesc.RequiredFields;
            int shapeFieldIndex = fields.FindField(fcDesc.ShapeFieldName);
            IField field = fields.get_Field(shapeFieldIndex);
            IGeometryDef geometryDef = field.GeometryDef;
            IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
            geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            //添加自定义字段
            IFieldsEdit fields2 = fields as IFieldsEdit;
            IFieldEdit field2 = new Field() as IFieldEdit;
            field2.Name_2 = "Name";
            field2.Type_2 = esriFieldType.esriFieldTypeString;
            field2.Length_2 = 20;
            fields2.AddField(field2);
            //生成有效的字段集
            IFieldChecker fieldChecker = new FieldChecker() as IFieldChecker;
            IEnumFieldError enumFieldError = null;
            IFields validatedFields = null;
            fieldChecker.ValidateWorkspace = (IWorkspace)featureWorkspace;
            fieldChecker.Validate(fields, out enumFieldError, out validatedFields);
            //创建要素类
            IFeatureClass targetFClass = featureWorkspace.CreateFeatureClass("points",
                  validatedFields,
                  null, null, esriFeatureType.esriFTSimple, fcDesc.ShapeFieldName, "");
            //IFeatureClass targetFClass = featureWorkspace.CreateFeatureClass("Simple1",
            //      ocDesc.RequiredFields,
            //      null, null, esriFeatureType.esriFTSimple, fcDesc.ShapeFieldName, "");
            IFeatureLayer layer = new FeatureLayer();
            layer.FeatureClass = targetFClass;
            layer.Name = targetFClass.AliasName;
            this.axMapControl1.AddLayer(layer);
        }

        private void simpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IWorkspace ws = null;
            IWorkspaceFactory wsf = new ShapefileWorkspaceFactory();
            ws = wsf.OpenFromFile(@"d:\csu", 0);
            IFeatureWorkspace fws = (IFeatureWorkspace)ws;
            CreatSimpleFeatureClass(fws);
        }

        private void customerizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IWorkspace ws = null;
            IWorkspaceFactory wsf = new ShapefileWorkspaceFactory();
            ws = wsf.OpenFromFile(@"d:\csu", 0);
            IFeatureWorkspace fws = (IFeatureWorkspace)ws;
            CreatCustomerizeFeatureClass(fws);
        }

        private void addFieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IWorkspace ws = null;
            IWorkspaceFactory wsf = new ShapefileWorkspaceFactory();
            ws = wsf.OpenFromFile(@"d:\csu", 0);
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)ws;
            IWorkspace2 ws2 = featureWorkspace as IWorkspace2;
            // 检查要素类是否已经存在
            if (ws2.get_NameExists(esriDatasetType.esriDTFeatureClass, "points"))
            {
                // 如果要素类已经存在
                IFeatureClass existingFClass = featureWorkspace.OpenFeatureClass("points");
                //添加字段
                IFieldEdit field = new Field() as IFieldEdit;
                field.Name_2 = "Num";
                field.Type_2 = esriFieldType.esriFieldTypeInteger;
                existingFClass.AddField(field);
                DataTable dt = new DataTable();
                dt.Columns.Add("Name");
                dt.Columns.Add("Type");
                IFields fields = existingFClass.Fields;
                DataRow row;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    row = dt.NewRow();
                    row[0] = fields.Field[i].Name;
                    row[1] = Convert.ToString(fields.Field[i].Type);
                    dt.Rows.Add(row);
                }
                Form1 frm = new Form1();
                frm.setDataTable(dt);
                frm.ShowDialog();
            }
        }

        private void addAFeatureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //生成单个要素
            IWorkspaceFactory wf = new ShapefileWorkspaceFactory();
            IFeatureWorkspace fws = wf.OpenFromFile(@"d:\csu", 0) as IFeatureWorkspace;
            IFeatureClass featureClass = fws.OpenFeatureClass("points");
            IFeature feature = featureClass.CreateFeature();
            //生成点
            IPoint point = new ESRI.ArcGIS.Geometry.Point();
            point.X = 800; point.Y = 800;
            feature.Shape = point;
            feature.Value[featureClass.FindField("Name")] =
                                   string.Format("Point_{0}", featureClass.FeatureCount(null));
            feature.Store();
        }

        private void addFeaturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //批量添加要素
            IWorkspaceFactory wf = new ShapefileWorkspaceFactory();
            IFeatureWorkspace fws = wf.OpenFromFile(@"d:\csu", 0) as IFeatureWorkspace;
            IFeatureClass featureClass = fws.OpenFeatureClass("points");
            // Create the feature buffer.
            IFeatureBuffer featureBuffer = featureClass.CreateFeatureBuffer();
            // Create insert feature cursor using buffering.
            IFeatureCursor featureCursor = featureClass.Insert(true);
            int index = featureClass.FindField("Name");
            Random rand = new Random();
            for (int i = 0; i < 500; i++)
            {
                IPoint point = new ESRI.ArcGIS.Geometry.Point();
                point.PutCoords(rand.NextDouble() * 100, rand.NextDouble() * 100);
                featureBuffer.Shape = point;
                featureBuffer.Value[index] = "Point_" + rand.Next(1000, 9999).ToString();
                featureCursor.InsertFeature(featureBuffer);
            }
            // Attempt to flush the buffer
            featureCursor.Flush();
            axMapControl1.Refresh();
        }

        private void deleteFeature1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //删除要素
            IWorkspaceFactory wf = new ShapefileWorkspaceFactory();
            IFeatureWorkspace fws = wf.OpenFromFile(@"d:\csu", 0) as IFeatureWorkspace;
            IFeatureClass featureClass = fws.OpenFeatureClass("points");
            IQueryFilter filter = new QueryFilter();
            filter.WhereClause = "FID>300";
            ITable table = (ITable)featureClass;
            table.DeleteSearchedRows(filter);  //如果filter=null，则删除全部要素
            axMapControl1.Refresh();
        }

        private void deleteFeature2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //删除要素方法2
            IWorkspaceFactory wf = new ShapefileWorkspaceFactory();
            IFeatureWorkspace fws = wf.OpenFromFile(@"d:\csu", 0) as IFeatureWorkspace;
            IFeatureClass featureClass = fws.OpenFeatureClass("points");
            IQueryFilter queryFilter = new QueryFilter();
            queryFilter.WhereClause = "FID>100";
            IFeatureCursor updateCursor = featureClass.Update(queryFilter, false);
            IFeature feature = updateCursor.NextFeature();
            while (feature != null)
            {
                updateCursor.DeleteFeature();
                feature = updateCursor.NextFeature();
            }
            axMapControl1.Refresh();
        }

        private void updateFeaturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //批量更新要素
            IWorkspaceFactory wf = new ShapefileWorkspaceFactory();
            IFeatureWorkspace fws = wf.OpenFromFile(@"d:\csu", 0) as IFeatureWorkspace;
            IFeatureClass featureClass = fws.OpenFeatureClass("points");
            IQueryFilter filter = new QueryFilter();
            filter.WhereClause = "FID>100";
            //利用FeatureCursor进行数据更新
            IFeatureCursor updateCursor = featureClass.Update(filter, false);
            IFeature feature = updateCursor.NextFeature();

            while (feature != null)
            {
                feature.Value[2] = "X_" + feature.Value[0].ToString();
                updateCursor.UpdateFeature(feature);
                feature = updateCursor.NextFeature();
            }
        }

        private void intersectCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //检查建筑物是否存在相交？
            IWorkspaceFactory wf = new ShapefileWorkspaceFactory();
            IFeatureWorkspace fws = wf.OpenFromFile(@"d:\csu", 0) as IFeatureWorkspace;
            IFeatureClass fc = fws.OpenFeatureClass("jmd.shp");
            if (fc == null) return;
            IFeature f1, f2;
            IFeatureCursor cur1 = fc.Search(null, true);
            while ((f1 = cur1.NextFeature()) != null)
            {
                ISpatialFilter filter = new SpatialFilter();
                filter.WhereClause = "FID<>" + f1.OID.ToString();
                filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                filter.Geometry = f1.Shape as IGeometry;
                ISelectionSet set = fc.Select(filter, esriSelectionType.esriSelectionTypeHybrid,
                                                     esriSelectionOption.esriSelectionOptionNormal, null);
                if (set.Count > 0) { MessageBox.Show(string.Format("{0} 有相交!", f1.OID)); }
            }
            MessageBox.Show("检查完成!");
        }

        private void drawAFeatureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand cmd = new ToolDrawFeature();
            cmd.OnCreate(axMapControl1.Object);
            cmd.OnClick();
            axMapControl1.CurrentTool = cmd as ITool;            
        }
    }
}