using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BusinessSolution
{

    /// <summary>
    /// Query class for managing products
    /// </summary>
    class ProductManagerQuery
    {
        /// <summary>
        /// Query for Showing all category
        /// </summary>
        /// <returns></returns>
        public string ShowAllCategory()
        {
            return "SELECT * FROM Category";
        }

        /// <summary>
        /// Query for creating a new category
        /// </summary>
        /// <returns></returns>
        internal string CreateCategory()
        {
            return "INSERT INTO Category (CategoryType) VALUES (@CategoryType)";
        }

        /// <summary>
        /// Query for searching category
        /// </summary>
        /// <returns></returns>
        internal string SearchCategory()
        {
            return "SELECT COUNT(*) FROM Category WHERE CategoryType = @CategoryType";
        }

        /// <summary>
        /// Query for getting specific category
        /// </summary>
        /// <returns></returns>
        internal string GetSpecificCategory()
        {
            return "SELECT * FROM Category WHERE CategoryType = '";
        }

        /// <summary>
        /// Query for deleting Category
        /// </summary>
        /// <returns></returns>
        internal string DeleteCategory()
        {
            return "DELETE FROM Category WHERE CategoryType = @CategoryType";
        }

        /// <summary>
        /// Query for adding new product
        /// </summary>
        /// <returns></returns>
        internal string AddProduct()
        {
            return "INSERT INTO Product (ProductName,  ProductQuantity, ProductBasePrice, ProductMarketPrice, DealerCommission,  BrandName)" +
                "VALUES (@ProductName, @ProductQuantity, @ProductBasePrice, @ProductMarketPrice, @DealerCommission,  @BrandName)";
        }

        /// <summary>
        /// Query for getting the full product inventory
        /// </summary>
        /// <returns></returns>
        internal string GetFullInventory()
        {
            return "SELECT * FROM PRODUCT";
        }

        /// <summary>
        /// query for getting specific brand inventory
        /// </summary>
        /// <param name="brandName"></param>
        /// <returns></returns>
        internal string GetBrandInventory(string brandName)
        {
            return "SELECT ProductName,  ProductQuantity, ProductBasePrice, ProductMarketPrice, DealerCommission,  BrandName  FROM Product WHERE BrandName = '" + brandName +"'" ;
        }

        /// <summary>
        /// Query for deleting any product
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        internal string RemoveProduct(string productName)
        {
            return "DELETE FROM Product WHERE ProductName = '" + productName +"'";
        }

        /// <summary>
        /// Query for searching any product
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        internal string SearchProduct(string productName)
        {
            return "SELECT COUNT(*) FROM Product WHERE ProductName = '" + productName + "'";
        }

        internal string GetSpecificProductDetails(string productName)
        {
            return "SELECT ProductName, ProductQuantity, ProductBasePrice, ProductMarketPrice, DealerCommission FROM Product WHERE ProductName = '" + productName + "'";
        }

        internal string GetBrandId(string brandName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BusinessSolution.Properties.Settings.BusinessSolutionDBv2ConnectionString"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string query = "SELECT BrandID FROM Brand WHERE BrandName = @BrandName";
                SqlCommand sqlcmd = new SqlCommand(query, sqlConnection);
                sqlcmd.Parameters.AddWithValue("@BrandName", brandName);

                return (string)sqlcmd.ExecuteScalar();
            }
        }
    }
}
