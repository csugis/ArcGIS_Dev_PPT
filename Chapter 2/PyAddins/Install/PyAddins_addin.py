import arcpy
import pythonaddins

class ButtonAddLayer(object):
    """Implementation for PyAddins_addin.button (Button)"""
    def __init__(self):
        self.enabled = True
        self.checked = False
    def onClick(self):
         # Add a layer
        arcpy.env.workspace = "d:/csu";
        mxd = arcpy.mapping.MapDocument("current");
        df  = arcpy.mapping.ListDataFrames(mxd,"*")[0];
        newLayer = arcpy.mapping.Layer("d:/csu/jmd.shp");
        arcpy.mapping.AddLayer(df,newLayer,"AUTO_ARRANGE");
        pass

class ButtonFull(object):
    """Implementation for PyAddins_addin.button_1 (Button)"""
    def __init__(self):
        self.enabled = True
        self.checked = False
    def onClick(self):
        mxd = arcpy.mapping.MapDocument( "current" );
        df  = arcpy.mapping.ListDataFrames(mxd,"*")[0];
        lyr  = arcpy.mapping.ListLayers(mxd, "*", df)[0];
        df.extent = lyr.getExtent();
        pass

class ButtonNewFeature(object):
    """Implementation for PyAddins_addin.button_2 (Button)"""
    def __init__(self):
        self.enabled = True
        self.checked = False
    def onClick(self):
        f_cls = "d:/CSU/points.shp";
        if arcpy.Exists(f_cls):
            arcpy.Delete_management(f_cls);
        arcpy.CreateFeatureclass_management("d:/CSU","points.shp","POINT");
        arcpy.AddField_management(f_cls,"name","TEXT",20);
        pass

class ToolDrawPts(object):
    """Implementation for PyAddins_addin.tool_4 (Tool)"""
    def __init__(self):
        self.enabled = True
        self.cursor = 3
        self.checked = False
        self.drawing = True
        self.shape = "None" # Can set to "Line", "Circle" or "Rectangle" for interactive shape drawing and to activate the onLine/Polygon/Circle event sinks.
    def onMouseDown(self, x, y, button, shift):
        pass
    def onMouseDownMap(self, x, y, button, shift):
        if self.drawing == True:
            f_cls = "d:/CSU/points.shp";
            with arcpy.da.InsertCursor(f_cls,['SHAPE@XY']) as dc:
                dc.insertRow([(x,y)]);
                print "One point ({0},{1}) has been added.".format(x,y);
            del dc;
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

class ToolCreatePts(object):
    """Implementation for PyAddins_addin.tool_1 (Tool)"""
    def __init__(self):
        self.enabled = True
        self.shape = "Rectangle" # Can set to "Line", "Circle" or "Rectangle" for interactive shape drawing and to activate the onLine/Polygon/Circle event sinks.
    def onMouseDown(self, x, y, button, shift):
        pass
    def onMouseDownMap(self, x, y, button, shift):
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
        arcpy.env.workspace = "d:/CSU/" ;
        arcpy.env.overwriteOutput = True;
        ext = rectangle_geometry;
        if arcpy.Exists( "points" ):
            arcpy.Delete_management( "points" );
        arcpy.CreateRandomPoints_management( "d:/CSU","points.shp" , "" ,ext,200);
        arcpy.RefreshTOC();
        pass

class ToolDrawPts(object):
    """Implementation for PyAddins_addin.tool_4 (Tool)"""
    def __init__(self):
        self.enabled = True
        self.cursor = 3
        self.checked = False
        self.drawing = True
        self.shape = "None" # Can set to "Line", "Circle" or "Rectangle" for interactive shape drawing and to activate the onLine/Polygon/Circle event sinks.
    def onMouseDown(self, x, y, button, shift):
        pass
    def onMouseDownMap(self, x, y, button, shift):
        if self.drawing == True:
            f_cls = "d:/CSU/points.shp";
            with arcpy.da.InsertCursor(f_cls,['SHAPE@XY']) as dc:
                dc.insertRow([(x,y)]);
                print "One point ({0},{1}) has been added.".format(x,y);
            del dc;
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

