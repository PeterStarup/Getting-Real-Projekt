using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Getting_Real_Projekt
{
    public class SQLController
    {
        
        private static string connectionString =
        "Server = ealSQL1.eal.local; Database = A_DB26_2018; User Id = A_STUDENT26; Password = A_OPENDB26;";
        
        private bool spWorked; //Check if spWorked
        public bool InsertReservation(string name, string tlfNumber, DateTime timeOfReservation, int antalPersoner)
        {
            //Using to make sure that connection closes after use
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand cmd1 = new SqlCommand("spGRInsertTableRes", con);
                    cmd1.CommandType = CommandType.StoredProcedure; //SP

                    cmd1.Parameters.Add(new SqlParameter("@CustomerName", name));
                    cmd1.Parameters.Add(new SqlParameter("@CustomerTLF", tlfNumber));
                    cmd1.Parameters.Add(new SqlParameter("@ReservationDate", timeOfReservation));
                    cmd1.Parameters.Add(new SqlParameter("@NumberOfPersons", antalPersoner));

                    cmd1.ExecuteNonQuery();
                    spWorked = true;
                    return spWorked;
                }
                catch (SqlException e)
                {
                    spWorked = false;
                    Console.WriteLine("Ups " + e.Message);
                }
            }
            
            return spWorked;
        }
        public bool InsertEntry(DateTime date, int numberofitems,double total)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("spGRInsertPurchaseWithTransaction", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@PurchaseDate", date));
                    cmd.Parameters.Add(new SqlParameter("@NumberOfItems", numberofitems));
                    cmd.Parameters.Add(new SqlParameter("@Total", total));

                    cmd.ExecuteNonQuery();
                    spWorked = true;
                    return spWorked;
                }
                catch (SqlException e)
                {
                    spWorked = false;
                    Console.WriteLine("Woopsi " + e.Message);
                }
            }

            
            return spWorked;
        }
        public bool BuyProduct(DateTime date, int numberofitems, double total, int productId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("spGRInsertPurchaseWithTransaction", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@PurchaseDate", date));
                    cmd.Parameters.Add(new SqlParameter("@NumberOfItems", numberofitems));
                    cmd.Parameters.Add(new SqlParameter("@Total", total));
                    cmd.Parameters.Add(new SqlParameter("@ProductId", productId));

                    cmd.ExecuteNonQuery();
                    spWorked = true;
                    return spWorked;
                }
                catch (SqlException e)
                {
                    spWorked = false;
                    Console.WriteLine("Woopsi " + e.Message);
                }
            }


            return spWorked;
        }
        public bool ReadData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("ReadData", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader read = cmd.ExecuteReader();
                    
                    

                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            string id = read["PurchaseId"].ToString();
                            string date = read["PurchaseDate"].ToString();
                            DateTime datetime = DateTime.Parse(date);
                            date = datetime.ToString("yyyy-MM-dd");
                            string numberofitems = read["NumberOfItems"].ToString();
                            Console.WriteLine("Id: " + id + "| Purchase date: " + date + "| Number of items: " + numberofitems);
                        }
                    }
                    if (!read.HasRows)
                    {
                        spWorked = false;
                        Console.WriteLine("Intet Data fundet");
                        return spWorked;
                    }
                    spWorked = true;
                    return spWorked;

                }
                catch (SqlException e)
                {
                    spWorked = false;
                    Console.WriteLine("Duuuhh " + e.Message);
                }
            }

            
            return spWorked;
        }
        public bool ReadSpecificData(DateTime date)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("ReadSpecData", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Dato", date));

                    SqlDataReader read = cmd.ExecuteReader();

                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            string id = read["PurchaseId"].ToString();
                            string dat = read["PurchaseDate"].ToString();
                            DateTime datetime = DateTime.Parse(dat);
                            dat = datetime.ToString("yyyy-MM-dd hh:mm");
                            string numberofitems = read["NumberOfItems"].ToString();
                            Console.WriteLine("Id: " + id + "| Purchase date: " + dat + "| Number of items: " + numberofitems);
                        }
                    }
                    if (!read.HasRows)
                    {
                        spWorked = false;
                        Console.WriteLine("Intet Data fundet");
                        return spWorked;
                    }
                    spWorked = true;
                    return spWorked;
                }
                catch (SqlException e)
                {
                    spWorked = false;
                    Console.WriteLine("Duuude " + e.Message);
                }
                catch (System.Data.SqlTypes.SqlTypeException e)
                {
                    spWorked = false;
                    Console.WriteLine("Skriv en dato mellem 1753-01-01 og 9999-12-12\n" + e.Message);
                }
            }

           
            return spWorked;
        }

        public bool FindReservation(DateTime date, string customerName ="")
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("spFindReservation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Date", date));
                    cmd.Parameters.Add(new SqlParameter("@CustomerName", customerName));

                    SqlDataReader read = cmd.ExecuteReader();

                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            string resdate = read["ResDate"].ToString();
                            string customername = read["CustomerName"].ToString();
                            string customertlf = read["CustomerTLF"].ToString();
                            string seats = read["Seats"].ToString();
                            Console.WriteLine("Date: " + resdate + "| Customer name: " + customername + "| Customer TLF: " + customertlf + "| Number of seats: " + seats);
                        }
                    }

                    if (!read.HasRows)
                    {
                        spWorked = false;
                        Console.WriteLine("Intet Data fundet");
                        return spWorked;
                    }
                    spWorked = true;
                    return spWorked;
                }
                catch (SqlException e)
                {
                    spWorked = false;
                    Console.WriteLine("What? " + e.Message);
                }
            }
            return spWorked;
        }
        public bool FindTotalPuchases(DateTime d)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("spGRTEST", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Date", d);

                    SqlDataReader read = cmd.ExecuteReader();
                    double totalPris = 0;
                    if(read.HasRows)
                    {
                        Console.WriteLine(String.Format("|{0,-20}|{1,-20}|{2,-20}|", "Produkt navn:", "Antal:", "Pris:"));
                        while (read.Read())
                        {
                            string productName = read["ProductName"].ToString();
                            string productPrice = read["TotalPris"].ToString();
                            string numberOfItems = read["NumberOfItems"].ToString();
                            Console.WriteLine(String.Format("|{0,-20}|{1,-20}|{2,-20}|", productName, numberOfItems, productPrice));
                            totalPris += double.Parse(productPrice);
                        }
                        Console.WriteLine(String.Format("|{0,41}|{1,-20}|", ">>>>Total Pris<<<<:", totalPris));
                    }

                    if (!read.HasRows)
                    {
                        spWorked = false;
                        Console.WriteLine("Intet Data fundet");
                        return spWorked;
                    }

                    spWorked = true;
                    return spWorked;
                }
                catch (SqlException e)
                {
                    spWorked = false;
                    Console.WriteLine("What? " + e.Message);

                }
                catch (System.Data.SqlTypes.SqlTypeException e)
                {
                    spWorked = false;
                    Console.WriteLine("Skriv en dato mellem 1753-01-01 og 9999-12-12\n" + e.Message);
                }
                return spWorked;
            }

        }

        public List<Product> GetProducts()
        {
            List<Product> p = new List<Product>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    
                    SqlCommand cmd = new SqlCommand("spGRShowAllProducts", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader read = cmd.ExecuteReader();

                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            
                            string productName = read["ProductName"].ToString();
                            string productPrice = read["ProductPrice"].ToString() + ".0";
                            string productId = read["ProductId"].ToString();
                            double proId = double.Parse(productId, System.Globalization.CultureInfo.InvariantCulture);
                            double douprice = double.Parse(productPrice, System.Globalization.CultureInfo.InvariantCulture);
                            p.Add(new Product { Name = productName, Price = douprice, Id = proId });
                        }
                    }
                    spWorked = true;
                    return p;
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Hej " + e.Message);
                }
                return p;
            }
        }
       
    }
}
