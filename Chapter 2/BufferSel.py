# 创建用户地理处理工具, 利用缓冲区选择
import arcpy, os
arcpy.env.workspace = "D:/CSU";
road = arcpy.GetParameterAsText(0);
roadBuf = arcpy.GetParameterAsText(1);
jmd = arcpy.GetParameterAsText(2);
arcpy.Buffer_analysis(road,roadBuf,500,'FULL','ROUND','ALL');
arcpy.SelectLayerByLocation_management(jmd,'intersect',roadBuf);
