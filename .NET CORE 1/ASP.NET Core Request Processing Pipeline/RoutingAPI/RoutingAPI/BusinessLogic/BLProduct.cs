using RoutingAPI.Model;

namespace RoutingAPI.BusinessLogic
{
    /// <summary>
    /// Contains logic for product
    /// </summary>
    public class BLProduct
    {

        #region Public Members

        /// <summary>
        /// Incrementor for product id
        /// </summary>
        public static int idCount = 0;

        /// <summary>
        /// List of products
        /// </summary>
        public static List<PRO01> lstPRO01 = new List<PRO01>();

        #endregion

        #region Public Methods 

        /// <summary>
        /// Gets single product through id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Object of PRO01 class</returns>
        public PRO01 GetProductById(int id)
        {
            var product = lstPRO01.FirstOrDefault(p => p.R01F01 == id);
            return product;
        }

        /// <summary>
        /// Validates object of product
        /// </summary>
        /// <param name="objPRO01">Object of PRO01 class</param>
        /// <returns>True if object is valid, false otherwise</returns>
        public bool Validation(PRO01 objPRO01)
        {
            var product = lstPRO01.FirstOrDefault(p => p.R01F02 == objPRO01.R01F02);

            if (product == null && objPRO01.R01F02 != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Validates object before updation
        /// </summary>
        /// <param name="objPRO01">Object of PRO01 class</param>
        /// <returns>True if object is valid, false otherwise</returns>
        public bool validationUpdate(PRO01 objPRO01)
        {
            var product = lstPRO01.FirstOrDefault(p => p.R01F02 == objPRO01.R01F02);

            if (product != null && objPRO01.R01F02 != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Adds product to list
        /// </summary>
        /// <param name="objPRO01">Object of PRO01 class</param>
        /// <returns>Appropriate message</returns>
        public string AddProduct(PRO01 objPRO01)
        {
            idCount++;
            lstPRO01.Add(new PRO01 { R01F01 = idCount, R01F02 = objPRO01.R01F02, R01F03 = objPRO01.R01F03, R01F04 = objPRO01.R01F04 });
            return "Product added successfully.";
        }

        /// <summary>
        /// Updates product
        /// </summary>
        /// <param name="objPRO01">Object of PRO01 class</param>
        /// <returns>Appropriate message</returns>
        public string EditProduct(PRO01 objPRO01)
        {
            var product = lstPRO01.FirstOrDefault(p => p.R01F01 == objPRO01.R01F01);

            if (product != null)
            {
                lstPRO01[product.R01F01 - 1] = objPRO01;
                return "Product updated successfully.";
            }
            return "Invalid data";
        }

        /// <summary>
        /// Deletes product from list 
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Appropriate message</returns>
        public string DeleteProduct(int id)
        {
            var product = lstPRO01.FirstOrDefault(p => p.R01F01 == id);

            if (product != null)
            {
                lstPRO01.Remove(product);
                return "Product deleted successfully.";
            }
            return "Invalid data";
        }

        #endregion

    }
}
