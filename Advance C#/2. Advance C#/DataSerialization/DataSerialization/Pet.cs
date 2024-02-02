using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace DataSerialization
{
    /// <summary>
    /// class of pets
    /// </summary>
    public class Pet
    {
        /// <summary>
        /// Name of pet
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Breed of pet
        /// </summary>
        public string Breed { get; set; }

        /// <summary>
        /// Constructor of class pet
        /// </summary>
        /// <param name="name">name of pet</param>
        /// <param name="breed">breed of pet</param>
        public Pet(string name,string breed) { 
            Name = name;
            Breed = breed;
        }

        public static void Main(string[] args)
        {
            Pet pet = new Pet("Sheru","Dog");

            // XML serialization
            XmlDocument myXml = new XmlDocument();
            XPathNavigator xNav = myXml.CreateNavigator();
            XmlSerializer x = new XmlSerializer(pet.GetType());
            using (var xs = xNav.AppendChild())
            {
                x.Serialize(xs, pet);
            }
            var xData = myXml.OuterXml;
            Console.WriteLine(xData);

            // XML deserialization
            Pet myPet = (Pet)x.Deserialize(new StringReader(xData));
            Console.WriteLine(myPet.Name);
        }

    }
}
