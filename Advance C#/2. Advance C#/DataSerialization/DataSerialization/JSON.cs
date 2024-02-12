using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;

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

    /// <summary>
    /// Constructor of flower class
    /// </summary>
    /// <param name="id">id of flower</param>
    /// <param name="name">name of flower</param>
    /// <param name="color">color of flower</param>
    public Flower(int id, string name, string color)
    {
        Id = id;
        Name = name;
        Color = color;
    }


    public static void Main(string[] args)
    {
        // Binary serialization
        Flower objFlower = new Flower(1, "Rose", "Red");
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
    }
}