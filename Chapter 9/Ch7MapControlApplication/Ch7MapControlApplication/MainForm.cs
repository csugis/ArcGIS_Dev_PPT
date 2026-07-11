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
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Ch7MapControlApplication
{
    public sealed partial class MainForm : Form
    {
        #region class private members
        private IMapControl3 m_mapControl = null;
        private string m_mapDocumentName = string.Empty;
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

        private void textEleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 创建颜色            
            IRgbColor pRgbColor = new RgbColor();
            pRgbColor.Red = 255; pRgbColor.Green = 0; pRgbColor.Blue = 0;
            // 创建字体            
            //IFontDisp pFontDisp = new StdFont() as IFontDisp;
            //pFontDisp.Bold = true; pFontDisp.Name = "楷体"; pFontDisp.Size = 20;
            // 创建符号            
            ITextSymbol pTextSymbol = new TextSymbol();
            pTextSymbol.Angle = 0;
            pTextSymbol.Color = pRgbColor;
            //pTextSymbol.Font = pFontDisp;
            // 删除已有文本元素            
            IActiveView pActiveView = axMapControl1.ActiveView;
            IGraphicsContainer pGraphicsContainer = pActiveView.GraphicsContainer; 
            pGraphicsContainer.DeleteAllElements();

            IFeatureClass pFeatureClass = ((IFeatureLayer)axMapControl1.get_Layer(0)).FeatureClass;

            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IFeature pFeature;
            int fieldIndex = pFeatureClass.Fields.FindField("name");
            while ((pFeature = pFeatureCursor.NextFeature()) != null)
            {// 遍历要素游标      
                IArea pArea = pFeature.ShapeCopy as IArea;
                ITextElement pTextElement = new TextElement() as ITextElement;  // 创建文本
                pTextElement.Symbol = pTextSymbol;
                pTextElement.Text = pFeature.get_Value(fieldIndex).ToString();
                IElement pElement = pTextElement as IElement;
                pElement.Geometry = pArea.Centroid; // 获取重心;                
                pGraphicsContainer.AddElement(pElement, 0);
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

        }

        private void annotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 创建颜色            
            IRgbColor pRgbColor = new RgbColor();
            pRgbColor.Red = 255; pRgbColor.Green = 0; pRgbColor.Blue = 0;
            // 创建字体            
            //IFontDisp pFontDisp = new StdFont() as IFontDisp;
            //pFontDisp.Bold = true; pFontDisp.Name = "楷体"; pFontDisp.Size = 20;
            // 创建符号            
            ITextSymbol pTextSymbol = new TextSymbol();
            pTextSymbol.Angle = 0;
            pTextSymbol.Color = pRgbColor;
            IGeoFeatureLayer pGeoFeatureLayer = axMapControl1.get_Layer(0) as IGeoFeatureLayer;
            pGeoFeatureLayer.DisplayAnnotation = true;          // 开启图层标注      
            IBasicOverposterLayerProperties pBasicOverposterLayerProprties = new BasicOverposterLayerProperties();
            pBasicOverposterLayerProprties.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolygon; ILabelEngineLayerProperties pLabelEngineLayerProperties = new LabelEngineLayerProperties() as ILabelEngineLayerProperties;       // 设置标注属性         
            pLabelEngineLayerProperties.Expression = "[" + "Name" + "]";    //指定标注的字段        
            pLabelEngineLayerProperties.Symbol = pTextSymbol;
            pLabelEngineLayerProperties.BasicOverposterLayerProperties = pBasicOverposterLayerProprties;
            // 刷新地图            
            IAnnotateLayerProperties pAnnotateLayerProperties = pLabelEngineLayerProperties as IAnnotateLayerProperties;
            IAnnotateLayerPropertiesCollection pAnnotateLayerPropertiesCollection = pGeoFeatureLayer.AnnotationProperties;
            pAnnotateLayerPropertiesCollection.Clear();
            pAnnotateLayerPropertiesCollection.Add(pAnnotateLayerProperties);
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
        }
    }
}