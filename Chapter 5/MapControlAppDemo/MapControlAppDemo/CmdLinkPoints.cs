using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MapControlAppDemo
{
    /// <summary>
    /// Command that works in ArcMap/Map/PageLayout
    /// </summary>
    [Guid("38c7fb08-09c6-4b38-aa74-b10a8a75dff6")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("MapControlAppDemo.CmdLinkPoints")]
    public sealed class CmdLinkPoints : BaseCommand
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper = null;
        public CmdLinkPoints()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text
            base.m_caption = "";  //localizable text 
            base.m_message = "This should work in ArcMap/MapControl/PageLayoutControl";  //localizable text
            base.m_toolTip = "";  //localizable text
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                    m_hookHelper = null;
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add CmdLinkPoints.OnClick implementation
            // 随机生成100个点，并绘制在地图上
            IGraphicsContainer gc = m_hookHelper.ActiveView.GraphicsContainer;
            ISimpleMarkerSymbol markerSym =
                        new SimpleMarkerSymbol();
            markerSym.Style = esriSimpleMarkerStyle.esriSMSCircle;
            IRgbColor color = new RgbColorClass();
            color.Red = 8; color.Green = 8; color.Blue = 8;
            markerSym.Color = color;
            markerSym.Size = 2;
            IMarkerElement me;
            IElement el;

            IPointCollection points = new MultipointClass();
            IPoint pt;
            Random rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                pt = new PointClass();
                pt.PutCoords(
                          rand.NextDouble() * 100,
                           rand.NextDouble() * 100);
                points.AddPoint(pt);

                me = new MarkerElementClass();
                me.Symbol = markerSym;
                el = me as IElement;
                el.Geometry = pt;
                gc.AddElement(el, 0);
            }
            // 将地图放到至生成的点集并生成凸包
            IEnvelope ext = ((IGeometry)points).Envelope; ext.Expand(1.1, 1.1, true);
            m_hookHelper.ActiveView.Extent = ext;
            //计算凸包
            ITopologicalOperator to = (ITopologicalOperator)points;
            IPolygon hull = to.ConvexHull() as IPolygon;
            IPolygonElement pe = new PolygonElementClass();
            IFillShapeElement fe = (IFillShapeElement)pe;
            color = new RgbColor(); color.NullColor = true; color.Transparency = 255;
            esriSimpleFillStyle fillStyle = esriSimpleFillStyle.esriSFSHollow;
            ISimpleFillSymbol sym = new SimpleFillSymbolClass(); //Symbolizer.CreatePolygonSymbol(fillStyle, color);
            sym.Color = color;
            sym.Style = fillStyle;
            fe.Symbol = sym as ISimpleFillSymbol;
            el = pe as IElement;
            el.Geometry = hull;
            gc.AddElement(el, 0);
            // 计算凸包中心点
            IPoint center = ((IArea)hull).Centroid;
            markerSym = new SimpleMarkerSymbol();
            markerSym.Style = esriSimpleMarkerStyle.esriSMSDiamond;
            color = new RgbColorClass();
            color.Red = 255;
            markerSym.Color = color;
            markerSym.Size = 8;

            me = new MarkerElementClass();
            me.Symbol = markerSym;
            el = me as IElement;
            el.Geometry = center;
            gc.AddElement(el, 0);
            // 生成连接线
            IPolyline line;
            ISimpleLineSymbol lineSymbol = new SimpleLineSymbolClass();
            double len = 0;
            for (int i = 0; i < points.PointCount; i++)
            {
                line = new PolylineClass();
                IPointCollection pc = (IPointCollection)line;
                pc.AddPoint(center);
                pc.AddPoint(points.Point[i]);
                if (line.Length > len)
                {
                    len = line.Length;
                }
                    
                ILineElement le = new LineElementClass();
                color = new RgbColorClass();
                color.Red = rand.Next(0, 255); color.Green = rand.Next(0, 255); color.Blue = rand.Next(0, 255);
                lineSymbol.Color = color;
                le.Symbol = (ILineSymbol)lineSymbol; el = le as IElement;
                el.Geometry = line as IGeometry;
                gc.AddElement(el, 0);
            }

            MessageBox.Show("最长的连线长度：" + len.ToString("0.00"));

        }

        #endregion
    }
}
