using System.Collections.Generic;
using System.Xml.Serialization;

namespace RhinoToOgre
{
    /// <summary>
    /// Ogre mesh xml
    /// </summary>
    [XmlRoot(ElementName="mesh")]
    public class XmlOgreMesh
    {
        [XmlElement(ElementName="sharedgeometry")]
        public XmlSharedGeometry sharedgeometry = new XmlSharedGeometry();

        [XmlElement(ElementName="submeshes")]
        public XmlSubMeshes submeshes = new XmlSubMeshes();

        [XmlElement(ElementName="submeshnames")]
        public XmlSubMeshNames submeshnames = new XmlSubMeshNames();
    }

    [XmlRoot(ElementName="shardgeometry")]
    public class XmlVertexBuffer
    {
        [XmlAttribute]
        public bool colours_diffuse = false;
        [XmlAttribute]
        public bool positions = true;
        [XmlAttribute]
        public int texture_coords = 0;
        [XmlAttribute]
        public bool normals = true;

        [XmlElement("vertex")]
        public List<XmlVertex> vertexes = new List<XmlVertex>();
    }

    [XmlRoot]
    public class XmlSharedGeometry
    {
        [XmlAttribute]
        public int vertexcount;
        [XmlElement]
        public XmlVertexBuffer vertexbuffer = new XmlVertexBuffer();
    }

    public class XmlVertex
    {
        public XmlVertex()
        { }
        public XmlVertex(Rhino.Geometry.Point3f v, Rhino.Geometry.Vector3f n)
        {
            position = new XmlPosition() 
            { 
                x = v.X,
                y = v.Y,
                z = v.Z
            };
            normal = new XmlNormal()
            { 
                x = n.X,
                y = n.Y,
                z = n.Z
            };
        }
        [XmlElement]
        public XmlPosition position = new XmlPosition();
        [XmlElement]
        public XmlNormal normal = new XmlNormal();
    }

    public class XmlPosition
    {
        [XmlAttribute]
        public double x;
        [XmlAttribute]
        public double y;
        [XmlAttribute]
        public double z;
    }

    public class XmlNormal
    {
        [XmlAttribute]
        public double x;
        [XmlAttribute]
        public double y;
        [XmlAttribute]
        public double z;
    }

    public class XmlSubMeshes
    {
        [XmlElement("submesh")]
        public List<XmlSubMesh1> submeshes = new List<XmlSubMesh1>();
    }

    public class XmlSubMesh1
    {
        [XmlAttribute]
        public bool usesharedvertices=true;
        [XmlAttribute]
        public bool use32bitindexes=false;
        [XmlAttribute]
        public string material="";
        [XmlAttribute]
        public string operationtype = "triangle_list";

        public XmlFaces faces = new XmlFaces();
    }

    public class XmlFaces
    {
        [XmlAttribute]
        public int count;

        [XmlElement("face")]
        public List<XmlFace> faces = new List<XmlFace>();
    }

    public class XmlFace
    {
        [XmlAttribute]
        public int v1;
        [XmlAttribute]
        public int v3;
        [XmlAttribute]
        public int v2;
    }

    public class XmlSubMeshNames
    {
        [XmlElement("submesh")]
        public List<XmlSubMesh2> submeshes = new List<XmlSubMesh2>();
    }

    public class XmlSubMesh2
    {
        [XmlAttribute]
        public string name = "";
        [XmlAttribute]
        public int index = 0;
    }
}

