using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Security;

namespace ElectronicComponents.Pages.Stock
{
    public class CreateModel : PageModel
    {
        public StockInfo stockInfo = new StockInfo();
        public String errorMessage = "";
        public String succesMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            stockInfo.name = Request.Form["name"];
            stockInfo.name = Request.Form["manufacturer"];
            stockInfo.name = Request.Form["price"];
            stockInfo.name = Request.Form["address"];

            if (stockInfo.name.Length == 0 || stockInfo.manufacturer.Length == 0 ||
                stockInfo.price.Length == 0 || stockInfo.address.Length ==0)
            {
                errorMessage = "All of the fields are required";
                return;
            }

            //Save the new client
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ElectronicComponents;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection( connectionString ))
                {
                    connection.Open();
                    String sql = "INSERT INTO stock" +
                                 "(name, manufacturer, price, address) VALUES " +
                                 "(@name, @manufacturer, @price, @address);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("name", stockInfo.name);
                        command.Parameters.AddWithValue("manufacturer", stockInfo.manufacturer);
                        command.Parameters.AddWithValue("price", stockInfo.price);
                        command.Parameters.AddWithValue("address", stockInfo.address);

                        command.ExecuteNonQuery();
                    }

                }
            }

            catch ( Exception ex )
            {
                errorMessage = ex.Message;
                return;
            }

            stockInfo.name = ""; stockInfo.manufacturer = ""; stockInfo.price = ""; stockInfo.address = "";
            succesMessage = "New Stock Added Correctly";

            Response.Redirect("/Stock/Index");
        }
    }
}
