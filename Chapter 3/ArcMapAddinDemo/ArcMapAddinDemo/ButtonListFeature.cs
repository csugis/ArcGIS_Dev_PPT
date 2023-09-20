using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace ArcMapAddinDemo
{
    public class ButtonListFeature : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public ButtonListFeature()
        {
        }

        protected override void OnClick()
        {
            //
            //  TODO: Sample code showing how to access button host
            //
            ArcMap.Application.CurrentTool = null;
            IMap pMap = ArcMap.Document.FocusMap;
            List<IFeature> list = new List<IFeature>();
            IEnumFeature pEnumFeat = (IEnumFeature)pMap.FeatureSelection;
            pEnumFeat.Reset();
            IFeature pfeat = pEnumFeat.Next();
            while (pfeat != null)
            {
                list.Add(pfeat);
                pfeat = pEnumFeat.Next();
            }
            MessageBox.Show("Selected Features:" + list.Count.ToString(), "CSUGIS");
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
