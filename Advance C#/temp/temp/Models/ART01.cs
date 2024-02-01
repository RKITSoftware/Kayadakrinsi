using ServiceStack.DataAnnotations;

namespace temp.Models
{
    [Alias("ART01")]
    public class ART01
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [References(typeof(USR01))] 
        public int UserID { get; set; }
    }
}