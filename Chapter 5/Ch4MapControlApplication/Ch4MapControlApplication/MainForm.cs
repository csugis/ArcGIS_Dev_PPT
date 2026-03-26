using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

namespace Ch4MapControlApplication
{
    public sealed partial class MainForm : Form
    {
        #region class private members
        private IMapControl3 m_mapControl = null;
        private string m_mapDocumentName = string.Empty;
        private ILayer m_selectedlayer = null;
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

        private void axMapControl1_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
        {
            // 放大窗口2倍
            IEnvelope envelope = axMapControl1.Extent;
            envelope.Expand(0.5, 0.5, true);
            axMapControl1.Extent = envelope;
        }

        private void axMapControl1_OnKeyDown(object sender, IMapControlEvents2_OnKeyDownEvent e)
        {

        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            // 创建RubberBand对象用于交互式矩形绘制
            IRubberBand rubberBand = new RubberEnvelopeClass();

            // 获取屏幕显示对象
            IScreenDisplay screenDisplay = axMapControl1.ActiveView.ScreenDisplay;

            // 使用RubberBand跟踪绘制矩形，参数true表示使用地图坐标
            IGeometry geometry = rubberBand.TrackNew(screenDisplay, null);

            // 如果绘制成功且几何对象不为空，则放大到绘制区域
            if (geometry != null && !geometry.IsEmpty)
            {
                axMapControl1.Extent = geometry.Envelope;
            }
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

        private void statusBarXY_Click(object sender, EventArgs e)
        {

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
    }
}