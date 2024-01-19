using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace DataSerialization
{
    public class Pet
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        public Pet() { }
        public Pet(string name,string breed) { 
            Name = name;
            Breed = breed;
        }
        public static void Main(string[] args)
        {
            Pet pet = new Pet("Sheru","Dog");
            XmlDocument myXml = new XmlDocument();
            XPathNavigator xNav = myXml.CreateNavigator();
            XmlSerializer x = new XmlSerializer(pet.GetType());
            using (var xs = xNav.AppendChild())
            {
                x.Serialize(xs, pet);
            }
            var xData = myXml.OuterXml;
            Console.WriteLine(xData);

            Pet myPet = (Pet)x.Deserialize(new StringReader(xData));
            Console.WriteLine(myPet.Name);
        }

    }
}
