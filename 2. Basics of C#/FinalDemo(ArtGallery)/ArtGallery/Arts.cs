/// <summary>
/// Art class is parent class and creates object of art with it's information
/// </summary>
public class Arts
{
    #region Public Members

    /// <summary>
    /// Used for Art name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// To categories art peices.
    /// </summary>
    public string category = "Art";

    /// <summary>
    /// Used for Artist's name who created an art piece.
    /// </summary>
    public string ArtistName { get; set; }

    /// <summary>
    /// Used for creation date of an art piece.
    /// </summary>
    public DateTime CreationDate= DateTime.Now;

    /// <summary>
    /// Indicates weather the art is sold out yet or not.
    /// </summary>
    public bool IsSold { get; set; }

    /// <summary>
    /// Declares price of an art piece.
    /// </summary>
    public long Price { get; set; }

    #endregion


    #region Constructors

    /// <summary>
    /// Creates object of Art class with parameters.
    /// </summary>
    /// <param name="name">Art name</param>
    /// <param name="artistName">Artist's name</param>
    /// <param name="creationDate">Creation date of an art</param>
    /// <param name="isSold">Is the art sold or not</param>
    /// <param name="price">Price of art</param>
    public Arts(string name, string artistName, DateTime creationDate, bool isSold, long price)
    {
        Name = name;
        CreationDate = creationDate;
        IsSold = isSold;
        Price = price;
        ArtistName = artistName;
    }

    #endregion

}
