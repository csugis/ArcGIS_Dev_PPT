import arcpy
import pythonaddins

class ButtonClassAddLayer(object):
    """Implementation for Exp1Addins_addin.buttonAddLayer (Button)"""
    def __init__(self):
        self.enabled = True
        self.checked = False
    def onClick(self):
        mxd = arcpy.mapping.MapDocument("current");
        df  = arcpy.mapping.ListDataFrames(mxd,"*")[0];
        newLayer = arcpy.mapping.Layer("d:/csu/jmd.shp");
        arcpy.mapping.AddLayer(df,newLayer,"AUTO_ARRANGE");
        pass

class ToolClassZoomIn(object):
    """Implementation for Exp1Addins_addin.toolZoomIn (Tool)"""
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