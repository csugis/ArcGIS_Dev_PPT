# 使用InsertCursor函数，在(0,0)到(80,80)范围内随机生成10个点，存入points.shp，再将这些点按序生成线，存入line.shp文件
import arcpy
import random
arcpy.env.workspace = "d:/CSU/"
fp_class = "d:/CSU/points.shp";
fl_class = "d:/CSU/lines.shp";
if arcpy.Exists(fp_class):
    arcpy.Delete_management(fp_class);
if arcpy.Exists(fl_class):
    arcpy.Delete_management(fl_class);
arcpy.CreateFeatureclass_management("d:/CSU/", "points.shp","POINT");
arcpy.CreateFeatureclass_management("d:/CSU/", "lines.shp","POLYLINE");
cursor=arcpy.da.InsertCursor(fp_class,['SHAPE@XY']);
arr =[];
for i in range(0,10):
    x = random.random()*80 + 12571510.0;
    y = random.random()*80 + 3270040.0;
    cursor.insertRow([(x,y)]);
    arr.append(arcpy.Point(x,y));
del cursor
cursor = arcpy.da.InsertCursor(fl_class,['SHAPE@']);
arr = arcpy.Array(arr);
polyline = arcpy.Polyline(arr);
cursor.insertRow([polyline]);
del cursor;