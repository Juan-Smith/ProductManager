using System;
using System.Text;
using Newtonsoft.Json;
using ItemClass;
using System.Net;


namespace ProductManagerClass
{
    /// <summary>
    /// The Product Manager class is resonsible for managing the API connection from the client side. The product manager class contains a list of relevant methods which can be used to add, update, view or remove products via the provided API 
    /// </summary>
    public class ProductManager
    {

        private HttpClient product_manager_client; // HttpClient which will communicate with the API 

        /// <summary>
        /// initialize an instance of the Product Manager class 
        /// </summary>
        /// <param name="base_address">The base address associated with API requests</param>
        /// <param name="timout_hour_count">Number of hours before a request times out</param>
        /// <param name="timeout_minute_count">Number of minutes before a request times out</param>
        /// <param name="timeout_second_count">Number of seconds before a request times out</param>
        public ProductManager(string base_address, int timout_hour_count, int timeout_minute_count, int timeout_second_count)
        {
            this.product_manager_client = new HttpClient();
            this.product_manager_client.BaseAddress = new Uri(base_address);
            this.product_manager_client.Timeout = new TimeSpan(timout_hour_count, timeout_minute_count, timeout_second_count); 
        }

        /// <summary>
        /// Print a list of all of the available products to the console screen 
        /// </summary>
        public async Task listAllProducts()
        {
            Console.WriteLine("Retrieving list of products...");

            string response = await this.product_manager_client.GetStringAsync(""); // request list of all products via the API  

            List<Item> item_list = JsonConvert.DeserializeObject<List<Item>>(response); // convert the received json data into a list of Item class instances stored in a list
                                                                                        
            Console.WriteLine($"{"Id",-10}" + $"{"Product Name",-20}" + $"{"Category",20}" + $"{"Price",10}"); // print out header with all of the product details

            foreach (Item item in item_list)
                Console.WriteLine($"{item.Id,-10}" + $"{item.Name,-20}" + $"{item.Category,20}" + $"{item.Price,10}"); // print out the details associated with each retrieved item
            
            Console.WriteLine();
        }

        /// <summary>
        /// Retrieve the details of a specific product given the product's unique ID number
        /// </summary>
        /// <param name="product_id">The id number associated with the product</param>
        public async Task viewProductDetails(string product_id)
        {
            Console.WriteLine("Retrieving product details...");

            try
            {
                /* Try retrieving the details associated with the specific product */

                string response = await this.product_manager_client.GetStringAsync(product_id); // retrieve product details via API 
                Item item = JsonConvert.DeserializeObject<Item>(response);  // convert the received json data into a list of Item class instances stored in a list

                Console.WriteLine($"{"Id",-10}" + $"{"Product Name",-20}" + $"{"Category",20}" + $"{"Price",10}"); // print out header with all of the product details
                Console.WriteLine($"{item.Id,-10}" + $"{item.Name,-20}" + $"{item.Category,20}" + $"{item.Price,10} \n"); // print details associated with retrived product
            }
            catch(Exception ex)
            {
                /* Manage any exceptions which occur whilst trying to retrieve product details */ 

                Console.WriteLine(); 
                Console.WriteLine("An error occured while trying to retrieve product details."); 
                Console.WriteLine(ex.Message + "\n");
            }
            
        }

