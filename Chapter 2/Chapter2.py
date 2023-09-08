# ����1��ʹ��forѭ��
total_sum = 0
for i in range(1, 101):
    total_sum += i

print("�ܺ�Ϊ:", total_sum)


# ����2��ʹ��whileѭ��
total_sum = 0
i = 1
while i <= 100:
    total_sum += i
    i += 1

print("�ܺ�Ϊ:", total_sum)

# ArcPy��������Ҫ������в���
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


# ArcPy�ࣺ��ʾ��ͼͶӰ��Ϣ
import arcpy
prjFile = "D:/CSU/3857.prj"
spatialRef = arcpy.SpatialReference(prjFile)
print(spatialRef.name)
print(spatialRef.type)

# ArcPy�ࣺ��
import arcpy 
pointA = arcpy.Point(2.0, 4.5)
print pointA
pointB = arcpy.Point(3.0, 7.0)
print pointB

# ArcPy���й���
arcpy.env.workspace = "D:/CSU"
result = arcpy.Buffer_analysis("road.shp", "roadBuf", "500 METERS")
print result