class ToolIdentifyPts(object):
    """Implementation for PyAddins_addin.tool_5 (Tool)"""
    def __init__(self):
        self.enabled = True
        self.cursor = 3
        self.shape = "None" # Can set to "Line", "Circle" or "Rectangle" for interactive shape drawing and to activate the onLine/Polygon/Circle event sinks.
    def onMouseDown(self, x, y, button, shift):
        pass
    def onMouseDownMap(self, x, y, button, shift):
        mxd = arcpy.mapping.MapDocument("current");
        df = mxd.activeDataFrame;
        lyr = arcpy.mapping.ListLayers("points","*",df)[0];
        pt = arcpy.Point(x,y);
        geom = arcpy.PointGeometry(pt)    
        arcpy.SelectLayerByLocation_management( "points","WITHIN_A_DISTANCE" ,geom,2.5);
        matchcount = arcpy.GetCount_management("points").getOutput(0);
        print "The matched points count: "+matchcount;
        with arcpy.da.SearchCursor(lyr,["OID@","SHAPE@WKT"]) as cursor:
            for row in cursor:
                if str(row[0]) in arcpy.Describe("points").FIDSet:
                    print u"FID:{0},SHAPE:{1}".format(row[0],row[1]);
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

class ToolSelect(object):
    """Implementation for PyAddins_addin.tool_2 (Tool)"""
    def __init__(self):
        self.enabled = True
        self.shape = "Line" # Can set to "Line", "Circle" or "Rectangle" for interactive shape drawing and to activate the onLine/Polygon/Circle event sinks.
    def onMouseDown(self, x, y, button, shift):
        pass
    def onMouseDownMap(self, x, y, button, shift):
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
        arcpy.env.workspace = "d:/CSU/" ;
        newLayer = arcpy.MakeFeatureLayer_management( "points",'lyr' );
        arcpy.SelectLayerByLocation_management( "lyr","WITHIN_A_DISTANCE" ,line_geometry,1.0);
        matchcount = arcpy.GetCount_management( "lyr" ).getOutput(0);
        print "The matched points count: " +matchcount;
        pass
    def onRectangle(self, rectangle_geometry):
        pass


def extent_to_polygon(extent):
    """    
    Args:
        extent (arcpy.Extent)        
    Returns:
        arcpy.Polygon
    """
    array = arcpy.Array()
    array.add(extent.lowerLeft)
    array.add(extent.lowerRight)
    array.add(extent.upperRight)
    array.add(extent.upperLeft)
    array.add(extent.lowerLeft)
    return arcpy.Polygon(array)

class ToolSelectRect(object):
    """Implementation for PyAddins_addin.tool_3 (Tool)"""
    def __init__(self):
        self.enabled = True
        self.shape = "Rectangle" # Can set to "Line", "Circle" or "Rectangle" for interactive shape drawing and to activate the onLine/Polygon/Circle event sinks.
    def onMouseDown(self, x, y, button, shift):
        pass
    def onMouseDownMap(self, x, y, button, shift):
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
        arcpy.env.workspace = "d:/CSU/" ;
        print str(rectangle_geometry);
        layer = arcpy.MakeFeatureLayer_management("points",'lyr');
        pg = extent_to_polygon(rectangle_geometry);
        arcpy.SelectLayerByLocation_management("lyr","INTERSECT",pg);
        matchcount = arcpy.GetCount_management("lyr").getOutput(0);
        print "The matched points count: "+matchcount;
        with arcpy.da.SearchCursor(layer,["OID@","SHAPE@WKT"]) as cursor:
            for row in cursor:
                if str(row[0]) in arcpy.Describe("lyr").FIDSet:
                    print u"FID:{0},SHAPE:{1}".format(row[0],row[1]);        
        pass

class ToolZoomIn(object):
    """Implementation for PyAddins_addin.tool (Tool)"""
    def __init__(self):
        self.enabled = True
        self.shape = "Rectangle" # Can set to "Line", "Circle" or "Rectangle" for interactive shape drawing and to activate the onLine/Polygon/Circle event sinks.
    def onMouseDown(self, x, y, button, shift):
        pass
    def onMouseDownMap(self, x, y, button, shift):
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
        mxd = arcpy.mapping.MapDocument( "current"  );
        df  = arcpy.mapping.ListDataFrames(mxd,"*")[0];
        df.extent = rectangle_geometry;        
        pass