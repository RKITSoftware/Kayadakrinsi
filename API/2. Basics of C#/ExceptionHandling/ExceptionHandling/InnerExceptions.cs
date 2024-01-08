///// <summary>
///// innerExceptions class is used to demonstrate how to handle innerExceptions within the ase class.
///// </summary>
//public class InnerExceptions
//{
//    #region Public Methods

//    /// <summary>
//    /// GetInt method is used to handle exception array index out of bound and inner exception parameter out of range as well.
//    /// </summary>
//    /// <param name="array">Used to get element to return elements of it at particluar index</param>
//    /// <param name="index">USed to access element at particular index from array</param>
//    /// <returns>Element of array from given index</returns>
//    /// <exception cref="ArgumentOutOfRangeException">Indicates that index given is greater than array size</exception>
//    public static int GetInt(int[] array, int index)
//    {
//        try
//        {
//            return array[index];
//        }
//        catch (IndexOutOfRangeException e) when (index < 0)
//        {
//            throw new ArgumentOutOfRangeException(
//                "Parameter index cannot be negative.", e);
//        }
//        catch (IndexOutOfRangeException e)
//        {
//            throw new ArgumentOutOfRangeException(
//                "Parameter index cannot be greater than the array size.", e);
//        }
//    }

//    public static void Main()
//    {
//        int[] array = { 1, 2, 3 };
//        int element = GetInt(array, -1);
//    }

//    #endregion
//}