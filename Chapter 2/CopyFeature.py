import os 
import arcpy 
arcpy.env.workspace = arcpy.GetParameterAsText(0) 
in_featureclass = arcpy.GetParameterAsText(1) 
out_workspace = arcpy.GetParameterAsText(2) 
out_featureclass = os.path.join(out_workspace, os.path.basename(in_featureclass));
print out_featureclass; 
arcpy.CopyFeatures_management(in_featureclass, out_featureclass);
