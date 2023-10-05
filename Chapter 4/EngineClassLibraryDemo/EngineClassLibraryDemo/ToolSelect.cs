using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

namespace EngineClassLibraryDemo
{
    /// <summary>
    /// Summary description for ToolSelect.
    /// </summary>
    [Guid("c3103d53-ca1b-4bb1-b921-f61a1ae7e952")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("EngineClassLibraryDemo.ToolSelect")]
    public sealed class ToolSelect : BaseTool
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
        private ILayer m_layer = null;
        public ToolSelect()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "CSharp Toolbar"; //localizable text 
            base.m_caption = "ToolSelect";  //localizable text 
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
            // TODO: Add ToolSelect.OnClick implementation
            IEnumLayer layers = this.m_hookHelper.FocusMap.Layers;
            layers.Reset();
            ILayer layer;
            while ((layer = layers.Next()) != null)
            {
                if (layer.Name == "poi_yuelu")
                    break;
            }
            m_layer = layer;
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolSelect.OnMouseDown implementation
            IRubberBand rubber = new RubberPolygonClass();
            IPolygon polygon = rubber.TrackNew(m_hookHelper.ActiveView.ScreenDisplay, null) as IPolygon;
            ISpatialFilter filter = new SpatialFilterClass();
            filter.Geometry = polygon;
            filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            IFeatureSelection set = m_layer as IFeatureSelection;
            set.SelectFeatures(filter, esriSelectionResultEnum.esriSelectionResultNew, false);
            set.SelectionSet.Refresh();
            m_hookHelper.ActiveView.Refresh();
            IMap pMap = m_hookHelper.FocusMap;
            List<IFeature> list = new List<IFeature>();
            IEnumFeature pEnumFeat = (IEnumFeature)pMap.FeatureSelection;
            pEnumFeat.Reset();
            IFields fields;
            IFeature pfeat = pEnumFeat.Next();
            while (pfeat != null)
            {
                fields = pfeat.Fields;
                list.Add(pfeat);
                pfeat = pEnumFeat.Next();
            }
            FormSelection frm = new FormSelection();
            frm.SetSection(list);
            frm.ShowDialog();
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolSelect.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolSelect.OnMouseUp implementation
        }
        #endregion
    }
}
