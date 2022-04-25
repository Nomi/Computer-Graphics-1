using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Computer_Graphics_1.Lab3
{
    public static class SerializationEx
    {
        public static void SerializeToXML(this List<Shape> shapes, string filepath)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Shape>));
            using (var writer = new System.IO.StreamWriter(filepath))
            {
                serializer.Serialize(writer, shapes);
            }
        }

        public static void Deserialize(this List<Shape>shapes, string filepath)
        {
            List<Shape> newShapes = new List<Shape>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Shape>));

            using (Stream reader = new FileStream(filepath, FileMode.Open))
            {
                newShapes = (List<Shape>)serializer.Deserialize(reader);
            }

            shapes.Clear();
            shapes.AddRange(newShapes);
        }
    }
}
