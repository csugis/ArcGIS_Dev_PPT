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
    }
}