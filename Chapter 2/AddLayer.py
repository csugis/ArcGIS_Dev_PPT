# �ű�����Ӿ���㵽��ǰ��ͼ�ĵ�
import arcpy
arcpy.env.workspace = "d:/csu";
mxd = arcpy.mapping.MapDocument("current");
df  = arcpy.mapping.ListDataFrames(mxd,"*")[0];
newLayer = arcpy.mapping.Layer("d:/csu/jmd.shp");
arcpy.mapping.AddLayer(df,newLayer,"AUTO_ARRANGE");
