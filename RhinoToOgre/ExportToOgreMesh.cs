using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace RhinoToOgre
{
    public class ExportToOgreMesh
    {
        public string Export(string path, List<Rhino.Geometry.Brep> bs, Rhino.Geometry.MeshingParameters mp)
        {
            List<Rhino.Geometry.Mesh> meshes = new List<Rhino.Geometry.Mesh>();
            bs.ForEach(p => meshes.AddRange(Rhino.Geometry.Mesh.CreateFromBrep(p, mp)));

            return Export(path, meshes);
        }
        public string Export(string path, List<Rhino.Geometry.Mesh> meshes)
        {
            XmlOgreMesh exp_mesh = new XmlOgreMesh();
            FileInfo f = new FileInfo(path);
            string exp_path = string.Format(@"{0}\{1}{2}", f.DirectoryName, Path.GetFileNameWithoutExtension(f.Name), ".xml");
            string exp_mesh_name = Path.GetFileNameWithoutExtension(f.Name);

            // integrate rhino meshs into 1 submesh 
            XmlSubMesh1 submesh = new XmlSubMesh1();
            submesh.material = exp_mesh_name;

            for (int m = 0; m < meshes.Count; m++)
            {
                var mesh = meshes[m];
                var previous_mesh_vertex_counter = exp_mesh.sharedgeometry.vertexcount;

                // build vertex buffer
                for (int i = 0; i < mesh.Vertices.Count; ++i)
                {
                    // add vertex buffer
                    var v = new XmlVertex(mesh.Vertices[i], mesh.Normals[i]);
                    exp_mesh.sharedgeometry.vertexbuffer.vertexes.Add(v);
                    exp_mesh.sharedgeometry.vertexcount++;
                }

                for (int i = 0; i < mesh.Faces.Count; ++i)
                {
                    var meshface = mesh.Faces[i];

                    if (meshface.IsTriangle || meshface.IsQuad)
                    {
                        var tri = new XmlFace()
                        {
                            v1 = meshface.A + previous_mesh_vertex_counter,
                            v2 = meshface.B + previous_mesh_vertex_counter,
                            v3 = meshface.C + previous_mesh_vertex_counter,
                        };
                        submesh.faces.faces.Add(tri);
                        submesh.faces.count++;
                    }

                    if (meshface.IsQuad)
                    {
                        var tri = new XmlFace()
                        {
                            v1 = meshface.A + previous_mesh_vertex_counter,
                            v2 = meshface.C + previous_mesh_vertex_counter,
                            v3 = meshface.D + previous_mesh_vertex_counter,
                        };
                        submesh.faces.faces.Add(tri);
                        submesh.faces.count++;
                    }
                }
            }
            exp_mesh.submeshes.submeshes.Add(submesh);

            // build submesh names
            XmlSubMesh2 submesh2 = new XmlSubMesh2();
            submesh2.index = 0;
            submesh2.name = string.Format("{0}.0", exp_mesh_name);
            exp_mesh.submeshnames.submeshes.Add(submesh2);

            new XmlReaderWriter<XmlOgreMesh>().WriteXml(exp_mesh, exp_path, Encoding.GetEncoding("UTF-8"));
            return exp_path;
        }

        public void ConvertXmlToMesh(string path)
        {
            try
            {
                // From Ogre3D standard CMake, suffix of output is "_d" in DEBUG
#if DEBUG
                string launcher = string.Format("\"{0}\"", "ogremeshtool_d.exe");
#else
                string launcher = string.Format("\"{0}\"", "ogremeshtool.exe");
#endif
                var meshbin_path = Regex.Replace(path, @"\.xml$", ".mesh", RegexOptions.IgnoreCase);

                string arg = string.Format("-v2 -ts 4 -d3d \"{0}\" \"{1}\"", path, meshbin_path);
                string workdir = new FileInfo(path).DirectoryName;

                RhinoLogger.InfoFormat("ogremeshexporter: path={0}, arg={1}, workdir={2}", launcher, arg, workdir);

                ProcessStartInfo processStart = new ProcessStartInfo(launcher, arg);
                processStart.CreateNoWindow = false;
                processStart.WorkingDirectory = workdir;
                processStart.UseShellExecute = false;
                
                var process = new Process();
                process.StartInfo = processStart;
                process.Start();
                //p.WaitForExit();
                RhinoLogger.Info("process spawned.");

                Thread.Sleep(1000);
                while (!process.HasExited)
                {
                    Thread.Sleep(500);
                }

            }
            catch (Exception ex)
            {
                RhinoLogger.Fatal(ex);
            }
        }
    }
}
