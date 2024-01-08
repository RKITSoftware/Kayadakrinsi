/// <summary>
/// RasinArt class inherites Art class
/// </summary>
public class RasinArt : Arts
{
    #region Public Members

    /// <summary>
    /// To categories art peices.
    /// </summary>
    public string category = "Rasin Art";

    #endregion


    #region Private Mambers

    /// <summary>
    /// Declares price of an art piece.
    /// </summary>
    long _Price { get; set; }

    #endregion


    #region Constructors

    /// <summary>
    /// Creates object of RasinArt class with parameters.
    /// </summary>
    /// <param name="name">Art name</param>
    /// <param name="artistName">Artist's name</param>
    /// <param name="creationDate">Creation date of an art</param>
    /// <param name="isSold">Is the art sold or not</param>
    /// <param name="price">Price of art</param>
    public RasinArt(string name, string artistName, DateTime creationDate, bool isSold, long price) : base(name, artistName, creationDate, isSold, price)
    {
        Name = name;
        CreationDate = creationDate;
        IsSold = isSold;
        _Price = price;
        ArtistName = artistName;
    }

    #endregion

}