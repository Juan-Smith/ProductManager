using System;

/// <summary>
/// Summary description for Class1
/// </summary>

namespace ItemClass
{
    public class Item
    {
        // store the attributes assocaited with the item class
        public int Id { get; set; }
        public string Name { get; set; }
        public int Category { get; set; }
        public double Price { get; set; }

        public Item(int id, string name, int category, double price)
        {
            this.Id = id;
            this.Name = name;
            this.Category = category;
            this.Price = price; 
        }
    }

}