        /// <summary>
        /// Create a new product and add it to the list via an API call
        /// </summary>
        /// <param name="product_id">Id number of the product</param>
        /// <param name="product_name">Name of the product</param>
        /// <param name="product_category">Category of the product</param>
        /// <param name="product_price">Price of the product</param>
        public async Task createNewProduct(int product_id, string product_name, int product_category, double product_price)
        {

            try
            {
                /* Try and add product to list via an API call */ 

                Item new_item = new Item(product_id, product_name, product_category, product_price); // instantiate new instance of the item class using provided parameters
                string json_item = JsonConvert.SerializeObject(new_item); // convert the item class instance into json data
                StringContent content = new StringContent(json_item, Encoding.UTF8, "application/json"); 
                HttpResponseMessage result = await this.product_manager_client.PostAsync("", content); // sent POST request to API to add item to list of products

                /* Display whether or not the item was successfully added to the list */ 
                if (result.IsSuccessStatusCode)
                    Console.WriteLine("Successfully added product to the list. \n");
                else
                    Console.WriteLine("Could not add product to list. Product with the same name already exists.\n"); 
            }
            catch(Exception ex)
            {
                /* Handle any exceptions which occur whilst trying to add product to list */ 
                Console.WriteLine();
                Console.WriteLine("An error occured while trying to add product to list.");
                Console.WriteLine(ex.Message + "\n");
            }
        }

        /// <summary>
        /// Update the details associated with a specific product 
        /// </summary>
        /// <param name="product_id">Id number of the product</param>
        /// <param name="product_name">Name of the product</param>
        /// <param name="product_category">Category of the product</param>
        /// <param name="product_price">Price of the product</param>
        public async Task updateProduct(int product_id, string product_name, int product_category, double product_price)
        {
            try
            {
                /* try and and update the details associated with an existing product */ 

                
                HttpResponseMessage deletion_result = await this.product_manager_client.DeleteAsync(product_id.ToString()); // delete the existing product with that id 
                if (deletion_result.IsSuccessStatusCode) // if the deletion of the existing product was successfull 
                {
                    /* Add a new product with the relevant product details */  
                    Item new_item = new Item(product_id, product_name, product_category, product_price); // instantiate new instance of the item class using the relevant details 
                    string json_item = JsonConvert.SerializeObject(new_item); // convert item class instance to json data 
                    StringContent content = new StringContent(json_item, Encoding.UTF8, "application/json");
                    HttpResponseMessage result = await this.product_manager_client.PostAsync(product_id.ToString(), content); // add the item to the existing list of products 

                    /* Display whether or not the item was successfully updated */
                    if (result.IsSuccessStatusCode)
                        Console.WriteLine("Successfully updated product details. \n");
                    else
                        Console.WriteLine("Could not update product details. \n");
                }
                else // if the deletion was not successfull
                {
                    Console.WriteLine("Could not update product details. No product with id {0} found. \n", product_id.ToString()); 
                }

            }
            catch(Exception ex)
            {
                /* Handle any exceptions which occured whilst trying to update the details of an existing product */ 

                Console.WriteLine();
                Console.WriteLine("An error occured while trying to update the product's details.");
                Console.WriteLine(ex.Message + "\n");
            }
        }

        /// <summary>
        /// Delete an existing product from the list when provided with a product's Id
        /// </summary>
        /// <param name="product_id">Id number of the product</param>
        public async Task deleteProduct(int product_id)
        {
            try
            {
                /* Try and delete the product with the specified id */ 

                HttpResponseMessage deletion_result = await this.product_manager_client.DeleteAsync(product_id.ToString()); // send DELETE request to API for specified product 
                if (deletion_result.IsSuccessStatusCode) 
                {
                    /* if deletion was successfull display a success message */ 
                    Console.WriteLine("Successfully deleted product from the list. \n"); 
                }
                else
                {
                    /* if deletion was unsuccessfull display an error message */ 
                    Console.WriteLine("Could not update product details. No product with id {0} found. \n", product_id.ToString());
                }
                    

            }
            catch (Exception ex)
            {
                /* Handle any exepctions which may occur whilst trying to delete the desired product */ 

                Console.WriteLine();
                Console.WriteLine("An error occured while trying to update the product's details.");
                Console.WriteLine(ex.Message + "\n");
            }
        }
        /// <summary>
        /// Run the unit tests for the Product Manager class
        /// </summary>
        public async Task runUnitTests()
        {
          // await testAllProducts();
            await testRetrieveProduct();
            await testAddProduct(); 
            await testUpdateProduct();
            await testDeleteProduct(); 
        }

