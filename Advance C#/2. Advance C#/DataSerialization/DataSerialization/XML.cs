using System.Xml.Serialization;

namespace DataSerialization
{
    /// <summary>
    /// class of pets
    /// </summary>
    [Serializable]
    [XmlRoot("PetDetails")]
    public class Pet
    {
        /// <summary>
        /// Name of pet
        /// </summary>
        // [XmlIgnore]
        public string Name { get; set; }

        /// <summary>
        /// Breed of pet
        /// </summary>
        // [XmlAttribute]
        public string Breed { get; set; }

        /// <summary>
        /// Serializes object to xml string
        /// </summary>
        /// <returns>Serialized string</returns>
        public static string SerializeXMLObjectToString()
        {
            Pet objPet = new Pet { Name = "Sheru", Breed = "Dog" };

            var xmlSerializer = new XmlSerializer(typeof(Pet));
            using(var writer = new StringWriter())
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
        public static void DeserializeXMLStringToObject(string xmlString) 
        {
            var xmlSerializer = new XmlSerializer(typeof(Pet));
            using (var reader = new StringReader(xmlString))
            {
                var objPet = (Pet)xmlSerializer.Deserialize(reader);
                Console.WriteLine("Pet Name : " + objPet.Name +  ", Pet breed : " + objPet.Breed);
            }
        }

        /// <summary>
        /// Serializes object to xml file
        /// </summary>
        public static void SerializeObjectToXmlFile()
        {
            var objPet = new Pet { Name = "Kitty", Breed = "cat" };

            var xmlSerializer = new XmlSerializer(typeof(Pet));
            using (var writer = new StreamWriter(@"F:\Krinsi - 379\Training\Advance C#\2. Advance C#\DataSerialization\DataSerialization\Files\Object.xml"))
            {
                xmlSerializer.Serialize(writer, objPet);
            }
            Console.WriteLine("Process completed");
        }

        /// <summary>
        /// Deserializes xml file to object
        /// </summary>
        public static void DeserializeXmlFileToObject()
        {
            var xmlSerializer = new XmlSerializer(typeof(Pet));
            using (var reader = new StreamReader(@"F:\Krinsi - 379\Training\Advance C#\2. Advance C#\DataSerialization\DataSerialization\Files\Object.xml"))
            {
                var objPet = (Pet)xmlSerializer.Deserialize(reader);
                Console.WriteLine("Pet Name : " + objPet.Name + ", Pet breed : " + objPet.Breed);
            }
        }
        
        /// <summary>
        /// Serializes list of object to xml file
        /// </summary>
        public static void SerializeListToXmlFile()
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
            using (var writer = new StreamWriter(@"F:\Krinsi - 379\Training\Advance C#\2. Advance C#\DataSerialization\DataSerialization\Files\List.xml"))
            {
                xmlSerializer.Serialize(writer, lstPet);
            }
            Console.WriteLine("Process completed");
        }

        /// <summary>
        /// Deserializes xml file to list of object
        /// </summary>
        public static void DeserializeXmlFileToList()
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Pet>));
            using (var reader = new StreamReader(@"F:\Krinsi - 379\Training\Advance C#\2. Advance C#\DataSerialization\DataSerialization\Files\List.xml"))
            {
                var lstPet = (List<Pet>)xmlSerializer.Deserialize(reader);
                foreach(var objPet in lstPet)
                {
                    Console.WriteLine("Pet Name : " + objPet.Name + ", Pet breed : " + objPet.Breed);
                }
            }
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Serialization of xml object to string : ");
            var xmlString = Pet.SerializeXMLObjectToString();

            Console.WriteLine("\nDeserialization of xml string to object : ");
            Pet.DeserializeXMLStringToObject(xmlString);

            Console.WriteLine("\nSerialization of object to xml file : ");
            Pet.SerializeObjectToXmlFile();

            Console.WriteLine("\nDeserialization of xml file to object : ");
            Pet.DeserializeXmlFileToObject();

            Console.WriteLine("\nSerialization of list of objects to xml file : ");
            Pet.SerializeListToXmlFile();

            Console.WriteLine("\nDeserialization of xml file to list of objects : ");
            Pet.DeserializeXmlFileToList();
        }
    }
}
