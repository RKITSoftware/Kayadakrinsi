namespace DataSerialization
{
    /// <summary>
    /// Class of flower
    /// </summary>
    [Serializable]
    public class Flower
    {
        /// <summary>
        /// Id of flower
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of flower
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Color of flower
        /// </summary>
        public string Color { get; set; }

    }
}