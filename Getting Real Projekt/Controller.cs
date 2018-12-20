using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getting_Real_Projekt
{
    public class Controller
    {
        SQLController Sqlcontroller = new SQLController();
        private bool spWorked;
        public bool InsertReservation(string name, string tlfNumber, DateTime timeOfReservation, int numberOfPersons)
        {
            return spWorked = Sqlcontroller.InsertReservation(name, tlfNumber, timeOfReservation, numberOfPersons);
        }
        public bool InsertEntry(DateTime date, int numberofitems,double total)
        {
            
            return spWorked= Sqlcontroller.InsertEntry(date, numberofitems, total);
        }
        public bool ReadData()
        {
            return spWorked = Sqlcontroller.ReadData();
        }
        public bool ReadSpecificData(DateTime date)
        {
            return spWorked = Sqlcontroller.ReadSpecificData(date);
        }
        public bool FindReservation(DateTime date,string customerName ="")
        {
            return spWorked = Sqlcontroller.FindReservation(date,customerName);
        }

        public bool FindTotalPurchases(DateTime d)
        {
            return spWorked = Sqlcontroller.FindTotalPuchases(d);
        }
        public List<Product> GetProducts()
        {
            return Sqlcontroller.GetProducts();
        }
        public bool BuyProduct(DateTime date, int numberofitems, double total, int productId)
        {
            return Sqlcontroller.BuyProduct(date, numberofitems, total, productId);
        }

        public void ChangePrices(Product pro, double newPrice)
        {
            pro.Price = newPrice;
        }
    }
}
