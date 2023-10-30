using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace MapControlAppDemo
{
    public partial class Form1 : Form
    {
        private IHookHelper m_hookHelper = null;
        public Form1()
        {
            InitializeComponent();
        }
        public void setDataTable(DataTable dt)
        {
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Refresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void setHookHelper(object hook)
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
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (m_hookHelper != null)
            {
                // 双击表格定位地图程序：
                IFeatureLayer layer = m_hookHelper.FocusMap.get_Layer(0) as IFeatureLayer;
                IFeatureClass featureClass = layer.FeatureClass;
                m_hookHelper.FocusMap.ClearSelection();
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                int fid1 = int.Parse(row.Cells[0].Value.ToString()), fid2 = int.Parse(row.Cells[0].Value.ToString());
                IQueryFilter qf = new QueryFilterClass();
                qf.WhereClause = "FID = " + fid1;
                IFeatureCursor cur = featureClass.Search(qf, true);
                IFeature feat1 = cur.NextFeature();
                qf.WhereClause = "FID = " + fid2;
                cur = featureClass.Search(qf, true);
                IFeature feat2 = cur.NextFeature();                
                m_hookHelper.FocusMap.SelectFeature(layer, feat1);
                m_hookHelper.FocusMap.SelectFeature(layer, feat2);

                ITopologicalOperator topo = (ITopologicalOperator)feat1.Shape;                
                IGeometry geom = topo.Union(feat2.Shape);
                IEnvelope env = geom.Envelope; 
                env.Expand(1.3, 1.3, true);
                m_hookHelper.ActiveView.Extent = env;
                m_hookHelper.ActiveView.Refresh();
                //m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            } 
        }
    }
}
