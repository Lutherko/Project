using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ElectronicComponents.Pages.Stock
{
    public class IndexModel : PageModel
    {
        public List<StockInfo> listStocks = new List<StockInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ElectronicComponents;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM stock";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StockInfo stockInfo = new StockInfo();
                                stockInfo.id = "" + reader.GetInt32(0);
                                stockInfo.name = reader.GetString(1);
                                stockInfo.manufacturer = reader.GetString(2);
                                stockInfo.price = reader.GetString(3);
                                stockInfo.address = reader.GetString(4);
                                stockInfo.created_at = reader.GetDateTime(5).ToString();

                                listStocks.Add(stockInfo);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class StockInfo
    {
        public String id;
        public String name;
        public String manufacturer;
        public String price;
        public String address;
        public String created_at;


    }
}
