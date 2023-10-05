using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace EngineClassLibraryDemo
{
    public partial class FormSelection : Form
    {
                public FormSelection()
        {
            InitializeComponent();
        }

        public void SetSection(List<IFeature> list)
        {
            this.lblMsg.Text = "选中的要素数：" + list.Count.ToString();
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("X", typeof(float));
            table.Columns.Add("Y", typeof(float));
            foreach (IFeature f in list)
            {
                IPoint pt = f.Shape as IPoint;
                table.Rows.Add(f.OID, pt.X, pt.Y);
            }
            this.dataGridView1.DataSource = table;
            this.dataGridView1.Refresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormSelection_Load(object sender, EventArgs e)
        {

        }
    }
}
