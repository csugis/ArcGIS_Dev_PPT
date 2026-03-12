import arcpy
arcpy.env.workspace = "d:/csu";
mxd = arcpy.mapping.MapDocument("current");
df = mxd.activeDataFrame;
# 定义要加载的要素类列表
fl = ["d:/csu/jmd.shp"];
for f in fl:
    lyr = arcpy.mapping.Layer(f);
    print lyr
    arcpy.mapping.AddLayer(df,lyr,"AUTO_ARRANGE");
