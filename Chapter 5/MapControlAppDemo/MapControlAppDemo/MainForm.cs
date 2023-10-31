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
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.DataSourcesRaster;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using stdole;



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
                ConstructPointAlongLine(line);
            }
        }

        private void drawLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.op != 1)
                this.op = 1;
            else
                this.op = -1;
        }

        public void ConstructPointAlongLine(IPolyline pl)
        {
            ICurve polyLine = pl;
            
            for(int i = 0; i< pl.Length/100 - 1; i++)
            {
                IPoint point1 = ConstructPointAlong(100*(i+1), polyLine, esriSegmentExtension.esriNoExtension, false);
                //System.Windows.Forms.MessageBox.Show("x,y = " + point1.X + "," + point1.Y);
                //this.axMapControl1.DrawShape(point1);
            }

            for(int i = 0 ; i< 10; i++)
            {
                IPoint point2 = ConstructPointAlong(0.1*(i+1), polyLine, esriSegmentExtension.esriNoExtension, true);
                //System.Windows.Forms.MessageBox.Show("x,y = " + point2.X + "," + point2.Y);
                this.axMapControl1.DrawShape(point2);
            }

        }

        public IPoint ConstructPointAlong(double distance, ICurve curve,
                                   esriSegmentExtension extension, bool asRatio)
        {
            IConstructPoint contructionPoint = new ESRI.ArcGIS.Geometry.Point() as IConstructPoint;
            contructionPoint.ConstructAlong(curve, extension, distance, asRatio);
            return contructionPoint as IPoint;
        }

        private void ConstructDivideEqual()
        {
            IPoint centerPoint = new ESRI.ArcGIS.Geometry.Point();
            centerPoint.PutCoords(1000, 1000);
            this.axMapControl1.DrawShape(centerPoint);
            IPoint fromPoint = new ESRI.ArcGIS.Geometry.Point();
            fromPoint.PutCoords(10, 10);
            this.axMapControl1.DrawShape(centerPoint);
            IPoint toPoint = new ESRI.ArcGIS.Geometry.Point();
            toPoint.PutCoords(10, 2000);
            this.axMapControl1.DrawShape(toPoint);
            ICircularArc circularArcConstruction = new CircularArc();
            circularArcConstruction.PutCoords(centerPoint, fromPoint, toPoint, esriArcOrientation.esriArcClockwise);
            ISegmentCollection sc = new ESRI.ArcGIS.Geometry.Path() as ISegmentCollection;
            sc.AddSegment(circularArcConstruction as ISegment);
            IGeometryCollection gc = new Polyline() as IGeometryCollection;
            gc.AddGeometry(sc as IGeometry);
            this.axMapControl1.DrawShape(gc as IGeometry);
            IConstructMultipoint constructMultipoint = new Multipoint() as IConstructMultipoint;
            constructMultipoint.ConstructDivideEqual(circularArcConstruction as ICurve, 10);
            IPointCollection pointCollection = constructMultipoint as IPointCollection;
            this.axMapControl1.DrawShape(constructMultipoint as IGeometry);
            System.Windows.Forms.MessageBox.Show("Number of points is: " + pointCollection.PointCount);
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
                        IGeometry geo = feature.Value[i] as IGeometry;
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
            IEnumDatasetName datasetName = ws.DatasetNames[esriDatasetType.esriDTFeatureClass]; //ɸѡ��shp
            IDatasetName dn = datasetName.Next(); //��ȡ�����ݼ������޺�׺��
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
        /// ʹ��Ҫ�����������󴴽���򵥵�Ҫ���ֻ࣬����2����Ҫ�ֶΡ�SHAPE���͡�OBJECTID�����޿ռ�ο�
        /// </summary>
        /// <param name="featureWorkspace">Ŀ�깤���ռ�</param>
        public void CreatSimpleFeatureClass(IFeatureWorkspace featureWorkspace)
        {
            // ���Ҫ�����Ƿ��Ѿ�����
            IWorkspace2 ws2 = featureWorkspace as IWorkspace2;
            if (ws2.get_NameExists(esriDatasetType.esriDTFeatureClass, "Simple"))
            {
                // ���Ҫ�����Ѿ����ڣ�����ѡ��ɾ����
                IFeatureClass existingFClass = featureWorkspace.OpenFeatureClass("Simple");
                IDataset ds = existingFClass as IDataset;
                ds.Delete();
            }            
            //ESRI Ҫ������������
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
            // ���Ҫ�����Ƿ��Ѿ�����
            if (ws2.get_NameExists(esriDatasetType.esriDTFeatureClass, "points"))
            {
                // ���Ҫ�����Ѿ����ڣ�����ѡ��ɾ����
                IFeatureClass existingFClass = featureWorkspace.OpenFeatureClass("points");
                IDataset ds = existingFClass as IDataset;
                ds.Delete();
            }
            //ESRI Ҫ������������
            IFeatureClassDescription fcDesc = new FeatureClassDescription() as IFeatureClassDescription;
            IObjectClassDescription ocDesc = (IObjectClassDescription)fcDesc;
            IFields fields = ocDesc.RequiredFields;
            int shapeFieldIndex = fields.FindField(fcDesc.ShapeFieldName);
            IField field = fields.get_Field(shapeFieldIndex);
            IGeometryDef geometryDef = field.GeometryDef;
            IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
            geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            //����Զ����ֶ�
            IFieldsEdit fields2 = fields as IFieldsEdit;
            IFieldEdit field2 = new Field() as IFieldEdit;
            field2.Name_2 = "Name";
            field2.Type_2 = esriFieldType.esriFieldTypeString;
            field2.Length_2 = 20;
            fields2.AddField(field2);
            //������Ч���ֶμ�
            IFieldChecker fieldChecker = new FieldChecker() as IFieldChecker;
            IEnumFieldError enumFieldError = null;
            IFields validatedFields = null;
            fieldChecker.ValidateWorkspace = (IWorkspace)featureWorkspace;
            fieldChecker.Validate(fields, out enumFieldError, out validatedFields);
            //����Ҫ����
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
            // ���Ҫ�����Ƿ��Ѿ�����
            if (ws2.get_NameExists(esriDatasetType.esriDTFeatureClass, "points"))
            {
                // ���Ҫ�����Ѿ�����
                IFeatureClass existingFClass = featureWorkspace.OpenFeatureClass("points");
                //����ֶ�
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
            //���ɵ���Ҫ��
            IWorkspaceFactory wf = new ShapefileWorkspaceFactory();
            IFeatureWorkspace fws = wf.OpenFromFile(@"d:\csu", 0) as IFeatureWorkspace;
            IFeatureClass featureClass = fws.OpenFeatureClass("points");
            IFeature feature = featureClass.CreateFeature();
            //���ɵ�
            IPoint point = new ESRI.ArcGIS.Geometry.Point();
            point.X = 800; point.Y = 800;
            feature.Shape = point;
            feature.Value[featureClass.FindField("Name")] =
                                   string.Format("Point_{0}", featureClass.FeatureCount(null));
            feature.Store();
        }

        private void addFeaturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //�������Ҫ��
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
            //ɾ��Ҫ��
            IWorkspaceFactory wf = new ShapefileWorkspaceFactory();
            IFeatureWorkspace fws = wf.OpenFromFile(@"d:\csu", 0) as IFeatureWorkspace;
            IFeatureClass featureClass = fws.OpenFeatureClass("points");
            IQueryFilter filter = new QueryFilter();
            filter.WhereClause = "FID>300";
            ITable table = (ITable)featureClass;
            table.DeleteSearchedRows(filter);  //���filter=null����ɾ��ȫ��Ҫ��
            axMapControl1.Refresh();
        }

        private void deleteFeature2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ɾ��Ҫ�ط���2
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
            //��������Ҫ��
            IWorkspaceFactory wf = new ShapefileWorkspaceFactory();
            IFeatureWorkspace fws = wf.OpenFromFile(@"d:\csu", 0) as IFeatureWorkspace;
            IFeatureClass featureClass = fws.OpenFeatureClass("points");
            IQueryFilter filter = new QueryFilter();
            filter.WhereClause = "FID>100";
            //����FeatureCursor�������ݸ���
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
            //��齨�����Ƿ�����ཻ��
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
                if (set.Count > 0) { MessageBox.Show(string.Format("{0} ���ཻ!", f1.OID)); }
            }
            MessageBox.Show("������!");
        }

        private void drawAFeatureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand cmd = new ToolDrawFeature();
            cmd.OnCreate(axMapControl1.Object);
            cmd.OnClick();
            axMapControl1.CurrentTool = cmd as ITool;            
        }

        private void constructMultipointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConstructDivideEqual();
        }

        private void addLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IPolyline polyline = new PolylineClass();
            IPointCollection pointColl = polyline as IPointCollection;
            IPoint point = new PointClass();
            point.PutCoords(100, 200);
            pointColl.AddPoint(point);
            point = new PointClass();
            point.PutCoords(300, 100);
            pointColl.AddPoint(point);
            this.axMapControl1.DrawShape(polyline);
        }

        private void drawPolylineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand command = new ToolPolyline();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
            this.axMapControl1.CurrentTool = command as ITool;
        }

        private void drawPolygonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand command = new ToolPolygon();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
            this.axMapControl1.CurrentTool = command as ITool;
        }

        private void linkPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand command = new CmdLinkPoints();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void statChartToolStripMenuItem_Click(object sender, EventArgs e)
        {             
            IFeatureLayer  layer  = axMapControl1.Map.get_Layer(0) as IFeatureLayer;
            IFeatureClass featureClass = layer.FeatureClass;
            int catIndex = featureClass.FindField("catalog1");
            //��������ϵ��
            List<String> cats = new List<string>(); List<int> nums = new List<int>();
            string cat;
            IFeatureCursor cursor = featureClass.Search(null, true); IFeature feat;
            //��ǰ����ڷ��༯�ϵ����
            int seq;
            while ((feat = cursor.NextFeature()) != null)
            {    //��ȡ����
                cat = feat.Value[catIndex].ToString();
                //��ȡ����ڷ��༯���е����
                seq = cats.IndexOf(cat);
                //���С��0����ʾδ�ҵ�������Ҫ������༯
                if (seq < 0)
                {
                    cats.Add(cat);
                    // ���һ�����ǵ�ǰ��������
                    seq = cats.Count - 1;
                    //������ҲҪ����һ��
                    nums.Add(0);
                }
                nums[seq]++;   //ͳ������+1
            }
            FormChart frm = new FormChart();
            frm.DataBindXY(cats, nums);
            frm.ShowDialog();
        }

        private void statChart1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureLayer layer = axMapControl1.Map.get_Layer(0) as IFeatureLayer;
            IFeatureClass featureClass = layer.FeatureClass;
            int catIndex = featureClass.FindField("catalog1");
            //��������ϵ��
            List<String> cats = new List<string>(); List<int> nums = new List<int>();
            string cat;
            IFeatureCursor cursor = featureClass.Search(null, true); IFeature feat;
            while ((feat = cursor.NextFeature()) != null)
            {
                cat = feat.Value[catIndex].ToString();
                if (cats.IndexOf(cat) < 0) //���ڷ��༯�У��ͼ���                
                    cats.Add(cat);
            }
            int n = 0;  //ÿ�����ݵļ�����
            IQueryFilter qf = new QueryFilterClass();
            foreach (String str in cats)
            {
                qf.WhereClause = "catalog1 = '" + str + "'";
                cursor = featureClass.Search(qf, false);
                n = 0;
                while ((feat = cursor.NextFeature()) != null)
                    n++;
                nums.Add(n);
            }
            FormChart frm = new FormChart();
            frm.DataBindXY(cats, nums);
            frm.ShowDialog();
        }

        private void bufferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand command = new ToolBufPnt();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
            this.axMapControl1.CurrentTool = command as ITool;
        }

        private void intersectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand command = new ToolIntersect();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
            this.axMapControl1.CurrentTool = command as ITool;
        }

        private void featureIntersectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureLayer layer = axMapControl1.Map.get_Layer(0) as IFeatureLayer;
            IFeatureClass featureClass = layer.FeatureClass;
            // �ཻ�Լ�����
            IFeatureCursor cur1 = featureClass.Search(null, true), cur2;
            IFeature feat1, feat2;
            IRelationalOperator ro;
            DataTable dt = new DataTable();
            dt.Columns.Add("OID1");
            dt.Columns.Add("OID2");
            DataRow row;
            while ((feat1 = cur1.NextFeature()) != null)
            {
                cur2 = featureClass.Search(null, true);
                while ((feat2 = cur2.NextFeature()) != null)
                {
                    if (feat1.OID == feat2.OID) continue;
                    ro = feat1.Shape as IRelationalOperator;
                    if (ro.Touches(feat2.Shape)/*ro.Overlaps(feat2.Shape)*/)
                    {
                        //this.dataGridView1.Rows.Add(feat1.OID, feat2.OID);
                        row = dt.NewRow();
                        row[0] = feat1.OID;
                        row[1] = feat2.OID;
                        dt.Rows.Add(row);
                        //MessageBox.Show(string.Format("{0}��{1}���ཻ", feat1.OID, feat2.OID), "Cross");
                    }
                }
            }
            Form1 frm = new Form1();
            frm.setDataTable(dt);
            frm.setHookHelper(m_mapControl.Object);
            frm.ShowDialog();
            //MessageBox.Show("������", "Cross");
        }

        private void geoprocessorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // GP���ߵ��÷�ʽ1 ʾ����
            //��ʼ��GP 
            Geoprocessor GP = new Geoprocessor();
            //��ʼ��Buffer 
            ESRI.ArcGIS.AnalysisTools.Buffer buffer = new ESRI.ArcGIS.AnalysisTools.Buffer();
            // ���Ҫ�����Ƿ��Ѿ�����
            IWorkspace ws = null;
            IFeatureClass existingFClass = null;
            IFeatureLayer layer = null;
            IWorkspaceFactory wsf = new ShapefileWorkspaceFactory();
            ws = wsf.OpenFromFile(@"d:\csu", 0);
            IFeatureWorkspace fws = (IFeatureWorkspace)ws;
            IWorkspace2 ws2 = fws as IWorkspace2;
            if (ws2.get_NameExists(esriDatasetType.esriDTFeatureClass, "road_bf30.shp"))
            {
                // ���Ҫ�����Ѿ����ڣ�����ѡ��ɾ����
                existingFClass = fws.OpenFeatureClass("road_bf30.shp");
                IDataset ds = existingFClass as IDataset;
                ds.Delete();
            }
            if (ws2.get_NameExists(esriDatasetType.esriDTFeatureClass, "road.shp"))
            {
                //
                layer = new FeatureLayerClass();
                existingFClass = fws.OpenFeatureClass("road.shp");
                layer.FeatureClass = existingFClass;
                layer.Name = existingFClass.AliasName;
                axMapControl1.Map.AddLayer(layer);
            }
            //����Ҫ�� 
            buffer.in_features = @"D:\CSU\road.shp";
            //���Ҫ���� 
            buffer.out_feature_class = @"D:\CSU\road_bf30.shp";
            //������� 
            buffer.buffer_distance_or_field = 0.00003; //Ĭ�ϵ�λ 
                                                  //ִ�й��� 
            GP.Execute(buffer, null);

            layer = new FeatureLayerClass();
            existingFClass = fws.OpenFeatureClass("road_bf30.shp");
            layer.FeatureClass = existingFClass;
            layer.Name = existingFClass.AliasName;
            axMapControl1.Map.AddLayer(layer);
            axMapControl1.ActiveView.Refresh();
        }

        private void geoprocessingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // GP���ߵ��÷���2 ʾ����
            // ���Ҫ�����Ƿ��Ѿ�����
            IWorkspace ws = null;
            IFeatureClass existingFClass = null;
            IFeatureLayer layer = null;
            IWorkspaceFactory wsf = new ShapefileWorkspaceFactory();
            ws = wsf.OpenFromFile(@"d:\csu", 0);
            IFeatureWorkspace fws = (IFeatureWorkspace)ws;
            IWorkspace2 ws2 = fws as IWorkspace2;
            if (ws2.get_NameExists(esriDatasetType.esriDTFeatureClass, "road_bf30.shp"))
            {
                // ���Ҫ�����Ѿ����ڣ�����ѡ��ɾ����
                existingFClass = fws.OpenFeatureClass("road_bf30.shp");
                IDataset ds = existingFClass as IDataset;
                ds.Delete();
            }
            if (ws2.get_NameExists(esriDatasetType.esriDTFeatureClass, "road.shp"))
            {
                //
                layer = new FeatureLayerClass();
                existingFClass = fws.OpenFeatureClass("road.shp");
                layer.FeatureClass = existingFClass;
                layer.Name = existingFClass.AliasName;
                axMapControl1.Map.AddLayer(layer);
            }
            //��ʼ��GP 
            IGeoProcessor2 gp2 = new GeoProcessor() as IGeoProcessor2;
            gp2.OverwriteOutput = true;
            IVariantArray parameters = new VarArrayClass();
            parameters.Add(@"D:\CSU\road.shp");
            parameters.Add(@"D:\CSU\road_bf30.shp");
            parameters.Add("10 meters");
            // Execute the tool.
            gp2.Execute("Buffer_analysis", parameters, null);

            layer = new FeatureLayerClass();
            existingFClass = fws.OpenFeatureClass("road_bf30.shp");
            layer.FeatureClass = existingFClass;
            layer.Name = existingFClass.AliasName;
            axMapControl1.Map.AddLayer(layer);
            axMapControl1.ActiveView.Refresh();
        }

        private void listFeatureClassToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            IWorkspace ws = null;
            IFeatureClass existingFClass = null;
            IFeatureLayer layer = null;
            IWorkspaceFactory wsf = new ShapefileWorkspaceFactory();
            ws = wsf.OpenFromFile(@"d:\csu", 0);
            IFeatureWorkspace fws = (IFeatureWorkspace)ws;
            List<IFeatureClass> lstFeatureClass = new List<IFeatureClass>();
            //��ʼ��GP 
            Geoprocessor GP = new Geoprocessor();
            GP.SetEnvironmentValue("workspace", @"d:\csu");
            IGpEnumList featureClasses = GP.ListFeatureClasses("*", "", "");
            string featureClass = featureClasses.Next();
            while (featureClass != "")
            {
                existingFClass = fws.OpenFeatureClass(featureClass);
                layer = new FeatureLayerClass();
                layer.FeatureClass = existingFClass;
                layer.Name = existingFClass.AliasName;
                axMapControl1.Map.AddLayer(layer);
                          
                lstFeatureClass.Add(existingFClass);
                featureClass = featureClasses.Next();
            }
            axMapControl1.ActiveView.Refresh();
        }

        private void loadRasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // AE����դ������
            IWorkspaceFactory wsf = new RasterWorkspaceFactoryClass();
            IRasterWorkspace rw = wsf.OpenFromFile(@"d:\CSU_TEMP", 0) as IRasterWorkspace;
            IRasterDataset rds = rw.OpenRasterDataset("csu.tif");

            //Ӱ����������ж��봴��,ʹ��IRasterPyramid3�ӿ�
            IRasterPyramid3 pRasPyrmid;
            pRasPyrmid = rds as IRasterPyramid3;    //�ӿ�ת��
            if (pRasPyrmid != null)
                if (!(pRasPyrmid.Present))
                    pRasPyrmid.Create();//����������

            IRasterLayer rasterLayer = new RasterLayerClass();
            //����1:
            //rasterLayer.CreateFromDataset(rds);
            //����2:
            IRaster raster = rds.CreateDefaultRaster();
            rasterLayer.CreateFromRaster(raster);

            ILayer layer = rasterLayer as ILayer;
            axMapControl1.Map.AddLayer(layer);
        }

        private void clipRasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand command = new ToolClipRaster();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
            this.axMapControl1.CurrentTool = command as ITool;
        }

        private void markerSymbolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ILayer lyr = axMapControl1.Map.get_Layer(0);
            ISimpleMarkerSymbol pMarkerSymbol = new SimpleMarkerSymbol();
            IRgbColor pRgbColor = new RgbColor();
            pRgbColor.Red = 255;
            pMarkerSymbol.Color = pRgbColor;
            pMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSSquare;

            ISimpleRenderer pSimpleRenderer = new SimpleRenderer();
            pSimpleRenderer.Symbol = (ISymbol)pMarkerSymbol;
            IGeoFeatureLayer layer = (IGeoFeatureLayer)lyr;
            layer.Renderer = (IFeatureRenderer)pSimpleRenderer;
            axMapControl1.ActiveView.Refresh();
            axTOCControl1.Update();
        }

        private void lineSymbolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ILayer lyr = axMapControl1.Map.get_Layer(0);
            ISimpleLineSymbol pMarkerSymbol = new SimpleLineSymbol(); ;
            IRgbColor pRgbColor = new RgbColor();
            pRgbColor.Red = 255;
            pMarkerSymbol.Color = pRgbColor;
            pMarkerSymbol.Style = esriSimpleLineStyle.esriSLSDashDotDot;
            pMarkerSymbol.Width = 5;

            ISimpleRenderer pSimpleRenderer = new SimpleRenderer();
            pSimpleRenderer.Symbol = (ISymbol)pMarkerSymbol;
            IGeoFeatureLayer oLyr = (IGeoFeatureLayer)lyr;
            oLyr.Renderer = (IFeatureRenderer)pSimpleRenderer;
            axMapControl1.ActiveView.Refresh();
            axTOCControl1.Update();
        }

        private void fillSymbolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ILayer lyr = axMapControl1.Map.get_Layer(0);
            IRgbColor pRgbColor = new RgbColor(); pRgbColor.Red = 255;
            ISimpleLineSymbol lineSym = new SimpleLineSymbol();
            lineSym.Color = pRgbColor;
            lineSym.Style = esriSimpleLineStyle.esriSLSDashDotDot;
            lineSym.Width = 5;

            ISimpleFillSymbol pSymbol = new SimpleFillSymbol();
            pSymbol.Outline = lineSym;
            pRgbColor = new RgbColor();
            pRgbColor.Blue = 255;
            pRgbColor.Transparency = 128;   //�����ɫ͸��������
            pSymbol.Color = pRgbColor;

            ISimpleRenderer pSimpleRenderer = new SimpleRenderer();
            pSimpleRenderer.Symbol = (ISymbol)pSymbol;
            IGeoFeatureLayer oLyr = (IGeoFeatureLayer)lyr;
            oLyr.Renderer = (IFeatureRenderer)pSimpleRenderer;
            axMapControl1.ActiveView.Refresh();
            axTOCControl1.Update();
        }

        private void textElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureLayer lyr = axMapControl1.Map.get_Layer(0) as IFeatureLayer;
            IFeatureClass pFeatureClass = lyr.FeatureClass;
            // ����ͼԪ�ķ���, ʾ����
            // ������ɫ            
            IRgbColor pRgbColor = new RgbColor();
            pRgbColor.Red = 255;
            pRgbColor.Green = 0;
            pRgbColor.Blue = 0;
            // ��������            
            IFontDisp pFontDisp = new StdFont() as IFontDisp;
            pFontDisp.Bold = true;
            pFontDisp.Name = "����";
            pFontDisp.Size = 20;
            // ��������            
            ITextSymbol pTextSymbol = new TextSymbol();
            pTextSymbol.Angle = 0;
            pTextSymbol.Color = pRgbColor;
            pTextSymbol.Font = pFontDisp;
            // ɾ�������ı�Ԫ��            
            IActiveView pActiveView = axMapControl1.ActiveView;
            IGraphicsContainer pGraphicsContainer = pActiveView.GraphicsContainer; pGraphicsContainer.DeleteAllElements();
            //����ͼԪ�ķ���, ʾ��(��)��
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IFeature pFeature = pFeatureCursor.NextFeature();
            // ����Ҫ���α�            
            int fieldIndex = pFeatureClass.Fields.FindField("Name");
            while (pFeature != null)
            {
                IArea pArea = pFeature.ShapeCopy as IArea;
                IPoint pPoint = pArea.Centroid; // ��ȡ���� 
                ITextElement pTextElement = new TextElement() as ITextElement;  // �����ı�
                pTextElement.Symbol = pTextSymbol;
                pTextElement.Text = pFeature.get_Value(fieldIndex).ToString();
                // ����ı�Ԫ��                
                IElement pElement = pTextElement as IElement;
                pElement.Geometry = pPoint;
                pGraphicsContainer.AddElement(pElement, 0);
                pFeature = pFeatureCursor.NextFeature();
            }
            // ˢ�µ�ͼ            
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private void labelEngineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ��ע���淨
            // ������ɫ            
            IRgbColor pRgbColor = new RgbColor();
            pRgbColor.Red = 255;
            pRgbColor.Green = 0;
            pRgbColor.Blue = 0;
            // ��������            
            IFontDisp pFontDisp = new StdFont() as IFontDisp;
            pFontDisp.Bold = true;
            pFontDisp.Name = "����";
            pFontDisp.Size = 20;
            // ��������            
            ITextSymbol pTextSymbol = new TextSymbol();
            pTextSymbol.Angle = 0;
            pTextSymbol.Color = pRgbColor;
            pTextSymbol.Font = pFontDisp;
            // ��ע���淨����)
            // ����ͼ���ע           
            IGeoFeatureLayer pGeoFeatureLayer = axMapControl1.get_Layer(0) as IGeoFeatureLayer;
            pGeoFeatureLayer.DisplayAnnotation = true;
            IBasicOverposterLayerProperties pBasicOverposterLayerProprties = new BasicOverposterLayerProperties();
            pBasicOverposterLayerProprties.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolygon;
            // ���ñ�ע����            
            ILabelEngineLayerProperties pLabelEngineLayerProperties = new LabelEngineLayerProperties() as ILabelEngineLayerProperties;
            pLabelEngineLayerProperties.Expression = "[" + "Name" + "]";
            pLabelEngineLayerProperties.Symbol = pTextSymbol;
            pLabelEngineLayerProperties.BasicOverposterLayerProperties = pBasicOverposterLayerProprties;
            // ˢ�µ�ͼ            
            IAnnotateLayerProperties pAnnotateLayerProperties = pLabelEngineLayerProperties as IAnnotateLayerProperties;
            IAnnotateLayerPropertiesCollection pAnnotateLayerPropertiesCollection = pGeoFeatureLayer.AnnotationProperties;
            pAnnotateLayerPropertiesCollection.Clear();
            pAnnotateLayerPropertiesCollection.Add(pAnnotateLayerProperties);
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
        }
    }
}