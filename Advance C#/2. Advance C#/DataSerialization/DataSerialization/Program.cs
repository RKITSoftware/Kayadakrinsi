using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace DataSerialization
{
    internal class Program
    {
        /// <summary>
        /// Serializes object to xml string
        /// </summary>
        /// <returns>Serialized string</returns>
        public string SerializeXMLObjectToString()
        {
            Pet objPet = new Pet { Name = "Sheru", Breed = "Dog" };

            var xmlSerializer = new XmlSerializer(typeof(Pet));
            using (var writer = new StringWriter())
            {
                xmlSerializer.Serialize(writer, objPet);
                var xmlString = writer.ToString();
                Console.WriteLine(xmlString);
                return xmlString;
            }
        }

        /// <summary>
        /// Deserializes xml string to object
        /// </summary>
        /// <param Name="xmlString">Serialized string</param>
        public void DeserializeXMLStringToObject(string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(Pet));
            using (var reader = new StringReader(xmlString))
            {
                var objPet = (Pet)xmlSerializer.Deserialize(reader);
                Console.WriteLine("Pet Name : " + objPet.Name + ", Pet breed : " + objPet.Breed);
            }
        }

        /// <summary>
        /// Serializes object to xml file
        /// </summary>
        public void SerializeObjectToXmlFile()
        {
            var objPet = new Pet { Name = "Kitty", Breed = "cat" };

            var xmlSerializer = new XmlSerializer(typeof(Pet));
            using (var writer = new StreamWriter(@"F:\Krinsi - 379\New folder\Advance C#\2. Advance C#\DataSerialization\DataSerialization\Files\Object.xml"))
            {
                xmlSerializer.Serialize(writer, objPet);
            }
            Console.WriteLine("Process completed");
        }

        /// <summary>
        /// Deserializes xml file to object
        /// </summary>
        public void DeserializeXmlFileToObject()
        {
            var xmlSerializer = new XmlSerializer(typeof(Pet));
            using (var reader = new StreamReader(@"F:\Krinsi - 379\New folder\Advance C#\2. Advance C#\DataSerialization\DataSerialization\Files\Object.xml"))
            {
                var objPet = (Pet)xmlSerializer.Deserialize(reader);
                Console.WriteLine("Pet Name : " + objPet.Name + ", Pet breed : " + objPet.Breed);
            }
        }

        /// <summary>
        /// Serializes list of object to xml file
        /// </summary>
        public void SerializeListToXmlFile()
        {
            var lstPet = new List<Pet>
            {
                new Pet{ Name = "Dolly", Breed = "Dolphin" },
                new Pet{ Name = "Tom", Breed = "Cat" },
                new Pet{ Name = "Jerry", Breed = "Mouse" },
                new Pet{ Name = "Mickey", Breed = "Mouse" },
                new Pet{ Name = "Donald", Breed = "Duck" }
            };
            
            var xmlSerializer = new XmlSerializer(typeof(List<Pet>));
            
            using (var writer = new StreamWriter(@"F:\Krinsi - 379\New folder\Advance C#\2. Advance C#\DataSerialization\DataSerialization\Files\List.xml"))
            {
                xmlSerializer.Serialize(writer, lstPet);
            }
            Console.WriteLine("Process completed");
        }

        /// <summary>
        /// Deserializes xml file to list of object
        /// </summary>
        public void DeserializeXmlFileToList()
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Pet>));
            using (var reader = new StreamReader(@"F:\Krinsi - 379\New folder\Advance C#\2. Advance C#\DataSerialization\DataSerialization\Files\List.xml"))
            {
                var lstPet = (List<Pet>)xmlSerializer.Deserialize(reader);
                foreach (var objPet in lstPet)
                {
                    Console.WriteLine("Pet Name : " + objPet.Name + ", Pet breed : " + objPet.Breed);
                }
            }
        }

        public static void Main(string[] args)
        {
            Flower objFlower = new Flower();
            objFlower.Id = 1;
            objFlower.Name = "Rose";
            objFlower.Color = "Red";

            // Binary serialization
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memStream, objFlower);

            // Prints binary array of serialized 
            //byte[] arr = memStream.ToArray();
            //foreach (byte b in arr)
            //{
            //    Console.WriteLine(b);
            //}


            // Json serialization using DataContract
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Flower));
            MemoryStream msObj = new MemoryStream();
            js.WriteObject(msObj, objFlower);
            msObj.Position = 0;
            StreamReader sr = new StreamReader(msObj);

            string json = sr.ReadToEnd();
            Console.WriteLine(json);

            sr.Close();
            msObj.Close();


            // Json deserialization using DataContract
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                // Deserialization from JSON
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(Flower));
                Flower objFlower2 = (Flower)deserializer.ReadObject(ms);
                Console.WriteLine(objFlower2.Color);
            }

            // Json serialization using .net
            string jsonData = JsonConvert.SerializeObject(objFlower);
            Console.WriteLine(jsonData);

            // Json deserialization using .net
            Flower objFlower3 = JsonConvert.DeserializeObject<Flower>(jsonData);
            Console.WriteLine(objFlower3.Color);

            Program obj = new Program();
            Console.WriteLine("\nSerialization of xml object to string : ");
            var xmlString = obj.SerializeXMLObjectToString();

            Console.WriteLine("\nDeserialization of xml string to object : ");
            obj.DeserializeXMLStringToObject(xmlString);

            Console.WriteLine("\nSerialization of object to xml file : ");
            obj.SerializeObjectToXmlFile();

            Console.WriteLine("\nDeserialization of xml file to object : ");
            obj.DeserializeXmlFileToObject();

            Console.WriteLine("\nSerialization of list of objects to xml file : ");
            obj.SerializeListToXmlFile();

            Console.WriteLine("\nDeserialization of xml file to list of objects : ");
            obj.DeserializeXmlFileToList();
        }
    }
}
