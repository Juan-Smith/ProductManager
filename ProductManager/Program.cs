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
            Console.WriteLine("Started product manager. Enter 'Q' to quit"); 

            ProductManager productManager = new ProductManager();
            string user_response = ""; 

            while(user_response.ToUpper() != "Q")
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
                else if(user_response.ToUpper() == "C")
                {
                    Console.WriteLine("Enter the details associated with the new product");


                    Console.Write("Product ID: ");
                    string potential_product_id = Console.ReadLine();
                    int product_id;

                    if (int.TryParse(potential_product_id, out product_id) && product_id >= 0)
                    {
                        Console.Write("Product Name: ");
                        string product_name = Console.ReadLine();

                        Console.Write("Product Category: ");
                        string potential_product_category = Console.ReadLine();
                        int product_category;

                        if (int.TryParse(potential_product_category, out product_category) && product_category >= 0)
                        {
                            Console.Write("Product Price: ");
                            string potential_product_price = Console.ReadLine();
                            double product_price;

                            if (double.TryParse(potential_product_price, out product_price)  && product_price >= 0)
                            {
                                await productManager.createNewProduct(product_id, product_name, product_category, product_price); 
                            }
                            else
                            {
                                Console.WriteLine("'{0}' Is not a valid Product Price. Product Price must be a positive real number \n", potential_product_price);
                            }
                                
                        }
                        else
                        {
                            Console.WriteLine("'{0}' Is not a valid Product Category. Product Category must be a positive whole number \n", potential_product_category);
                        }
  
                    }
                    else
                    {
                        Console.WriteLine("'{0}' Is not a valid Product ID. Product ID must be a positive whole number \n", potential_product_id);
                    }

                }
                else if(user_response.ToUpper() == "U")
                {
                    Console.WriteLine("Enter the details of the product to update");


                    Console.Write("Product ID: ");
                    string potential_product_id = Console.ReadLine();
                    int product_id;

                    if (int.TryParse(potential_product_id, out product_id) && product_id >= 0)
                    {
                        Console.Write("Product Name: ");
                        string product_name = Console.ReadLine();

                        Console.Write("Product Category: ");
                        string potential_product_category = Console.ReadLine();
                        int product_category;

                        if (int.TryParse(potential_product_category, out product_category) && product_category >= 0)
                        {
                            Console.Write("Product Price: ");
                            string potential_product_price = Console.ReadLine();
                            double product_price;

                            if (double.TryParse(potential_product_price, out product_price) && product_price >= 0)
                            {
                                await productManager.updateProduct(product_id, product_name, product_category, product_price); 
                            }
                            else
                            {
                                Console.WriteLine("'{0}' Is not a valid Product Price. Product Price must be a positive real number \n", potential_product_price);
                            }

                        }
                        else
                        {
                            Console.WriteLine("'{0}' Is not a valid Product Category. Product Category must be a positive whole number \n", potential_product_category);
                        }

                    }
                    else
                    {
                        Console.WriteLine("'{0}' Is not a valid Product ID. Product ID must be a positive whole number \n", potential_product_id);
                    }
                }
                else if(user_response.ToUpper() == "D")
                {
                    Console.Write("Enter product ID associated with product you wish to delete: ");
                    string potential_product_id = Console.ReadLine();
                    int product_id;

                    if (int.TryParse(potential_product_id, out product_id) && product_id >= 0)
                    {
                        await productManager.deleteProduct(product_id);
                    }
                    else
                    {
                        Console.WriteLine("'{0}' Is not a valid Product ID. Product ID must be a positive whole number \n", potential_product_id);
                    }
                }
                else
                {
                    Console.WriteLine("'{0}' Is not a valid command. Enter the letter 'H' for a list of all available commands. \n", user_response); 
                }
            }
        }


    }
}
