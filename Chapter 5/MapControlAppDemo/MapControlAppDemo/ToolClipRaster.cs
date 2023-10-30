using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MapControlAppDemo
{
    /// <summary>
    /// Summary description for ToolClipRaster.
    /// </summary>
    [Guid("caf88588-a5da-4cb8-9deb-b283d8e8064c")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("MapControlAppDemo.ToolClipRaster")]
    public sealed class ToolClipRaster : BaseTool
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

        public ToolClipRaster()
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
            // TODO: Add ToolClipRaster.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolClipRaster.OnMouseDown implementation
            IRasterLayer rl = m_hookHelper.FocusMap.get_Layer(0) as IRasterLayer;
            IRaster raster = rl.Raster;
            IClipFunctionArguments rasterFuncArgs = new ClipFunctionArguments() as IClipFunctionArguments;
            rasterFuncArgs.Raster = raster;
            rasterFuncArgs.ClippingType = esriRasterClippingType.esriRasterClippingOutside;
            IRubberBand rb = new RubberPolygonClass();
            rasterFuncArgs.ClippingGeometry = rb.TrackNew(m_hookHelper.ActiveView.ScreenDisplay,null);

            IRasterFunction clipFunction = new ClipFunctionClass();
            IFunctionRasterDataset funcRasterDataset = new FunctionRasterDataset();
            IFunctionRasterDatasetName funcRasterDatasetName = new FunctionRasterDatasetName() as IFunctionRasterDatasetName;
            funcRasterDatasetName.FullName = @"d:\CSU_TEMP\clip.afr";  //不是保存
            funcRasterDataset.FullName = (IName)funcRasterDatasetName;
            funcRasterDataset.Init(clipFunction, rasterFuncArgs);

            IRasterDataset rds = funcRasterDataset as IRasterDataset;
            IRasterLayer rLayer = new RasterLayerClass();
            rLayer.CreateFromDataset(rds);
            m_hookHelper.FocusMap.AddLayer(rLayer as ILayer);

            // 保存影像
            string fmt = "TIFF";
            rl = m_hookHelper.FocusMap.get_Layer(0) as IRasterLayer;
            raster = rl.Raster;

            //去黑边
            IRasterProps pRasterProps = raster as IRasterProps;
            pRasterProps.NoDataValue = 255;
            //另存输出
            ISaveAs2 pSaveAs = raster as ISaveAs2;
            if (!pSaveAs.CanSaveAs("TIFF"))
            {
                MessageBox.Show("不支持指定像素类型或文件格式的输出", "提示"
                                , MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            IWorkspaceFactory worksapceFactory = new RasterWorkspaceFactoryClass();
            IWorkspace workspace = worksapceFactory.OpenFromFile(@"d:\CSU_TEMP", 0);
            IDataset dataset = pSaveAs.SaveAs("clip_csu.tif", workspace, fmt);
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolClipRaster.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolClipRaster.OnMouseUp implementation
        }
        #endregion
    }
}
