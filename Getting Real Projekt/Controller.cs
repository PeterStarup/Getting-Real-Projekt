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
        public void InsertReservation(string name, string tlfNumber, DateTime timeOfReservation, int numberOfPersons)
        {
            Sqlcontroller.InsertReservation(name, tlfNumber, timeOfReservation, numberOfPersons);
        }
        public void InsertEntry(DateTime date)
        {
            Sqlcontroller.InsertEntry(date);
        }
        public void ReadData()
        {
            Sqlcontroller.ReadData();
        }
        public void ReadSpecificData(DateTime date)
        {
            Sqlcontroller.ReadSpecificData(date);
        }
    }
}
