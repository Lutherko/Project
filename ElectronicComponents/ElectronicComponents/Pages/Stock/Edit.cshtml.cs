using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ElectronicComponents.Pages.Stock
{
    public class EditModel : PageModel
    {
        public StockInfo stockInfo = new StockInfo();
        public String errorMessage = "";
        public String succesMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ElectronicComponents;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM stock WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                stockInfo.id = "" + reader.GetInt32(0);
                                stockInfo.name = reader.GetString(1);
                                stockInfo.manufacturer = reader.GetString(2);
                                stockInfo.price = reader.GetString(3);
                                stockInfo.address = reader.GetString(4);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            stockInfo.id = Request.Form["id"];
            stockInfo.name = Request.Form["name"];
            stockInfo.manufacturer = Request.Form["manufacturer"];
            stockInfo.price = Request.Form["price"];
            stockInfo.address = Request.Form["address"];

            if (stockInfo.id.Length == 0 || stockInfo.name.Length == 0 ||
                stockInfo.manufacturer.Length == 0 || stockInfo.price.Length == 0 ||
                stockInfo.address.Length == 0)
            {
                errorMessage = "All of the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ElectronicComponents;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE stock " +
                                 "SET name=@name, manufacturer=@manufacturer, price=@price, address=@address" +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", stockInfo.id);
                        command.Parameters.AddWithValue("name", stockInfo.name);
                        command.Parameters.AddWithValue("manufacturer",stockInfo.manufacturer);
                        command.Parameters.AddWithValue("price", stockInfo.price);
                        command.Parameters.AddWithValue ("address", stockInfo.address);

                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Stock/Index");

        }

      
    }
}
