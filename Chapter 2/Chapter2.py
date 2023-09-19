# 方法1：使用for循环
total_sum = 0
for i in range(1, 101):
    total_sum += i

print("总和为:", total_sum)


# 方法2：使用while循环
total_sum = 0
i = 1
while i <= 100:
    total_sum += i
    i += 1

print("总和为:", total_sum)

# ArcPy函数：对要素类进行裁切
import arcpy
import os
arcpy.env.workspace = "D:/CSU"
out_workspace = "D:/CSU_CLIP"
clip_features = "D:/CSU_CLIP/boundary.shp"

# Loop through a list of feature classes in the workspace
for fc in arcpy.ListFeatureClasses():
    output = os.path.join(out_workspace, fc)
    result = arcpy.Clip_analysis(fc, clip_features, output, 0.1)
    print result


# ArcPy类：显示地图投影信息
import arcpy
prjFile = "D:/CSU/3857.prj"
spatialRef = arcpy.SpatialReference(prjFile)
print(spatialRef.name)
print(spatialRef.type)

# ArcPy类：点
import arcpy 
pointA = arcpy.Point(2.0, 4.5)
print pointA
pointB = arcpy.Point(3.0, 7.0)
print pointB

# ArcPy 缓冲区分析
arcpy.env.workspace = "D:/CSU"
result = arcpy.Buffer_analysis("road.shp", "roadBuf", "500 METERS")
print result


import os
mypath = "d:/CSU/CSU.mxd"
os.startfile(mypath)  #打开地图文件
# 当前地图： 
print arcpy.mapping.MapDocument("current");
# 地图文件： 
print arcpy.mapping.MapDocument("d:/CSU/CSU.mxd"); 

# 修改地图范围(缩放)
mxd = arcpy.mapping.MapDocument("current");
df = arcpy.mapping.ListDataFrames(mxd,"*")[0];
lyr  = arcpy.mapping.ListLayers(mxd, "*", df)[0];
df.extent = lyr.getExtent();

# 获取地图图层列表
mxd = arcpy.mapping.MapDocument("current");
df  = mxd.activeDataFrame;
layers = arcpy.mapping.ListLayers(mxd,'*',df);
for lyr in layers:
   print lyr.name

# 移除图层
mxd = arcpy.mapping.MapDocument("current");
df  = mxd.activeDataFrame;
lyr = arcpy.mapping.ListLayers(mxd,'road_old',df)[0];
arcpy.mapping.RemoveLayer(df,lyr);

# 统改mxd文件的工作区
mxd = arcpy.mapping.MapDocument( "current" );
mxd.replaceWorkspaces(r"d:\csu" ,                    #原工作区
                    "SHAPEFILE_WORKSPACE" ,     #原工作区类型
                    r"D:\CSU_CLIP" ,                  #新工作区
                    "SHAPEFILE_WORKSPACE" ,     #新工作区类型
                    True);
mxd.save();

# 逐层修改数据源
mxd = arcpy.mapping.MapDocument( "current" );
df = mxd.activeDataFrame;
layers = arcpy.mapping.ListLayers(mxd,'*',df);
#获取所有缺失数据源的图层列表
brknList = arcpy.mapping.ListBrokenDataSources(mxd);
for lyr in layers:
    if lyr.name in [brn.name for brn in brknList]: 
        lyr.replaceDataSource(r"d:\csu" ,                 #新数据源
                            "SHAPEFILE_WORKSPACE",   #新数据源工作区类型
                            lyr.name);                             #新数据源名称                             
mxd.save();

# 打印地图
mxd = arcpy.mapping.MapDocument( "current" );
df = mxd.activeDataFrame;
arcpy.mapping.PrintMap(mxd,"",df);

# 输出为PDF
mxd = arcpy.mapping.MapDocument( "current" );
df = mxd.activeDataFrame;
arcpy.mapping.ExportToPDF(mxd,"d:/CSU/csu.pdf");

# 列出指定目录下的SHP文件列表
import arcpy
from arcpy import env
env.workspace = "d:/csu" ;
fcList = arcpy.ListFeatureClasses();
print fcList;