        private async Task testAllProducts()
        {
            Console.WriteLine("Program should successfully return list of all availabe products");
            await listAllProducts(); 
        }

        private async Task testRetrieveProduct()
        {
            Console.WriteLine(); 
            Console.WriteLine("=========================================================");
            Console.WriteLine("TESTING RETRIEVAL OF PRODUCTS");
            Console.WriteLine("=========================================================");
            Console.WriteLine(); 

            List<object> test_product_ids = new List<object>{ 100, 189, -1, 10, 3891, 56, 10000000, "s", "adaf", 391};
            List<string> test_case_statuses = new List<string> { "Success", "Success", "Fail", "Fail", "Success", "Success", "Fail", "Fail", "Fail", "Success" }; 
            for(int i =0; i < test_case_statuses.Count; i++)
            {
                Console.WriteLine("Test case {0}. Status should be: {1}", i, test_case_statuses[i]); 
                await viewProductDetails(test_product_ids[i].ToString());
            }
        }

        private async Task testAddProduct()
        {

            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine("TESTING ADDING OF PRODUCTS");
            Console.WriteLine("=========================================================");
            Console.WriteLine();

            List<int> test_product_ids = new List<int> {10, 1000, 10000, 10000, 10000, 10000}; 
            List<string> test_product_names = new List<string> {"Product ABC12345", "Product XYZ12345", "Product XYZ12345", "Product TQR09875", "Product MNQ12345", "Product ABC12345"};
            List<int> test_product_categories = new List<int> {1, 7, 3, 0, 1, 4};
            List<double> test_product_prices = new List<double> {100, 45, 2300, 150, 12, 900};
            List<string> test_case_statuses = new List<string> {"Success", "Success", "Fail", "Fail", "Success", "Fail"};
            for (int i = 0; i < test_case_statuses.Count; i++)
            {
                Console.WriteLine("Test case {0}. Status should be: {1}", i, test_case_statuses[i]);
                await createNewProduct(test_product_ids[i], test_product_names[i], test_product_categories[i], test_product_prices[i]); 
            }
        }

        private async Task testUpdateProduct()
        {
            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine("TESTING UPDATING OF PRODUCTS");
            Console.WriteLine("=========================================================");
            Console.WriteLine();

            List<int> test_product_ids = new List<int> { 1487, -1, 5845, 9000, 1567, 10000000 };
            List<string> test_product_names = new List<string> { "Product THQA1234", "Product JKAPQ11234", "Product XYZ112345", "Product MVNA19193", "Product KWKQ11595", "Product ABC112345" };
            List<int> test_product_categories = new List<int> { 1, 7, 3, 0, 1, 4 };
            List<double> test_product_prices = new List<double> { 100, 45, 2300, 150, 12, 900 };
            List<string> test_case_statuses = new List<string> { "Success", "Fail", "Fail", "Success", "Success", "Fail" };
            for (int i = 0; i < test_case_statuses.Count; i++)
            {
                Console.WriteLine("Test case {0}. Status should be: {1}", i, test_case_statuses[i]);
                await updateProduct(test_product_ids[i], test_product_names[i], test_product_categories[i], test_product_prices[i]);
            }
        }

        private async Task testDeleteProduct()
        {
            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine("TESTING DELETING OF PRODUCTS");
            Console.WriteLine("=========================================================");
            Console.WriteLine();

            List<int> test_product_ids = new List<int> { 10, 1000, 1589, 1459, 136, 147, 1487, -1, 5845, 9000, 1567, 10000000 };
            List<string> test_cse_statuses = new List<string> { "Fail", "Success", "Success", "Success", "Success", "Success", "Fail", "Fail", "Fail", "Fail", "Fail", "Fail" };
            for (int i = 0; i < test_cse_statuses.Count; i++)
            {
                Console.WriteLine("Test case {0}. Status should be: {1}", i, test_cse_statuses[i]);
                await deleteProduct(test_product_ids[i]);
            }

        }

    }
}

