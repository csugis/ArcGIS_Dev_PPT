using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapControlAppDemo
{
    public partial class FormChart : Form
    {
        public FormChart()
        {
            InitializeComponent();
        }

        private void FormChart_Load(object sender, EventArgs e)
        {
            //chart是C#统计图控件
            chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧 
            chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
            
        }
        public void  DataBindXY(List<string> cats, List<int> nums)
        {
            chart1.Series[0].Points.DataBindXY(cats, nums);
        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
