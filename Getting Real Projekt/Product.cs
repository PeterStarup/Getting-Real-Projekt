using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getting_Real_Projekt
{
    public class Product
    {
        private string name;
        private string price;
        public List<Product> products = new List<Product>();

        public void AddProduct(Product p)
        {
            products.Add(p);
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
    }
}
