using DesignPatterns.Creational.Factory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DesignPatterns.Creational.ProtoType
{
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            MemoryStream memoryStream= new MemoryStream();
            BinaryFormatter binaryFormatter= new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, self);
            memoryStream.Seek(0, SeekOrigin.Begin);
            object copy = binaryFormatter.Deserialize(memoryStream);
            memoryStream.Close();
            return (T)copy;
        }

        public static T DeepcopyXML<T>(this T self)
        {
            using(var ms = new MemoryStream())
            {
                XmlSerializer s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T) s.Deserialize(ms);
                
            }
        }
    }

    [Serializable]
    public class Foo
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}:{Name}, {nameof(Description)}: {Description}";
        }
    }

    internal class PrototypeSerialization
    {
        static void Main()
        {
            Foo fu = new Foo { Name = "42", Description = "abc" };
            Foo foo2 = fu.DeepcopyXML();

            foo2.Description = "xyz";
            Console.WriteLine(fu);
            Console.WriteLine(foo2);
        }
    }
}
