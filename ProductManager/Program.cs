using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

using ItemClass;
using ProductManagerClass;

namespace ItemManager
{
    internal class Program
    {
        

        static async Task Main(string[] args)
        {
            Program program = new Program();
            await program.RunProductManager();
        }

        private async Task RunProductManager()
        {
            ProductManager productManager = new ProductManager();
            string user_response = ""; 

            while(user_response != "Q")
            {
                Console.Write("Enter a command: ");
                user_response = Console.ReadLine();
                if (user_response.ToUpper() == "L")
                {
                    await productManager.listAllProducts();
                }
                else if (user_response.ToUpper() == "W")
                {
                    Console.Clear();
                }
                else if (user_response.ToUpper() == "R")
                {
                    Console.Write("Enter the ID number of the product you would like to view the details of: ");
                    string product_id = Console.ReadLine();
                    int test_product_id;
                    if (int.TryParse(product_id, out test_product_id))
                        await productManager.viewProductDetails(product_id);
                    else
                        Console.WriteLine("Invalid product ID entered. Product ID must be an integer number \n");

                }
                else if (user_response.ToUpper() == "H")
                {
                    Console.WriteLine($"{"Command",-10}" + $"{"Description",-20}");
                    Console.WriteLine($"{"C",-10}" + $"{"Create a new product",-20}");
                    Console.WriteLine($"{"D",-10}" + $"{"Delete an existing product",-20}");
                    Console.WriteLine($"{"H",-10}" + $"{"List of all available commands",-20}");
                    Console.WriteLine($"{"L",-10}" + $"{"List of all available products",-20}");
                    Console.WriteLine($"{"R",-10}" + $"{"Display the details of a specific product",-20}");
                    Console.WriteLine($"{"U",-10}" + $"{"Update the details associated with a specific product",-20}");
                    Console.WriteLine($"{"W",-10}" + $"{"Clears the console screen",-20} \n");

                }
                else
                {
                    Console.WriteLine("'{0}' Is not a valid command. Enter the letter 'H' for a list of all available commands. \n", user_response); 
                }
            }
        }


    }
}
