
namespace PageLayoutCtrlAppDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.symbolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markerSymbolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineSymbolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillSymbolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textElementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelEngineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axPageLayoutControl1.Location = new System.Drawing.Point(332, 63);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(925, 618);
            this.axPageLayoutControl1.TabIndex = 0;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(905, 449);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 1;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.axTOCControl1.Location = new System.Drawing.Point(1, 63);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(331, 618);
            this.axTOCControl1.TabIndex = 2;
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Location = new System.Drawing.Point(1, 35);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(1256, 28);
            this.axToolbarControl1.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.symbolToolStripMenuItem,
            this.layoutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1257, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // symbolToolStripMenuItem
            // 
            this.symbolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.markerSymbolToolStripMenuItem,
            this.lineSymbolToolStripMenuItem,
            this.fillSymbolToolStripMenuItem,
            this.textElementToolStripMenuItem,
            this.labelEngineToolStripMenuItem});
            this.symbolToolStripMenuItem.Name = "symbolToolStripMenuItem";
            this.symbolToolStripMenuItem.Size = new System.Drawing.Size(78, 24);
            this.symbolToolStripMenuItem.Text = "Symbol";
            // 
            // markerSymbolToolStripMenuItem
            // 
            this.markerSymbolToolStripMenuItem.Name = "markerSymbolToolStripMenuItem";
            this.markerSymbolToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.markerSymbolToolStripMenuItem.Text = "Marker Symbol";
            this.markerSymbolToolStripMenuItem.Click += new System.EventHandler(this.markerSymbolToolStripMenuItem_Click);
            // 
            // lineSymbolToolStripMenuItem
            // 
            this.lineSymbolToolStripMenuItem.Name = "lineSymbolToolStripMenuItem";
            this.lineSymbolToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.lineSymbolToolStripMenuItem.Text = "Line Symbol";
            this.lineSymbolToolStripMenuItem.Click += new System.EventHandler(this.lineSymbolToolStripMenuItem_Click);
            // 
            // fillSymbolToolStripMenuItem
            // 
            this.fillSymbolToolStripMenuItem.Name = "fillSymbolToolStripMenuItem";
            this.fillSymbolToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.fillSymbolToolStripMenuItem.Text = "Fill Symbol";
            this.fillSymbolToolStripMenuItem.Click += new System.EventHandler(this.fillSymbolToolStripMenuItem_Click);
            // 
            // textElementToolStripMenuItem
            // 
            this.textElementToolStripMenuItem.Name = "textElementToolStripMenuItem";
            this.textElementToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.textElementToolStripMenuItem.Text = "Text Element";
            this.textElementToolStripMenuItem.Click += new System.EventHandler(this.textElementToolStripMenuItem_Click);
            // 
            // labelEngineToolStripMenuItem
            // 
            this.labelEngineToolStripMenuItem.Name = "labelEngineToolStripMenuItem";
            this.labelEngineToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.labelEngineToolStripMenuItem.Text = "Label Engine";
            this.labelEngineToolStripMenuItem.Click += new System.EventHandler(this.labelEngineToolStripMenuItem_Click);
            // 
            // layoutToolStripMenuItem
            // 
            this.layoutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapFrameToolStripMenuItem});
            this.layoutToolStripMenuItem.Name = "layoutToolStripMenuItem";
            this.layoutToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.layoutToolStripMenuItem.Text = "Layout";
            // 
            // mapFrameToolStripMenuItem
            // 
            this.mapFrameToolStripMenuItem.Name = "mapFrameToolStripMenuItem";
            this.mapFrameToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.mapFrameToolStripMenuItem.Text = "MapFrame";
            this.mapFrameToolStripMenuItem.Click += new System.EventHandler(this.mapFrameToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 682);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.axTOCControl1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axPageLayoutControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem symbolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markerSymbolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineSymbolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillSymbolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textElementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labelEngineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapFrameToolStripMenuItem;
    }
}

