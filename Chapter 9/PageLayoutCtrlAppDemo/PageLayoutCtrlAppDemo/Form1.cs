using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using stdole;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.CartoUI;


namespace PageLayoutCtrlAppDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void markerSymbolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ILayer lyr = axPageLayoutControl1.ActiveView.FocusMap.get_Layer(0);
            ISimpleMarkerSymbol pMarkerSymbol = new SimpleMarkerSymbol();
            IRgbColor pRgbColor = new RgbColor();
            pRgbColor.Red = 255;
            pMarkerSymbol.Color = pRgbColor;
            pMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSSquare;

            ISimpleRenderer pSimpleRenderer = new SimpleRenderer();
            pSimpleRenderer.Symbol = (ISymbol)pMarkerSymbol;
            IGeoFeatureLayer layer = (IGeoFeatureLayer)lyr;
            layer.Renderer = (IFeatureRenderer)pSimpleRenderer;
            axPageLayoutControl1.ActiveView.Refresh();
            axTOCControl1.Update();
        }

        private void lineSymbolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ILayer lyr = axPageLayoutControl1.ActiveView.FocusMap.get_Layer(0);
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
            axPageLayoutControl1.ActiveView.Refresh();
            axTOCControl1.Update();

        }

        private void fillSymbolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ILayer lyr = axPageLayoutControl1.ActiveView.FocusMap.get_Layer(0);
            IRgbColor pRgbColor = new RgbColor(); pRgbColor.Red = 255;
            ISimpleLineSymbol lineSym = new SimpleLineSymbol();
            lineSym.Color = pRgbColor;
            lineSym.Style = esriSimpleLineStyle.esriSLSDashDotDot;
            lineSym.Width = 5;

            ISimpleFillSymbol pSymbol = new SimpleFillSymbol();
            pSymbol.Outline = lineSym;
            pRgbColor = new RgbColor();
            pRgbColor.Blue = 255;
            pRgbColor.Transparency = 128;   //填充颜色透明度设置
            pSymbol.Color = pRgbColor;

            ISimpleRenderer pSimpleRenderer = new SimpleRenderer();
            pSimpleRenderer.Symbol = (ISymbol)pSymbol;
            IGeoFeatureLayer oLyr = (IGeoFeatureLayer)lyr;
            oLyr.Renderer = (IFeatureRenderer)pSimpleRenderer;
            axPageLayoutControl1.ActiveView.Refresh();
            axTOCControl1.Update();
        }

        private void textElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 测试与页面坐标不能吻合，无法在PageLayoutControl中使用
            IFeatureLayer lyr = axPageLayoutControl1.ActiveView.FocusMap.get_Layer(0) as IFeatureLayer;
            IFeatureClass pFeatureClass = lyr.FeatureClass;
            // 绘制图元的方法, 示例：
            // 创建颜色            
            IRgbColor pRgbColor = new RgbColor();
            pRgbColor.Red = 255;
            pRgbColor.Green = 0;
            pRgbColor.Blue = 0;
            // 创建字体            
            IFontDisp pFontDisp = new StdFont() as IFontDisp;
            pFontDisp.Bold = true;
            pFontDisp.Name = "楷体";
            pFontDisp.Size = 20;
            // 创建符号            
            ITextSymbol pTextSymbol = new TextSymbol();
            pTextSymbol.Angle = 0;
            pTextSymbol.Color = pRgbColor;
            pTextSymbol.Font = pFontDisp;
            // 删除已有文本元素            
            IActiveView pActiveView = axPageLayoutControl1.ActiveView;
            IGraphicsContainer pGraphicsContainer = pActiveView.GraphicsContainer;

            //pGraphicsContainer.DeleteAllElements();
            //绘制图元的方法, 示例(续)：
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IFeature pFeature = pFeatureCursor.NextFeature();
            // 遍历要素游标            
            int fieldIndex = pFeatureClass.Fields.FindField("Name");
            while (pFeature != null)
            {
                IArea pArea = pFeature.ShapeCopy as IArea;
                IPoint pPoint = pArea.Centroid; // 获取重心 
                // 获取要素的空间参考
                ISpatialReference featureSpatialRef = pPoint.SpatialReference;
                int X, Y;
                pActiveView.ScreenDisplay.DisplayTransformation.FromMapPoint(pPoint, out X, out Y);
                IPoint pagePoint = new PointClass();
                pagePoint.PutCoords(X, Y);
                ITextElement pTextElement = new TextElement() as ITextElement;  // 创建文本
                pTextElement.Symbol = pTextSymbol;
                pTextElement.Text = pFeature.get_Value(fieldIndex).ToString();
                // 添加文本元素                
                IElement pElement = pTextElement as IElement;
                pElement.Geometry = pagePoint;
                pGraphicsContainer.AddElement(pElement, 0);
                pFeature = pFeatureCursor.NextFeature();
            }
            // 刷新地图            
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private void labelEngineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 标注引擎法
            // 创建颜色            
            IRgbColor pRgbColor = new RgbColor();
            pRgbColor.Red = 255;
            pRgbColor.Green = 0;
            pRgbColor.Blue = 0;
            // 创建字体            
            IFontDisp pFontDisp = new StdFont() as IFontDisp;
            pFontDisp.Bold = true;
            pFontDisp.Name = "楷体";
            pFontDisp.Size = 20;
            // 创建符号            
            ITextSymbol pTextSymbol = new TextSymbol();
            pTextSymbol.Angle = 0;
            pTextSymbol.Color = pRgbColor;
            pTextSymbol.Font = pFontDisp;
            // 标注引擎法（续)
            // 开启图层标注           
            IGeoFeatureLayer pGeoFeatureLayer = axPageLayoutControl1.ActiveView.FocusMap.get_Layer(0) as IGeoFeatureLayer;
            pGeoFeatureLayer.DisplayAnnotation = true;
            IBasicOverposterLayerProperties pBasicOverposterLayerProprties = new BasicOverposterLayerProperties();
            pBasicOverposterLayerProprties.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolygon;
            // 设置标注属性            
            ILabelEngineLayerProperties pLabelEngineLayerProperties = new LabelEngineLayerProperties() as ILabelEngineLayerProperties;
            pLabelEngineLayerProperties.Expression = "[" + "Name" + "]";
            pLabelEngineLayerProperties.Symbol = pTextSymbol;
            pLabelEngineLayerProperties.BasicOverposterLayerProperties = pBasicOverposterLayerProprties;
            // 刷新地图            
            IAnnotateLayerProperties pAnnotateLayerProperties = pLabelEngineLayerProperties as IAnnotateLayerProperties;
            IAnnotateLayerPropertiesCollection pAnnotateLayerPropertiesCollection = pGeoFeatureLayer.AnnotationProperties;
            pAnnotateLayerPropertiesCollection.Clear();
            pAnnotateLayerPropertiesCollection.Add(pAnnotateLayerProperties);
            axPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);

        }

        private void mapFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //测试对话框均无法弹出
            //设置边框属性
            IStyleSelector ipSSelector = new BorderSelectorClass();
            if (ipSSelector.DoModal(axPageLayoutControl1.hWnd))
            {
                IMap map = axPageLayoutControl1.ActiveView.FocusMap;
                IGraphicsContainer gContainer = axPageLayoutControl1.GraphicsContainer;
                IMapFrame mapFrame = (IMapFrame)gContainer.FindFrame(map);
                mapFrame.Border = (IBorder)ipSSelector.GetStyle(0);
                axPageLayoutControl1.Refresh();
            }
            // 设置背景属性
            ipSSelector = new BackgroundSelectorClass();
            if (ipSSelector.DoModal(axPageLayoutControl1.hWnd))
            {
                IMap map = axPageLayoutControl1.ActiveView.FocusMap;
                IGraphicsContainer gContainer = axPageLayoutControl1.GraphicsContainer;
                IMapFrame mapFrame = (IMapFrame)gContainer.FindFrame(map);
                mapFrame.Background = (IBackground)ipSSelector.GetStyle(0);
                axPageLayoutControl1.Refresh();
            }
            // 设置阴影属性
            ipSSelector = new ShadowSelectorClass();
            if (ipSSelector.DoModal(axPageLayoutControl1.hWnd))
            {
                IMap map = axPageLayoutControl1.ActiveView.FocusMap;
                IGraphicsContainer gContainer = axPageLayoutControl1.GraphicsContainer;
                IFrameProperties fProperties = (IFrameProperties)gContainer.FindFrame(map);
                fProperties.Shadow = (IShadow)ipSSelector.GetStyle(0);
                axPageLayoutControl1.Refresh();
            }

        }
    }
}
