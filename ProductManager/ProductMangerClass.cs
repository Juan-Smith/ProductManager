using System;
using Newtonsoft.Json;
using ItemClass;
using System.Net;

/// <summary>
/// Summary description for Class1
/// </summary>
/// 
namespace ProductManagerClass
{
    public class ProductManager
    {
        private const string base_address = "https://gendacproficiencytest.azurewebsites.net/API/ProductsAPI/";

        private const int timout_hour_count = 0;
        private const int timeout_minute_count = 2;
        private const int timeout_second_count = 0; 

        private HttpClient product_manager_client;


        public ProductManager()
        {
            this.product_manager_client = new HttpClient();
            this.product_manager_client.BaseAddress = new Uri(base_address);
            this.product_manager_client.Timeout = new TimeSpan(timout_hour_count, timeout_minute_count, timeout_second_count); 
        }

        public async Task listAllProducts()
        {
            Console.WriteLine("Retrieving list of products...");

            string response = await this.product_manager_client.GetStringAsync("");

            List<Item> item_list = JsonConvert.DeserializeObject<List<Item>>(response);

            Console.WriteLine($"{"Id",-10}" + $"{"Product Name",-20}" + $"{"Category",20}" + $"{"Price",10}");

            foreach (Item item in item_list)
            {
                // add the details of each item in the list. 
                Console.WriteLine($"{item.Id,-10}" + $"{item.Name,-20}" + $"{item.Category,20}" + $"{item.Price,10}");
            }
            Console.WriteLine();
        }

        public async Task viewProductDetails(string product_id)
        {
            Console.WriteLine("Retrieving product details...");

            try
            {
                string response = await this.product_manager_client.GetStringAsync(product_id);
                Item item = JsonConvert.DeserializeObject<Item>(response);

                Console.WriteLine($"{"Id",-10}" + $"{"Product Name",-20}" + $"{"Category",20}" + $"{"Price",10}");
                Console.WriteLine($"{item.Id,-10}" + $"{item.Name,-20}" + $"{item.Category,20}" + $"{item.Price,10} \n");
            }
            catch(Exception ex)
            {
                Console.WriteLine(); 
                Console.WriteLine("An error occured while trying to retrive product details."); 
                Console.WriteLine(ex.Message + "\n");
            }
            
        }

        public async Task createNewProduct(int product_id, string product_name, int product_category, double product_price)
        {

        }

    }
}
