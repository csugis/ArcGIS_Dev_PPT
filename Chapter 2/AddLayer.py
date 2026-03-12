# 脚本：添加居民点到当前地图文档
import arcpy
import os

arcpy.env.workspace = "d:/CSU/"
mxd = arcpy.mapping.MapDocument("current")
df = arcpy.mapping.ListDataFrames(mxd, "*")[0]

try:
    inFeatureClass = "d:/CSU/poi_yuelu.shp"  
    print inFeatureClass
    newLayer = arcpy.mapping.Layer(inFeatureClass)
    print newLayer
    arcpy.mapping.AddLayer(df, newLayer, "AUTO_ARRANGE")
    print "图层添加成功！"   
except Exception as e:
    print "执行错误：" + str(e)