using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace EngineClassLibraryDemo
{
    /// <summary>
    /// Summary description for ArcGISToolbarCSharp.
    /// </summary>
    [Guid("19d02e6b-dfe2-4fc2-a314-027e7d49b0d9")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("EngineClassLibraryDemo.ArcGISToolbarCSharp")]
    public sealed class ArcGISToolbarCSharp : BaseToolbar
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
            MxCommandBars.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommandBars.Unregister(regKey);
        }

        #endregion
        #endregion

        public ArcGISToolbarCSharp()
        {
            //
            // TODO: Define your toolbar here by adding items
            //
            AddItem("esriArcMapUI.ZoomInTool");
            AddItem("EngineClassLibraryDemo.ToolSelect");            
            AddItem("EngineClassLibraryDemo.CmdSelection");
            BeginGroup(); //Separator
            AddItem("{FBF8C3FB-0480-11D2-8D21-080009EE4E51}", 1); //undo command
            AddItem(new Guid("FBF8C3FB-0480-11D2-8D21-080009EE4E51"), 2); //redo command
        }

        public override string Caption
        {
            get
            {
                //TODO: Replace bar caption
                return "CSharp Toolbar";
            }
        }
        public override string Name
        {
            get
            {
                //TODO: Replace bar ID
                return "ArcGISToolbarCSharp";
            }
        }
    }
}