# 判断shp文件是否存在示例
arcpy.env.workspace = "d:/csu" ;
if arcpy.Exists( "road.shp" ):
    lyr = arcpy.mapping.Layer( "road.shp" );
    arcpy.mapping.AddLayer(df,lyr, "AUTO_ARRANGE" );
else:
    print "The road feature class not exists";

# 要素类描述
import arcpy
import sys
reload(sys);
sys.setdefaultencoding('utf-8');
desc = arcpy.Describe("d:/csu/road.shp");
print "要素类型: " + desc.featureType +"\n几何类型: " + desc.shapeType+ "\n有索引:" + str(desc.hasSpatialIndex);

# 打印给定目录(如d:\csu)下所有shapefile的名称、要素类型和几何类型。
import arcpy
arcpy.env.workspace = "d:/csu" ;
shps = arcpy.ListFeatureClasses();
for shp in shps:
    desc = arcpy.Describe(shp);
    print "文件: \t" +shp+ "\n要素类型:\t" +desc.featureType+" \n几何类型:\t"+desc.shapeType;

# 字段组描述
import arcpy 
fields = arcpy.ListFields( "d:/csu/road.shp" );
for field in fields:
    print( "字段{0}: 类型{1},长度{2}" .format(field.name, field.type, field.length));

# 打印给定目录(如d:\csu)下所有shapefile的名称及其字段名称、类型。如果字段为文本类型，则显示字段长度。
import arcpy
arcpy.env.workspace = "d:/csu " ;
shps = arcpy.ListFeatureClasses();
for shp in shps:
    fields = arcpy.ListFields(shp);
    print "\n{0}的字段列表:" .format(shp);
    for field in fields:
        str = "字段[{0}]\t{1}\t" .format(field.name,field.type);
        if field.type == 'String':
            str += "({0})".format(field.length);
        print str;

# 创建要素类
import arcpy  
arcpy.env.workspace = "d:/csu" ;
out_path = arcpy.env.workspace ;
out_name = "lines.shp" ;
geometry_type = "POLYLINE" ;
template = "d:/csu/scope.shp" ;
has_m = "DISABLED" ;
has_z = "DISABLED" ;
f_class = out_path + "/" + out_name;
spatial_ref = arcpy.Describe( "d:/csu/scope.shp" ).spatialReference ; 
if arcpy.Exists(f_class):
    arcpy.Delete_management(f_class);
arcpy.CreateFeatureclass_management(out_path, out_name, geometry_type,template, has_m, has_z, spatial_ref);

# 删除要素类
f_class = "d:/csu/habitatareas.shp"
if arcpy.Exists(f_class):
    arcpy.Delete_management(f_class);

# 添加字段
import arcpy
f_class = "d:/csu/points.shp";
arcpy.AddField_management(f_class , "name" , "STRING" ,20);
arcpy.management.AddField(f_class , "name" , "STRING" ,20);

#添加多个字段,查无此函数
arcpy.management.AddFields(
    'habitatareas.shp' , 
    [
     ['school_name', 'TEXT', 'Name', 255, 'Hello world', ''], 
     ['street_number', 'LONG', 'Street Number', None, 35, 'StreetNumDomain'],
     ['year_start', 'DATE', 'Year Start', None, '2017-08-09 16:05:07', '']
    ]);

# 删除字段
f_class = "d:/csu/habitatareas.shp";
arcpy.management.DeleteField(f_class,"name");

# 编辑字段-测试失败
f_class = "d:/csu/habitatareas.shp";
arcpy.management.AlterField("habitatareas.shp", "name", "newname");

# 添加点要素
import arcpy
f_class = "d:/csu/points.shp";
row_values = [('Anderson', (12571516.0, 3270041.0)),
                       ('Andrews', (12571515.0, 3270045.0))]
cursor = arcpy.da.InsertCursor( f_class , ['NAME', 'SHAPE@XY'])
for row in row_values:
    cursor.insertRow(row)
del cursor
# 采用WKT的POINT,添加点要素
f_class = "d:/csu/points.shp";
cur = arcpy.da.InsertCursor(f_class,( "SHAPE@WKT" ,"name"));
cur.insertRow([ "POINT(12571510.0 3270040.0)" , "test_wkt" ]);
del cur


