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
        arcpy.SelectLayerByLocation_management( "lyr","WITHIN_A_DISTANCE" ,line_geometry,1.00);
        matchcount = arcpy.GetCount_management( "lyr" ).getOutput(0);
        print "The matched points count: " +matchcount;
        pass
    def onRectangle(self, rectangle_geometry):
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