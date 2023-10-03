import arcpy
import pythonaddins
import random

class BtnAnalyst(object):
    """Implementation for Test2Addins_addin.button_2 (Button)"""
    def __init__(self):
        self.enabled = True
        self.checked = False
    def onClick(self):
        fv_class = "d:/csu/voronois.shp";
        fp_class = "d:/CSU/points.shp";
        cursor = arcpy.da.SearchCursor(fv_class,["SHAPE@","FID"]);
        for row in cursor:
            polygon = arcpy.Polygon(row[0].getPart(0));
            arcpy.SelectLayerByLocation_management("points","INTERSECT",polygon);
            matchcount = arcpy.GetCount_management("points").getOutput(0);
            print u"The voronoi {0} matched {1} points.".format(row[1],matchcount);
            with arcpy.da.SearchCursor(fp_class,["OID@","SHAPE@WKT","name"]) as cur:
                for r in cur:
                    fid_set = list(map(int, arcpy.Describe("points").FIDSet.split(";")))
                    if r[0] in fid_set:
                        print u"Name:{0},SHAPE:{1}".format(r[2],r[1]);
        del cursor
        pass

class BtnNewFeatureClass(object):
    """Implementation for Test2Addins_addin.button (Button)"""
    def __init__(self):
        self.enabled = True
        self.checked = False
    def onClick(self):
        arcpy.env.workspace = "d:/CSU/"
        fp_class = "d:/CSU/points.shp";
        if arcpy.Exists(fp_class):
            arcpy.Delete_management(fp_class);
        arcpy.CreateFeatureclass_management("d:/CSU/", "points.shp","POINT");
        arcpy.AddField_management(fp_class , "name" , "STRING" ,20);
        cursor=arcpy.da.InsertCursor(fp_class,['SHAPE@XY','name']);
        arr =[];
        for i in range(0,500):
            x = random.random()*8000;
            y = random.random()*8000;
            name = "Point_" + str(i);
            cursor.insertRow([(x,y),name]);
            arr.append(arcpy.Point(x,y));
        del cursor

        fs_class = "d:/CSU/stations.shp";
        if arcpy.Exists(fs_class):
            arcpy.Delete_management(fs_class);
        arcpy.CreateFeatureclass_management("d:/CSU/", "stations.shp","POINT");

        fscope_class = "d:/CSU/scope.shp";
        if arcpy.Exists(fscope_class):
            arcpy.Delete_management(fscope_class);
        arcpy.CreateFeatureclass_management("d:/CSU/", "scope.shp","POLYGON");
        array = arcpy.Array([arcpy.Point(0.0, 0.0),
                            arcpy.Point(8000.0, 0.0),
                            arcpy.Point(8000.0, 8000.0),
                            arcpy.Point(0.0, 8000.0)])
        polygon = arcpy.Polygon(array)
        cursor = arcpy.da.InsertCursor(fscope_class, ['SHAPE@'])
        cursor.insertRow([polygon])
        del cursor
        pass

class BtnPrint(object):
    """Implementation for Test2Addins_addin.button_3 (Button)"""
    def __init__(self):
        self.enabled = True
        self.checked = False
    def onClick(self):
        mxd = arcpy.mapping.MapDocument( "current" );
        df = mxd.activeDataFrame;
        arcpy.mapping.ExportToPDF(mxd,"d:/CSU/voronoi.pdf");
        pass

class BtnVoronoi(object):
    """Implementation for Test2Addins_addin.button_1 (Button)"""
    def __init__(self):
        self.enabled = True
        self.checked = False
    def onClick(self):
        fscope_class = "d:/CSU/scope.shp";
        arcpy.env.extent = fscope_class;
        fs_class = "d:/CSU/stations.shp";
        fv_class = "d:/CSU/voronois.shp";
        if arcpy.Exists(fv_class):
            arcpy.Delete_management(fv_class);
        arcpy.CreateThiessenPolygons_analysis(fs_class, fv_class, "ALL");
        pass

class ToolDrawStation(object):
    """Implementation for Test2Addins_addin.tool (Tool)"""
    def __init__(self):
        self.enabled = True
        self.shape = "NONE" # Can set to "Line", "Circle" or "Rectangle" for interactive shape drawing and to activate the onLine/Polygon/Circle event sinks.
    def onMouseDown(self, x, y, button, shift):
        pass
    def onMouseDownMap(self, x, y, button, shift):
        fs_class = "d:/CSU/stations.shp";
        cur = arcpy.da.InsertCursor(fs_class,("SHAPE@"));
        cur.insertRow([arcpy.Point(x, y)]);
        del cur
        arcpy.RefreshActiveView();
        pass
    def onMouseUp(self, x, y, button, shift):
        pass
    def onMouseUpMap(self, x, y, button, shift):
        pass
    def onMouseMove(self, x, y, button, shift):
        pass
    def onMouseMoveMap(self, x, y, button, shift):
        pass
    def onDblClick(self):
        pass
    def onKeyDown(self, keycode, shift):
        pass
    def onKeyUp(self, keycode, shift):
        pass
    def deactivate(self):
        pass
    def onCircle(self, circle_geometry):
        pass
    def onLine(self, line_geometry):
        pass
    def onRectangle(self, rectangle_geometry):
        pass