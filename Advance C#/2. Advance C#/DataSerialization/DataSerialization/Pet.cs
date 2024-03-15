using System.Xml.Serialization;

namespace DataSerialization
{
    /// <summary>
    /// class of pets
    /// </summary>
    [Serializable]
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

    }
}
