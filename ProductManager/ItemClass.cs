using System;


namespace ItemClass
{
    /// <summary>
    /// The Item class is responsible for storing all the relevant info of a product. This includes the associated Id, Name, Category and Price. 
    /// </summary>
    public class Item
    {
        // store the attributes assocaited with the item class
        public int Id { get; set; }
        public string Name { get; set; }
        public int Category { get; set; }
        public double Price { get; set; }


        /// <summary>
        /// Constructor for the Item class which takes in all of the information associated with a product
        /// </summary>
        /// <param name="id">Id number of the product</param>
        /// <param name="name">Name of the product</param>
        /// <param name="category">Category of the product</param>
        /// <param name="price">Price of the product</param>
        public Item(int id, string name, int category, double price)
        {
            /* Initialize all of the class instance's parameters using the provided input data */ 

            this.Id = id;
            this.Name = name;
            this.Category = category;
            this.Price = price; 
        }
    }

}

