using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;

namespace Ch6MapControlApplication
{
    /// <summary>
    /// Summary description for ToolPolygon.
    /// </summary>
    [Guid("729de2d2-4401-464b-b825-c87f479e6a5e")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Ch6MapControlApplication.ToolPolygon")]
    public sealed class ToolPolygon : BaseTool
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
        private IPointCollection pc = new PolygonClass();
        private IElement marker = new MarkerElementClass();
        public ToolPolygon()
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
            // TODO: Add ToolPolygon.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolPolygon.OnMouseDown implementation
            if (Button == 1)
            {
                //创建点
                IPoint pt = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X,Y); 
                
                //表单成员变量List<IPoint> pc = new List<IPoint>();
                pc.AddPoint(pt);
                //创建图元
                IElement marker = new MarkerElementClass();
                marker.Geometry = pt;
                m_hookHelper.ActiveView.GraphicsContainer
                                     .AddElement(marker, 0);

                if (pc.PointCount > 2)
                {
                    IElement el = new PolygonElementClass(); el.Geometry = (IPolygon)pc;
                    m_hookHelper.ActiveView.GraphicsContainer.AddElement(el, 0);
                }

                //刷新屏幕               
                m_hookHelper.ActiveView
                   .PartialRefresh(esriViewDrawPhase.esriViewGraphics,
                                               null, null);
            }
            else if (Button == 2)
            {
                double x = 0, y = 0;

                for (int i = 0; i < pc.PointCount; i++)
                {
                    x += pc.Point[i].X;
                    y += pc.Point[i].Y;
                }
                IPoint cent = new PointClass();
                int n = pc.PointCount;
                cent.PutCoords(x / n, y / n);

                //使用图元绘制中心点，代码略
                //创建图元              
                IElement marker = new MarkerElementClass();
                marker.Geometry = cent;
                m_hookHelper.ActiveView.GraphicsContainer
                                     .AddElement(marker, 0);
                //刷新屏幕               
                m_hookHelper.ActiveView
                   .PartialRefresh(esriViewDrawPhase.esriViewGraphics,
                                               null, null);

            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolPolygon.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolPolygon.OnMouseUp implementation
        }
        #endregion
    }
}
