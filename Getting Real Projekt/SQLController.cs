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
        public bool InsertEntry(DateTime date)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("spGRInsertEntry", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@PurchaseDate", date));

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

                    SqlCommand cmd = new SqlCommand("spGRShowPurchasesByDate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Date", d);

                    SqlDataReader read = cmd.ExecuteReader();

                    if(read.HasRows)
                    {
                        while (read.Read())
                        {
                            string productName = read["ProductName"].ToString();
                            string productPrice = read["ProductPrice"].ToString();
                            string numberOfItems = read["SumOfItems"].ToString();

                            Console.WriteLine($"Product name: {productName}| Product price: {productPrice}| Number of items: {numberOfItems}");
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
                catch (System.Data.SqlTypes.SqlTypeException e)
                {
                    spWorked = false;
                    Console.WriteLine("Skriv en dato mellem 1753-01-01 og 9999-12-12\n" + e.Message);
                }
                return spWorked;
            }

        }
       
    }
}
