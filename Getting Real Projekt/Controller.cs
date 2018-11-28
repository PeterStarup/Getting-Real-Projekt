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
        public void InsertReservation(string name, string tlfNumber, DateTime timeOfReservation)
        {
            Sqlcontroller.InsertReservation(name, tlfNumber, timeOfReservation);
        }
        public void InsertEntry(DateTime date)
        {
            Sqlcontroller.InsertEntry(date);
        }
        public void InsertEntryAndReservation(string name, string tlfNumber, DateTime timeOfReservation, DateTime date)
        {
            Sqlcontroller.InsertEntryAndReservation(name, tlfNumber, timeOfReservation);
            Sqlcontroller.InsertEntry(date);
        }
        public void ReadData()
        {
            Sqlcontroller.ReadData();
        }
        public void ReadSpecificData(string row)
        {
            Sqlcontroller.ReadSpecificData(row);
        }
    }
}
