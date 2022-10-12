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
            /* Uncomments first line to run unit tests AND comment out the second line */
            
            //await RunUnitTests();
            await RunProductManager(); // call the main function to run the product manager function. 

        }

        /// <summary>
        /// Main funciton which executes the Product Manager 
        /// </summary>
        static async Task RunProductManager()
        {

            ProductManager productManager = new ProductManager("https://gendacproficiencytest.azurewebsites.net/API/ProductsAPI/", 0, 2, 0); // initalize instance of the Product Manager class

            Console.WriteLine("Started product manager. Enter 'Q' to quit");
            string user_response = "";

            while (user_response.ToUpper() != "Q") // repeat until the user enter s the quit command. 
            {
                Console.Write("Enter a command: ");
                user_response = Console.ReadLine();

                if (user_response.ToUpper() == "L") // if user wants to list all availabe products 
                {
                    await productManager.listAllProducts();
                }
                else if (user_response.ToUpper() == "W") // if user wants to clear the console screen 
                {
                    Console.Clear();
                }
                else if (user_response.ToUpper() == "R") // if users wants to read details associated with a specific product 
                {
                    Console.Write("Enter the ID number of the product you would like to view the details of: ");
                    string product_id = Console.ReadLine(); // read the ID associated with the specific product 
                    int test_product_id;
                    if (int.TryParse(product_id, out test_product_id)) // check to see if the provided Id is a valid integer 
                        await productManager.viewProductDetails(product_id); // call the product managers function to display details of the specified product 
                    else
                        Console.WriteLine("Invalid product ID entered. Product ID must be an integer number \n"); // display error message 

                }
                else if (user_response.ToUpper() == "H") // if user enters the Help command 
                {
                    /* List out all of the available commands and their associated descriptions */

                    Console.WriteLine($"{"Command",-10}" + $"{"Description",-20}");
                    Console.WriteLine($"{"C",-10}" + $"{"Create a new product",-20}");
                    Console.WriteLine($"{"D",-10}" + $"{"Delete an existing product",-20}");
                    Console.WriteLine($"{"H",-10}" + $"{"List of all available commands",-20}");
                    Console.WriteLine($"{"L",-10}" + $"{"List of all available products",-20}");
                    Console.WriteLine($"{"R",-10}" + $"{"Display the details of a specific product",-20}");
                    Console.WriteLine($"{"U",-10}" + $"{"Update the details associated with a specific product",-20}");
                    Console.WriteLine($"{"W",-10}" + $"{"Clears the console screen",-20} \n");

                }
                else if (user_response.ToUpper() == "C") // If user wants to create a new product 
                {
                    Console.WriteLine("Enter the details associated with the new product");

                    /* prompt the user for the relevant product details and perform data validation as soon as the data is received */

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
                                /* if all of the data received was valid, request that product manager add the new product to the list of products via the API */
                                await productManager.createNewProduct(product_id, product_name, product_category, Math.Round(product_price, 1));
                            }
                            else
                            {
                                /* Invalid product Price received */
                                Console.WriteLine("'{0}' Is not a valid Product Price. Product Price must be a positive real number \n", potential_product_price);
                            }

                        }
                        else
                        {
                            /* Invalid product Product Category received */
                            Console.WriteLine("'{0}' Is not a valid Product Category. Product Category must be a positive whole number \n", potential_product_category);
                        }

                    }
                    else
                    {
                        /* Invalid product Id received */
                        Console.WriteLine("'{0}' Is not a valid Product ID. Product ID must be a positive whole number \n", potential_product_id);
                    }

                }
                else if (user_response.ToUpper() == "U") // if user wants to update an existing product 
                {
                    Console.WriteLine("Enter the details of the product to update");

                    /* Prompt the user for the relevant product details and perform data validation as soon as the data is received */

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
                                /* If all of the received data was valid, call the productManager's updateProduct method to udpate details of existing product */
                                await productManager.updateProduct(product_id, product_name, product_category, Math.Round(product_price, 1));
                            }
                            else
                            {
                                /* Invalid product Price received */
                                Console.WriteLine("'{0}' Is not a valid Product Price. Product Price must be a positive real number \n", potential_product_price);
                            }

                        }
                        else
                        {
                            /* Invalid product Cateogory received */
                            Console.WriteLine("'{0}' Is not a valid Product Category. Product Category must be a positive whole number \n", potential_product_category);
                        }

                    }
                    else
                    {
                        /* Invalid product Id received */
                        Console.WriteLine("'{0}' Is not a valid Product ID. Product ID must be a positive whole number \n", potential_product_id);
                    }
                }
                else if (user_response.ToUpper() == "D") // if user wants to delete an existing product 
                {
                    /* Prompt user to enter the Id of the product that should be deleted */
                    Console.Write("Enter product ID associated with product you wish to delete: ");
                    string potential_product_id = Console.ReadLine();
                    int product_id;

                    if (int.TryParse(potential_product_id, out product_id) && product_id >= 0)
                    {
                        /* If the received product Id is valid, call the deleteProduct function */
                        await productManager.deleteProduct(product_id);
                    }
                    else
                    {
                        /* If invalid product Id was reveived */
                        Console.WriteLine("'{0}' Is not a valid Product ID. Product ID must be a positive whole number \n", potential_product_id);
                    }
                }
                else if (user_response.ToUpper() != "Q")
                {
                    /* If user eneterd an invalid command */
                    Console.WriteLine("'{0}' Is not a valid command. Enter the letter 'H' for a list of all available commands. \n", user_response);
                }
            }
        }

        static async Task RunUnitTests()
        {
            ProductManager productManager = new ProductManager("https://gendacproficiencytest.azurewebsites.net/API/ProductsAPI/", 0, 2, 0); // initalize instance of the Product Manager class
            await productManager.runUnitTests();
        }

    }
}
