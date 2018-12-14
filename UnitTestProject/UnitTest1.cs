using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Getting_Real_Projekt;


namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        SQLController sqlController;
        DateTime date1;
        DateTime date2= DateTime.Now;
        [TestInitialize]
        public void CreateNewObject()
        {
            sqlController = new SQLController();
            date1 = new DateTime(2008, 12, 1, 18, 30, 52);
            date2 = DateTime.Now;
        }

       
        [TestMethod]
        public void InsertEntry()
        {
            Assert.AreEqual(true, sqlController.InsertEntry(date2, 1,1.0));
        }
        [TestMethod]
        public void InsertReservation()
        {
            Assert.AreEqual(true, sqlController.InsertReservation("unittest,", "unittest", date2, 6));
        }
        [TestMethod]
        public void ReadData()
        {
            Assert.AreEqual(true, sqlController.ReadData());
        }
        [TestMethod]
        public void ReadSpecificData()
        {
            Assert.AreEqual(true, sqlController.ReadSpecificData(date1));
        }
    }
}
