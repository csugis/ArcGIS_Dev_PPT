# -------------------------------------------------------------------------------
# 功能：使用InsertCursor函数，在指定范围内随机生成10个点，存入points.shp，再将这些点按序生成线，存入lines.shp文件
# 作者：CSUGIS
# 日期：2026年3月1日
# 说明：本脚本使用ArcPy库操作地理数据，需要在ArcGIS环境中运行
# -------------------------------------------------------------------------------

# 导入必要的库
import arcpy  # ArcPy库，用于地理数据处理
import random  # 随机数生成库

# 设置工作空间
arcpy.env.workspace = "d:/CSU/"  # 工作空间路径

# 定义输出文件路径
fp_class = "d:/CSU/points.shp"  # 点要素类路径
fl_class = "d:/CSU/lines.shp"  # 线要素类路径

# 检查文件是否存在，如果存在则删除
if arcpy.Exists(fp_class):  # 检查点要素类是否存在
    arcpy.Delete_management(fp_class)  # 删除已存在的点要素类
if arcpy.Exists(fl_class):  # 检查线要素类是否存在
    arcpy.Delete_management(fl_class)  # 删除已存在的线要素类

# 创建新的要素类
arcpy.CreateFeatureclass_management("d:/CSU/", "points.shp", "POINT")  # 创建点要素类
arcpy.CreateFeatureclass_management("d:/CSU/", "lines.shp", "POLYLINE")  # 创建线要素类

# 创建点要素的插入游标
# ['SHAPE@XY']表示存储点的坐标信息
cursor = arcpy.da.InsertCursor(fp_class, ['SHAPE@XY'])

# 创建一个空列表，用于存储生成的点对象
arr = []

# 循环生成10个随机点
for i in range(0, 10):
    # 生成随机x坐标：在12571510.0到12571590.0之间（80单位范围）
    x = random.random() * 80 + 12571510.0
    # 生成随机y坐标：在3270040.0到3270120.0之间（80单位范围）
    y = random.random() * 80 + 3270040.0
    # 插入点到点要素类中
    cursor.insertRow([(x, y)])
    # 将点对象添加到列表中，用于后续创建线
    arr.append(arcpy.Point(x, y))

# 删除游标，释放资源
del cursor

# 创建线要素的插入游标
# ['SHAPE@']表示存储几何对象
cursor = arcpy.da.InsertCursor(fl_class, ['SHAPE@'])

# 将点列表转换为ArcPy数组
arr = arcpy.Array(arr)

# 使用点数组创建线对象
polyline = arcpy.Polyline(arr)

# 插入线到线要素类中
cursor.insertRow([polyline])

# 删除游标，释放资源
del cursor