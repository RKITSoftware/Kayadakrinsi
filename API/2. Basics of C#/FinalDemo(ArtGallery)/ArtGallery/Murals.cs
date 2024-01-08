/// <summary>
/// Murals class inherites Paintings class
/// </summary>
public class Murals : Paintings
{
    #region Public Members

    /// <summary>
    /// To categories art peices.
    /// </summary>
    public string category = "Murals";

    /// <summary>
    /// Declares price of an art piece.
    /// </summary>
    public long Price { get; set; }

    #endregion


    #region Constructors

    /// <summary>
    /// Creates object of Murals class with parameters.
    /// </summary>
    /// <param name="name">Art name</param>
    /// <param name="artistName">Artist's name</param>
    /// <param name="creationDate">Creation date of an art</param>
    /// <param name="isSold">Is the art sold or not</param>
    /// <param name="price">Price of art</param>
    public Murals(string name, string artistName, DateTime creationDate, bool isSold, long price) : base(name, artistName, creationDate, isSold, price)
    {
        Name = name;
        CreationDate = creationDate;
        IsSold = isSold;
        Price = price;
        ArtistName = artistName;
    }

    #endregion

}