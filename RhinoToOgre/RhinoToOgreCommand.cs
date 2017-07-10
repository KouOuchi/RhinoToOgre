using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input.Custom;
using Rhino.DocObjects;
using Rhino.UI;
using System.Runtime.InteropServices;

namespace RhinoToOgre
{
    [Guid("9F594EDB-D82C-4169-908F-968A3601C8B5")]
    public class RhinoToOgreCommand : Command
    {
        public RhinoToOgreCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static RhinoToOgreCommand Instance
        {
            get; private set;
        }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName
        {
            get { return "RhinoToOgre"; }
        }
        const Rhino.DocObjects.ObjectType geometryFilter = ObjectType.Brep |
                                                            ObjectType.Mesh;

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            
            List<Rhino.Geometry.Mesh> meshes = new List<Rhino.Geometry.Mesh>();
            try
            {
                string ogre_bin = System.Environment.GetEnvironmentVariable("OGRE_HOME");
#if DEBUG
                ogre_bin += @"\bin\Debug";
#else
                ogre_bin += @"\bin\Release";
#endif

                string path = System.Environment.GetEnvironmentVariable("PATH");
                System.Environment.SetEnvironmentVariable("PATH", path + ";" + ogre_bin,
                    EnvironmentVariableTarget.Process);
                
                Rhino.Input.Custom.GetObject go = new Rhino.Input.Custom.GetObject();
                
                OptionToggle export_as_xml = new OptionToggle(false, "No", "Yes");
                go.AddOptionToggle("export_as_xml", ref export_as_xml);

                go.SetCommandPrompt("Select surfaces, polysurfaces, or meshes");
                go.GeometryFilter = geometryFilter;
                go.GroupSelect = true;
                go.SubObjectSelect = false;
                go.EnableClearObjectsOnEntry(false);
                go.EnableUnselectObjectsOnExit(false);
                go.DeselectAllBeforePostSelect = false;

                bool bHavePreselectedObjects = false;

                for (; ; )
                {
                    Rhino.Input.GetResult res = go.GetMultiple(1, 0);

                    if (res == Rhino.Input.GetResult.Option)
                    {
                        go.EnablePreSelect(false, true);
                        continue;
                    }

                    else if (res != Rhino.Input.GetResult.Object)
                    {
                        RhinoLogger.Info("Canceled.");
                        return Rhino.Commands.Result.Cancel;
                    }
                    if (go.ObjectsWerePreselected)
                    {
                        bHavePreselectedObjects = true;
                        go.EnablePreSelect(false, true);
                        continue;
                    }

                    break;
                }

                SaveFileDialog sv = new SaveFileDialog();

                if (!export_as_xml.CurrentValue)
                {
                    sv.Filter = "Mesh files (*.mesh)|*.mesh";
                    sv.DefaultExt = "mesh";
                }
                else
                {
                    sv.Filter = "XML files (*.xml)|*.xml";
                    sv.DefaultExt = "xml";
                }

                sv.Title = "Select File to Export :";
                if (sv.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    RhinoLogger.Info("Canceled.");
                    return Result.Cancel;
                }

                if (bHavePreselectedObjects)
                {
                    // Normally when command finishes, pre-selected objects will remain
                    // selected, when and post-selected objects will be unselected.
                    // With this sample, it is possible to have a combination of 
                    // pre-selected and post-selected objects. To make sure everything
                    // "looks the same", unselect everything before finishing the command.
                    for (int i = 0; i < go.ObjectCount; i++)
                    {
                        Rhino.DocObjects.RhinoObject rhinoObject = go.Object(i).Object();
                        if (null != rhinoObject)
                            rhinoObject.Select(false);
                    }
                    doc.Views.Redraw();
                }

                int objectCount = go.ObjectCount;

                Rhino.RhinoApp.WriteLine("Select object count = {0}", objectCount);

                for(int i = 0;i<objectCount;i++)
                {
                    var objref = go.Object(i);
                    if (objref.Geometry().ObjectType == Rhino.DocObjects.ObjectType.Mesh)
                    {
                        meshes.Add(objref.Mesh());
                    }
                    else if(objref.Geometry().ObjectType == Rhino.DocObjects.ObjectType.Brep)
                    {
                        var ms = CheckOrCreateMesh(objref.Brep(), RhinoDoc.ActiveDoc.GetMeshingParameters(MeshingParameterStyle.Custom));
                        meshes.AddRange(ms);
                    }
                    else
                    {
                        RhinoLogger.ErrorFormat("selection is unexpected ObjectType : {0}", objref.Geometry().ObjectType);
                        return Result.Failure;
                    }
                }


                var exporter = new ExportToOgreMesh();
                var exp_path = exporter.Export(sv.FileName, meshes);

                if (!export_as_xml.CurrentValue)
                {
                    exporter.ConvertXmlToMesh(exp_path);
                }
            }
            catch (Exception e)
            {
                RhinoLogger.Fatal(e);
                return Result.Failure;
            }

            return Result.Success;
        }


        private List<Rhino.Geometry.Mesh> CheckOrCreateMesh(Brep brep, MeshingParameters mp)
        {
            List<Rhino.Geometry.Mesh> ret = null;
            if (brep != null)
            {
                ret = new List<Rhino.Geometry.Mesh>(
                    Rhino.Geometry.Mesh.CreateFromBrep(brep, mp));
            }
                
            // no mesh/brep face found
            return ret;
        }
    }
}
