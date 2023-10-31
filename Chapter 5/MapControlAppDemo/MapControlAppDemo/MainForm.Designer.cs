
namespace MapControlAppDemo
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            //Ensures that any ESRI libraries that have been used are unloaded in the correct order. 
            //Failure to do this may result in random crashes on exit due to the operating system unloading 
            //the libraries in the incorrect order. 
            ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.menuExitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.layerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bufferSelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geoDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listFeatureClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFeatureClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customerizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fieldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.featuresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAFeatureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawAFeatureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFeaturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFeature1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFeature2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateFeaturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intersectCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geometryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.constructMultipointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawPolylineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawPolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spatialAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statChart1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bufferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intersectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.featureIntersectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geoprocessorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geoprocessingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listFeatureClassToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clipRasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBarXY = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.symbolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markerSymbolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineSymbolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillSymbolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textElementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelEngineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.layerToolStripMenuItem,
            this.drawToolStripMenuItem,
            this.geoDatabaseToolStripMenuItem,
            this.geometryToolStripMenuItem,
            this.spatialAnalysisToolStripMenuItem,
            this.symbolToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1145, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewDoc,
            this.menuOpenDoc,
            this.menuSaveDoc,
            this.menuSaveAs,
            this.menuSeparator,
            this.menuExitApp});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(48, 24);
            this.menuFile.Text = "File";
            // 
            // menuNewDoc
            // 
            this.menuNewDoc.Image = ((System.Drawing.Image)(resources.GetObject("menuNewDoc.Image")));
            this.menuNewDoc.ImageTransparentColor = System.Drawing.Color.White;
            this.menuNewDoc.Name = "menuNewDoc";
            this.menuNewDoc.Size = new System.Drawing.Size(224, 26);
            this.menuNewDoc.Text = "New Document";
            this.menuNewDoc.Click += new System.EventHandler(this.menuNewDoc_Click);
            // 
            // menuOpenDoc
            // 
            this.menuOpenDoc.Image = ((System.Drawing.Image)(resources.GetObject("menuOpenDoc.Image")));
            this.menuOpenDoc.ImageTransparentColor = System.Drawing.Color.White;
            this.menuOpenDoc.Name = "menuOpenDoc";
            this.menuOpenDoc.Size = new System.Drawing.Size(224, 26);
            this.menuOpenDoc.Text = "Open Document...";
            this.menuOpenDoc.Click += new System.EventHandler(this.menuOpenDoc_Click);
            // 
            // menuSaveDoc
            // 
            this.menuSaveDoc.Image = ((System.Drawing.Image)(resources.GetObject("menuSaveDoc.Image")));
            this.menuSaveDoc.ImageTransparentColor = System.Drawing.Color.White;
            this.menuSaveDoc.Name = "menuSaveDoc";
            this.menuSaveDoc.Size = new System.Drawing.Size(224, 26);
            this.menuSaveDoc.Text = "SaveDocument";
            this.menuSaveDoc.Click += new System.EventHandler(this.menuSaveDoc_Click);
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(224, 26);
            this.menuSaveAs.Text = "Save As...";
            this.menuSaveAs.Click += new System.EventHandler(this.menuSaveAs_Click);
            // 
            // menuSeparator
            // 
            this.menuSeparator.Name = "menuSeparator";
            this.menuSeparator.Size = new System.Drawing.Size(221, 6);
            // 
            // menuExitApp
            // 
            this.menuExitApp.Name = "menuExitApp";
            this.menuExitApp.Size = new System.Drawing.Size(224, 26);
            this.menuExitApp.Text = "Exit";
            this.menuExitApp.Click += new System.EventHandler(this.menuExitApp_Click);
            // 
            // layerToolStripMenuItem
            // 
            this.layerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addLayerToolStripMenuItem});
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.layerToolStripMenuItem.Text = "Layer";
            // 
            // addLayerToolStripMenuItem
            // 
            this.addLayerToolStripMenuItem.Name = "addLayerToolStripMenuItem";
            this.addLayerToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.addLayerToolStripMenuItem.Text = "Add Layer";
            this.addLayerToolStripMenuItem.Click += new System.EventHandler(this.addLayerToolStripMenuItem_Click);
            // 
            // drawToolStripMenuItem
            // 
            this.drawToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawLineToolStripMenuItem,
            this.bufferSelectToolStripMenuItem});
            this.drawToolStripMenuItem.Name = "drawToolStripMenuItem";
            this.drawToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.drawToolStripMenuItem.Text = "Draw";
            // 
            // drawLineToolStripMenuItem
            // 
            this.drawLineToolStripMenuItem.Name = "drawLineToolStripMenuItem";
            this.drawLineToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.drawLineToolStripMenuItem.Text = "Draw Line";
            this.drawLineToolStripMenuItem.Click += new System.EventHandler(this.drawLineToolStripMenuItem_Click);
            // 
            // bufferSelectToolStripMenuItem
            // 
            this.bufferSelectToolStripMenuItem.Name = "bufferSelectToolStripMenuItem";
            this.bufferSelectToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.bufferSelectToolStripMenuItem.Text = "Buffer Select";
            this.bufferSelectToolStripMenuItem.Click += new System.EventHandler(this.bufferSelectToolStripMenuItem_Click);
            // 
            // geoDatabaseToolStripMenuItem
            // 
            this.geoDatabaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listFeatureClassToolStripMenuItem,
            this.createFeatureClassToolStripMenuItem,
            this.fieldsToolStripMenuItem,
            this.featuresToolStripMenuItem,
            this.intersectCheckToolStripMenuItem});
            this.geoDatabaseToolStripMenuItem.Name = "geoDatabaseToolStripMenuItem";
            this.geoDatabaseToolStripMenuItem.Size = new System.Drawing.Size(120, 24);
            this.geoDatabaseToolStripMenuItem.Text = "GeoDatabase";
            // 
            // listFeatureClassToolStripMenuItem
            // 
            this.listFeatureClassToolStripMenuItem.Name = "listFeatureClassToolStripMenuItem";
            this.listFeatureClassToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.listFeatureClassToolStripMenuItem.Text = "List Feature Class";
            this.listFeatureClassToolStripMenuItem.Click += new System.EventHandler(this.listFeatureClassToolStripMenuItem_Click);
            // 
            // createFeatureClassToolStripMenuItem
            // 
            this.createFeatureClassToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simpleToolStripMenuItem,
            this.customerizeToolStripMenuItem});
            this.createFeatureClassToolStripMenuItem.Name = "createFeatureClassToolStripMenuItem";
            this.createFeatureClassToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.createFeatureClassToolStripMenuItem.Text = "Create Feature Class";
            // 
            // simpleToolStripMenuItem
            // 
            this.simpleToolStripMenuItem.Name = "simpleToolStripMenuItem";
            this.simpleToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.simpleToolStripMenuItem.Text = "Simple";
            this.simpleToolStripMenuItem.Click += new System.EventHandler(this.simpleToolStripMenuItem_Click);
            // 
            // customerizeToolStripMenuItem
            // 
            this.customerizeToolStripMenuItem.Name = "customerizeToolStripMenuItem";
            this.customerizeToolStripMenuItem.Size = new System.Drawing.Size(183, 26);
            this.customerizeToolStripMenuItem.Text = "Customerize";
            this.customerizeToolStripMenuItem.Click += new System.EventHandler(this.customerizeToolStripMenuItem_Click);
            // 
            // fieldsToolStripMenuItem
            // 
            this.fieldsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFieldToolStripMenuItem});
            this.fieldsToolStripMenuItem.Name = "fieldsToolStripMenuItem";
            this.fieldsToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.fieldsToolStripMenuItem.Text = "Fields";
            // 
            // addFieldToolStripMenuItem
            // 
            this.addFieldToolStripMenuItem.Name = "addFieldToolStripMenuItem";
            this.addFieldToolStripMenuItem.Size = new System.Drawing.Size(162, 26);
            this.addFieldToolStripMenuItem.Text = "Add Field";
            this.addFieldToolStripMenuItem.Click += new System.EventHandler(this.addFieldToolStripMenuItem_Click);
            // 
            // featuresToolStripMenuItem
            // 
            this.featuresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAFeatureToolStripMenuItem,
            this.drawAFeatureToolStripMenuItem,
            this.addFeaturesToolStripMenuItem,
            this.deleteFeature1ToolStripMenuItem,
            this.deleteFeature2ToolStripMenuItem,
            this.updateFeaturesToolStripMenuItem});
            this.featuresToolStripMenuItem.Name = "featuresToolStripMenuItem";
            this.featuresToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.featuresToolStripMenuItem.Text = "Features";
            // 
            // addAFeatureToolStripMenuItem
            // 
            this.addAFeatureToolStripMenuItem.Name = "addAFeatureToolStripMenuItem";
            this.addAFeatureToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.addAFeatureToolStripMenuItem.Text = "Add a Feature";
            this.addAFeatureToolStripMenuItem.Click += new System.EventHandler(this.addAFeatureToolStripMenuItem_Click);
            // 
            // drawAFeatureToolStripMenuItem
            // 
            this.drawAFeatureToolStripMenuItem.Name = "drawAFeatureToolStripMenuItem";
            this.drawAFeatureToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.drawAFeatureToolStripMenuItem.Text = "Draw a Feature";
            this.drawAFeatureToolStripMenuItem.Click += new System.EventHandler(this.drawAFeatureToolStripMenuItem_Click);
            // 
            // addFeaturesToolStripMenuItem
            // 
            this.addFeaturesToolStripMenuItem.Name = "addFeaturesToolStripMenuItem";
            this.addFeaturesToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.addFeaturesToolStripMenuItem.Text = "Add Features";
            this.addFeaturesToolStripMenuItem.Click += new System.EventHandler(this.addFeaturesToolStripMenuItem_Click);
            // 
            // deleteFeature1ToolStripMenuItem
            // 
            this.deleteFeature1ToolStripMenuItem.Name = "deleteFeature1ToolStripMenuItem";
            this.deleteFeature1ToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.deleteFeature1ToolStripMenuItem.Text = "Delete Feature1";
            this.deleteFeature1ToolStripMenuItem.Click += new System.EventHandler(this.deleteFeature1ToolStripMenuItem_Click);
            // 
            // deleteFeature2ToolStripMenuItem
            // 
            this.deleteFeature2ToolStripMenuItem.Name = "deleteFeature2ToolStripMenuItem";
            this.deleteFeature2ToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.deleteFeature2ToolStripMenuItem.Text = "Delete Feature2";
            this.deleteFeature2ToolStripMenuItem.Click += new System.EventHandler(this.deleteFeature2ToolStripMenuItem_Click);
            // 
            // updateFeaturesToolStripMenuItem
            // 
            this.updateFeaturesToolStripMenuItem.Name = "updateFeaturesToolStripMenuItem";
            this.updateFeaturesToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.updateFeaturesToolStripMenuItem.Text = "Update Features";
            this.updateFeaturesToolStripMenuItem.Click += new System.EventHandler(this.updateFeaturesToolStripMenuItem_Click);
            // 
            // intersectCheckToolStripMenuItem
            // 
            this.intersectCheckToolStripMenuItem.Name = "intersectCheckToolStripMenuItem";
            this.intersectCheckToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.intersectCheckToolStripMenuItem.Text = "Intersect Check";
            this.intersectCheckToolStripMenuItem.Click += new System.EventHandler(this.intersectCheckToolStripMenuItem_Click);
            // 
            // geometryToolStripMenuItem
            // 
            this.geometryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.constructMultipointToolStripMenuItem,
            this.addLineToolStripMenuItem,
            this.drawPolylineToolStripMenuItem,
            this.drawPolygonToolStripMenuItem,
            this.linkPointsToolStripMenuItem});
            this.geometryToolStripMenuItem.Name = "geometryToolStripMenuItem";
            this.geometryToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.geometryToolStripMenuItem.Text = "Geometry";
            // 
            // constructMultipointToolStripMenuItem
            // 
            this.constructMultipointToolStripMenuItem.Name = "constructMultipointToolStripMenuItem";
            this.constructMultipointToolStripMenuItem.Size = new System.Drawing.Size(244, 26);
            this.constructMultipointToolStripMenuItem.Text = "Construct Multipoint";
            this.constructMultipointToolStripMenuItem.Click += new System.EventHandler(this.constructMultipointToolStripMenuItem_Click);
            // 
            // addLineToolStripMenuItem
            // 
            this.addLineToolStripMenuItem.Name = "addLineToolStripMenuItem";
            this.addLineToolStripMenuItem.Size = new System.Drawing.Size(244, 26);
            this.addLineToolStripMenuItem.Text = "Add Line";
            this.addLineToolStripMenuItem.Click += new System.EventHandler(this.addLineToolStripMenuItem_Click);
            // 
            // drawPolylineToolStripMenuItem
            // 
            this.drawPolylineToolStripMenuItem.Name = "drawPolylineToolStripMenuItem";
            this.drawPolylineToolStripMenuItem.Size = new System.Drawing.Size(244, 26);
            this.drawPolylineToolStripMenuItem.Text = "Draw Polyline";
            this.drawPolylineToolStripMenuItem.Click += new System.EventHandler(this.drawPolylineToolStripMenuItem_Click);
            // 
            // drawPolygonToolStripMenuItem
            // 
            this.drawPolygonToolStripMenuItem.Name = "drawPolygonToolStripMenuItem";
            this.drawPolygonToolStripMenuItem.Size = new System.Drawing.Size(244, 26);
            this.drawPolygonToolStripMenuItem.Text = "Draw Polygon";
            this.drawPolygonToolStripMenuItem.Click += new System.EventHandler(this.drawPolygonToolStripMenuItem_Click);
            // 
            // linkPointsToolStripMenuItem
            // 
            this.linkPointsToolStripMenuItem.Name = "linkPointsToolStripMenuItem";
            this.linkPointsToolStripMenuItem.Size = new System.Drawing.Size(244, 26);
            this.linkPointsToolStripMenuItem.Text = "Link Points";
            this.linkPointsToolStripMenuItem.Click += new System.EventHandler(this.linkPointsToolStripMenuItem_Click);
            // 
            // spatialAnalysisToolStripMenuItem
            // 
            this.spatialAnalysisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statChartToolStripMenuItem,
            this.statChart1ToolStripMenuItem,
            this.bufferToolStripMenuItem,
            this.intersectToolStripMenuItem,
            this.featureIntersectToolStripMenuItem,
            this.geoprocessorToolStripMenuItem,
            this.geoprocessingToolStripMenuItem,
            this.listFeatureClassToolStripMenuItem1,
            this.loadRasterToolStripMenuItem,
            this.clipRasterToolStripMenuItem});
            this.spatialAnalysisToolStripMenuItem.Name = "spatialAnalysisToolStripMenuItem";
            this.spatialAnalysisToolStripMenuItem.Size = new System.Drawing.Size(134, 24);
            this.spatialAnalysisToolStripMenuItem.Text = "Spatial Analysis";
            // 
            // statChartToolStripMenuItem
            // 
            this.statChartToolStripMenuItem.Name = "statChartToolStripMenuItem";
            this.statChartToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.statChartToolStripMenuItem.Text = "Stat Chart";
            this.statChartToolStripMenuItem.Click += new System.EventHandler(this.statChartToolStripMenuItem_Click);
            // 
            // statChart1ToolStripMenuItem
            // 
            this.statChart1ToolStripMenuItem.Name = "statChart1ToolStripMenuItem";
            this.statChart1ToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.statChart1ToolStripMenuItem.Text = "Stat Chart1";
            this.statChart1ToolStripMenuItem.Click += new System.EventHandler(this.statChart1ToolStripMenuItem_Click);
            // 
            // bufferToolStripMenuItem
            // 
            this.bufferToolStripMenuItem.Name = "bufferToolStripMenuItem";
            this.bufferToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.bufferToolStripMenuItem.Text = "Buffer";
            this.bufferToolStripMenuItem.Click += new System.EventHandler(this.bufferToolStripMenuItem_Click);
            // 
            // intersectToolStripMenuItem
            // 
            this.intersectToolStripMenuItem.Name = "intersectToolStripMenuItem";
            this.intersectToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.intersectToolStripMenuItem.Text = "Intersect";
            this.intersectToolStripMenuItem.Click += new System.EventHandler(this.intersectToolStripMenuItem_Click);
            // 
            // featureIntersectToolStripMenuItem
            // 
            this.featureIntersectToolStripMenuItem.Name = "featureIntersectToolStripMenuItem";
            this.featureIntersectToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.featureIntersectToolStripMenuItem.Text = "Feature Intersect";
            this.featureIntersectToolStripMenuItem.Click += new System.EventHandler(this.featureIntersectToolStripMenuItem_Click);
            // 
            // geoprocessorToolStripMenuItem
            // 
            this.geoprocessorToolStripMenuItem.Name = "geoprocessorToolStripMenuItem";
            this.geoprocessorToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.geoprocessorToolStripMenuItem.Text = "Geoprocessor";
            this.geoprocessorToolStripMenuItem.Click += new System.EventHandler(this.geoprocessorToolStripMenuItem_Click);
            // 
            // geoprocessingToolStripMenuItem
            // 
            this.geoprocessingToolStripMenuItem.Name = "geoprocessingToolStripMenuItem";
            this.geoprocessingToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.geoprocessingToolStripMenuItem.Text = "Geoprocessing";
            this.geoprocessingToolStripMenuItem.Click += new System.EventHandler(this.geoprocessingToolStripMenuItem_Click);
            // 
            // listFeatureClassToolStripMenuItem1
            // 
            this.listFeatureClassToolStripMenuItem1.Name = "listFeatureClassToolStripMenuItem1";
            this.listFeatureClassToolStripMenuItem1.Size = new System.Drawing.Size(216, 26);
            this.listFeatureClassToolStripMenuItem1.Text = "List Feature Class";
            this.listFeatureClassToolStripMenuItem1.Click += new System.EventHandler(this.listFeatureClassToolStripMenuItem1_Click);
            // 
            // loadRasterToolStripMenuItem
            // 
            this.loadRasterToolStripMenuItem.Name = "loadRasterToolStripMenuItem";
            this.loadRasterToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.loadRasterToolStripMenuItem.Text = "Load Raster";
            this.loadRasterToolStripMenuItem.Click += new System.EventHandler(this.loadRasterToolStripMenuItem_Click);
            // 
            // clipRasterToolStripMenuItem
            // 
            this.clipRasterToolStripMenuItem.Name = "clipRasterToolStripMenuItem";
            this.clipRasterToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.clipRasterToolStripMenuItem.Text = "Clip Raster";
            this.clipRasterToolStripMenuItem.Click += new System.EventHandler(this.clipRasterToolStripMenuItem_Click);
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 28);
            this.axToolbarControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(1145, 28);
            this.axToolbarControl1.TabIndex = 3;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.axTOCControl1.Location = new System.Drawing.Point(4, 56);
            this.axTOCControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(235, 594);
            this.axTOCControl1.TabIndex = 4;
            this.axTOCControl1.OnMouseUp += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseUpEventHandler(this.axTOCControl1_OnMouseUp);
            this.axTOCControl1.OnDoubleClick += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnDoubleClickEventHandler(this.axTOCControl1_OnDoubleClick);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(466, 278);
            this.axLicenseControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 5;
            this.axLicenseControl1.Enter += new System.EventHandler(this.axLicenseControl1_Enter);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 56);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 620);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarXY});
            this.statusStrip1.Location = new System.Drawing.Point(4, 650);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1141, 26);
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusBar1";
            // 
            // statusBarXY
            // 
            this.statusBarXY.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.statusBarXY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusBarXY.Name = "statusBarXY";
            this.statusBarXY.Size = new System.Drawing.Size(71, 20);
            this.statusBarXY.Text = "Test 123";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.enableSelectedToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(195, 100);
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(194, 24);
            this.moveUpToolStripMenuItem.Text = "Move Up";
            this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(194, 24);
            this.moveDownToolStripMenuItem.Text = "Move Down";
            this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.moveDownToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(194, 24);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // enableSelectedToolStripMenuItem
            // 
            this.enableSelectedToolStripMenuItem.Name = "enableSelectedToolStripMenuItem";
            this.enableSelectedToolStripMenuItem.Size = new System.Drawing.Size(194, 24);
            this.enableSelectedToolStripMenuItem.Text = "Enable Selected";
            this.enableSelectedToolStripMenuItem.Click += new System.EventHandler(this.enableSelectedToolStripMenuItem_Click);
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(239, 56);
            this.axMapControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(906, 594);
            this.axMapControl1.TabIndex = 2;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
            this.axMapControl1.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.axMapControl1_OnMapReplaced);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 676);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.axTOCControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "MainForm";
            this.Text = "ArcEngine Controls Application";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuNewDoc;
        private System.Windows.Forms.ToolStripMenuItem menuOpenDoc;
        private System.Windows.Forms.ToolStripMenuItem menuSaveDoc;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
        private System.Windows.Forms.ToolStripMenuItem menuExitApp;
        private System.Windows.Forms.ToolStripSeparator menuSeparator;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarXY;
        private System.Windows.Forms.ToolStripMenuItem layerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addLayerToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bufferSelectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geoDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listFeatureClassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createFeatureClassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simpleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customerizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fieldsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFieldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem featuresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAFeatureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFeaturesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFeature1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFeature2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateFeaturesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intersectCheckToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawAFeatureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geometryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem constructMultipointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawPolylineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawPolygonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linkPointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spatialAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statChartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statChart1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bufferToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intersectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem featureIntersectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geoprocessorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geoprocessingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listFeatureClassToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadRasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clipRasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem symbolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markerSymbolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineSymbolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillSymbolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textElementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labelEngineToolStripMenuItem;
    }
}

