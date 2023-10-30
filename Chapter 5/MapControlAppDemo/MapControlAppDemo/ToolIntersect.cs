using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MapControlAppDemo
{
    /// <summary>
    /// Summary description for ToolIntersect.
    /// </summary>
    [Guid("879f80cc-0cc8-4009-896a-f5fd8e7762d6")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("MapControlAppDemo.ToolIntersect")]
    public sealed class ToolIntersect : BaseTool
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
        private IPolyline line = null;
        private IPolygon pg = null;

        public ToolIntersect()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text 
            base.m_caption = "";  //localizable text 
            base.m_message = "This should work in ArcMap/MapControl/PageLayoutControl";  //localizable text
            base.m_toolTip = "";  //localizable text
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                {
                    m_hookHelper = null;
                }
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
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add ToolIntersect.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolIntersect.OnMouseDown implementation
            IRubberBand rb = null;
            if ( pg == null)
            {
                rb = new RubberPolygonClass();
                pg = rb.TrackNew(m_hookHelper.ActiveView.ScreenDisplay, null) as IPolygon;
                IElement pe = new PolygonElementClass();
                pe.Geometry = pg;
                m_hookHelper.ActiveView.GraphicsContainer.AddElement(pe, 0);
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
            else
            {
                if(line == null)
                {
                    rb = new RubberLineClass();
                    line = rb.TrackNew(m_hookHelper.ActiveView.ScreenDisplay, null) as IPolyline;
                    IElement le = new LineElementClass();
                    le.Geometry = line;
                    m_hookHelper.ActiveView.GraphicsContainer.AddElement(le, 0);
                    // 绘线和多边形，判断线是否穿过多边形
                    IRelationalOperator ro = (IRelationalOperator)line;
                    if (ro.Crosses(pg))
                        MessageBox.Show("穿过", "Cross");
                    else
                        MessageBox.Show("不穿过", "Cross");
                    // 图上点击绘制线和多边形，计算线与多边形交集。
                    ITopologicalOperator topo = (ITopologicalOperator)pg;
                    IGeometry geom = topo.Intersect(line, esriGeometryDimension.esriGeometry1Dimension);
                    IPointCollection pc = geom as IPointCollection;
                    for(int i=0; i < pc.PointCount; i++)
                    {
                        IElement ele = new MarkerElementClass();
                        ele.Geometry = pc.Point[i];
                        m_hookHelper.ActiveView.GraphicsContainer.AddElement(ele,0);
                        m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                    }
                }

            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolIntersect.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolIntersect.OnMouseUp implementation
        }
        #endregion
    }
}