# 新建要素类，添加多边形要素
import arcpy
f_class = "d:/csu/habitatareas.shp";
if arcpy.Exists(f_class):
    arcpy.Delete_management(f_class);
feature_info = [[[12571516.0, 3270041.0], [12571510.0, 3270040.0], [12571515.0, 3270045.0]],
                [[12571516.0, 3270041.0], [12571515.0, 3270041.0], [12571515.0, 3270045.0] , [12571516.0, 3270045.0]]]
features = []
for feature in feature_info:
    array = arcpy.Array([arcpy.Point(*coords) for coords in feature])
    array.append(array[0])
    features.append(arcpy.Polygon(array))
arcpy.CopyFeatures_management(features, f_class);

# 新建线要素
f_class = "d:/csu/lines.shp";
array = arcpy.Array([arcpy.Point(12571516.0, 3270041.0),
                     arcpy.Point(12571510.0, 3270040.0),
                     arcpy.Point(12571515.0, 3270045.0)])
polyline = arcpy.Polyline(array)
cursor = arcpy.da.InsertCursor(f_class, [ 'SHAPE@' ])
cursor.insertRow([polyline])

# 编辑要素
f_class = "d:/csu/points.shp";
with arcpy.da.UpdateCursor(f_class, "name") as cur:
    for row in cur:
        row[0]="point_"+row[0];
        cur.updateRow(row);
del cur

# 获取ID值，编辑要素
f_class = "d:/csu/points.shp";
with arcpy.da.UpdateCursor(f_class,["name","FID"]) as cur:
    for row in cur:
        row[0] = "point_"+str(row[1]);
        cur.updateRow(row);
del cur;

# 删除要素
f_class = "d:/csu/points.shp";
with arcpy.da.UpdateCursor(f_class, "OID@") as cur:
    for row in cur:
        print row[0];
        if row[0]==1:
            cur.deleteRow();
del cur

# 根据属性条件删除要素
f_class = "d:/csu/points.shp";
with arcpy.da.UpdateCursor(f_class, "OID@", where_clause="FID = 0" ) as cur:
    for row in cur:
        print row[0];
        cur.deleteRow();
del cur

# 查询要素
import arcpy
f_class = "d:/csu/points.shp";
cursor = arcpy.da.SearchCursor(f_class,["name","SHAPE@WKT"],'fid>3');
for row in cursor:
    print(u'Name:{0},WKT:{1}'.format(row[0],row[1]));
del cursor

# 根据属性查询要素
import arcpy
arcpy.env.workspace = 'D:/CSU'
# Select all pois that overlap the CSU polygon
csu_pois = arcpy.management.SelectLayerByLocation('poi_yuelu', 'INTERSECT', 
                                                        'csu_scope', 0, 
                                                        'NEW_SELECTION');
# Within selected features, further select only those pois with a 
# "catalog1" = '体育休闲服务'   
arcpy.management.SelectLayerByAttribute(csu_pois, 'SUBSET_SELECTION', 
                                        '"catalog1" = \'体育休闲服务\'');
# Write the selected features to a new feature class
arcpy.management.CopyFeatures(csu_pois, 'csu_pe_pois');

# 反距离加权插值
import arcpy
from arcpy import env  
from arcpy.sa import *
env.workspace = "D:/CSU"
f_class = "d:/csu/points.shp";
# 添加字段
arcpy.AddField_management(f_class , "Elevation" , "DOUBLE" ,20);
# 更新高程字段的值
with arcpy.da.UpdateCursor(f_class, ["FID", "Elevation"]) as cursor:
    for row in cursor:
        elevation = row[0] # 这里假设高程为FID，你可以根据实际情况设置
        row[1] = elevation
        cursor.updateRow(row)
del cursor;
outIDW = Idw(f_class, "Elevation", 0.1, 2, RadiusVariable(10, 150));
outIDW.save("D:/CSU/idwout.tif");


# 选择要素
import arcpy 
arcpy.env.workspace = 'D:/CSU'
arcpy.analysis.Select("poi_yuelu", "poi_pe_yuelu.shp", '"catalog1" = \'体育休闲服务\'')

# 分割要素
arcpy.env.workspace = 'D:/CSU/'
arcpy.analysis.Split("district", "jmd", "name","d:/CSU/")
