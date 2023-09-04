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
