using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace RhinoToOgre
{
    public class XmlReaderWriter<T>
    {
        /// <summary>
        /// if filename does not exists, exception throws
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public T ReadXml(string filename)
        {
            // guess encoding by extention
            Encoding enc = Encoding.GetEncoding("UTF-8");
            using (TextReader reader = new StreamReader(filename, enc))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// if filename does not exists with overwrite mode, exception throws
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public void WriteXml(T obj, string filename)
        {
            using (TextWriter writer = new StreamWriter(filename, false, Encoding.GetEncoding("Shift-JIS")))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, obj);
            }
        }

        public void WriteXml(T obj, string filename, Encoding enc)
        {
            using (TextWriter writer = new StreamWriter(filename, false, enc))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, obj);
            }
        }
    }
}