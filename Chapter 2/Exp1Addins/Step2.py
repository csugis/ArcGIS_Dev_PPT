import arcpy
arcpy.env.workspace = arcpy.GetParameterAsText(0);
mxd = arcpy.mapping.MapDocument("current");
df = mxd.activeDataFrame;
fl = arcpy.ListFeatureClasses();
for f in fl:
    lyr = arcpy.mapping.Layer(f);
    arcpy.mapping.AddLayer(df,lyr,"AUTO_ARRANGE");
    print f;
