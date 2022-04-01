using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BusinessSolution
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : UserControl
    {
        string connectionString = ConfigurationManager.ConnectionStrings["BusinessSolution.Properties.Settings.BusinessSolutionDBv2ConnectionString"].ConnectionString;
        ProductManagerQuery productManagerQuery = new ProductManagerQuery();

        public AddProduct()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (this.Parent as StackPanel).Children.Remove(this);
                Message.Content = "";
                Message.Visibility = Visibility.Collapsed;
                CalculateDealerCommissionButton.Visibility = Visibility.Collapsed;
                RegisterProductButton.Visibility = Visibility.Collapsed;
                CalculateMarketPriceButton.Visibility = Visibility.Visible;
                RefreshButton.Visibility = Visibility.Collapsed;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CalculateMarketPriceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductBasePrice.Text == "")
                {
                    Message.Content = "Base Price Field cannot be empty!!!";
                    Message.Foreground = Brushes.Red;
                    Message.Visibility = Visibility.Visible;
                }
                else
                {
                    Message.Content = "";
                    Message.Visibility = Visibility.Collapsed;
                    ProductMarketPrice.Text = ((Convert.ToInt32(ProductBasePrice.Text.ToString()) * .2) + (Convert.ToInt32(ProductBasePrice.Text.ToString()))).ToString();
                    CalculateMarketPriceButton.Visibility = Visibility.Collapsed;
                    CalculateDealerCommissionButton.Visibility = Visibility.Visible;
                }     
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }      
        }

        private void RegisterProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlcmd = new SqlCommand(productManagerQuery.AddProduct(), sqlConnection);
                    sqlcmd.Parameters.AddWithValue("@ProductName", ProductName.Text.ToString().ToUpper());
                    sqlcmd.Parameters.AddWithValue("@ProductLaunchDate", ProductLaunchDate.Text.ToUpper());
                    sqlcmd.Parameters.AddWithValue("@ProductQuantity", ProductQuantity.Text.ToUpper());
                    sqlcmd.Parameters.AddWithValue("@ProductBasePrice", ProductBasePrice.Text.ToString().ToUpper());
                    sqlcmd.Parameters.AddWithValue("@ProductMarketPrice", ProductMarketPrice.Text.ToString().ToUpper());
                    sqlcmd.Parameters.AddWithValue("@DealerCommission", DealerCommission.Text.ToString().ToUpper());

                    //if(ProductBrand.Text == "")
                    //{
                    //    sqlcmd.Parameters.AddWithValue("@BrandId", null);
                    //    sqlcmd.Parameters.AddWithValue("@BrandName", null);
                    //}
                    //else
                    //{
                        //sqlcmd.Parameters.AddWithValue("@BrandId", Convert.ToInt32(productManagerQuery.GetBrandId(ProductBrand.Text.ToString())));
                        sqlcmd.Parameters.AddWithValue("@BrandName", ProductBrand.Text.ToString().ToUpper());
                    //}
                    //sqlcmd.Parameters.AddWithValue("@ProductMarketPrice", ProductMarketPrice.Text);
                    sqlcmd.ExecuteNonQuery();

                    if(Message.Visibility == Visibility.Collapsed)
                    {
                        RegisterProductButton.Visibility = Visibility.Collapsed;
                        RefreshButton.Visibility = Visibility.Visible;
                        Message.Visibility = Visibility.Visible;
                        Message.Foreground = Brushes.Green;
                        Message.Content = "Product ADDED into the database!!";
                    }
                    else
                    {
                        RegisterProductButton.Visibility = Visibility.Visible;
                        Message.Foreground = Brushes.Green;
                        Message.Content = "Product ADDED into the database!!";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CalculateDealerCommissionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductMarketPrice.Text == "")
                {
                    Message.Content = "Calculate Market Price First!!!";
                    Message.Foreground = Brushes.Red;
                    Message.Visibility = Visibility.Visible;
                }
                else
                {
                    Message.Content = "";
                    Message.Visibility = Visibility.Collapsed;
                    DealerCommission.Text = (Convert.ToInt32(ProductMarketPrice.Text.ToString()) * .05).ToString();
                    CalculateDealerCommissionButton.Visibility = Visibility.Collapsed;
                    RegisterProductButton.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshButton.Visibility = Visibility.Collapsed;
                Message.Content = "";
                ProductBrand.Text = "";
                ProductName.Text = "";
                ProductQuantity.Text = "";
                ProductBasePrice.Text = "";
                ProductMarketPrice.Text = "";
                DealerCommission.Text = "";
                CalculateMarketPriceButton.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
