# 脚本：添加居民点到当前地图文档
import arcpy
arcpy.env.workspace = "d:/csu";
mxd = arcpy.mapping.MapDocument("current");
df  = arcpy.mapping.ListDataFrames(mxd,"*")[0];
newLayer = arcpy.mapping.Layer("d:/csu/jmd.shp");
arcpy.mapping.AddLayer(df,newLayer,"AUTO_ARRANGE");
