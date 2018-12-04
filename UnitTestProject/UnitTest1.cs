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
        [TestInitialize]
        public void CreateNewObject()
        {
            sqlController = new SQLController();
            date1 = new DateTime(2008, 12, 1, 18, 30, 52);
        }


        [TestMethod]
        public void DoesMethodsExists()
        {
            Assert.AreEqual(false, sqlController.InsertEntry(date1));
            Assert.AreEqual(false, sqlController.InsertReservation("test,", "test", date1,1));
            Assert.AreEqual(false, sqlController.ReadData());
            Assert.AreEqual(false, sqlController.ReadSpecificData(date1));
        }
    }
}
