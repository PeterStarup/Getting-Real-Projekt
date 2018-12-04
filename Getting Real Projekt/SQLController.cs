﻿using System;
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

        bool spWorked;
        public bool InsertReservation(string name, string tlfNumber, DateTime timeOfReservation, int antalPersoner)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand cmd1 = new SqlCommand("spGRInsertCustomer", con);
                    cmd1.CommandType = CommandType.StoredProcedure;

                    cmd1.Parameters.Add(new SqlParameter("@CustomerName", name));
                    cmd1.Parameters.Add(new SqlParameter("@CustomerTLF", tlfNumber));

                    cmd1.ExecuteNonQuery();
                    spWorked = true;
                    return spWorked;
                }
                catch (SqlException e)
                {

                    Console.WriteLine("Ups " + e.Message);
                }
            }
            spWorked = false;
            return spWorked;
        }
        public bool InsertEntry(DateTime date)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("spGRInsertPurchase", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@PurchaseDate", date));

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Woopsi " + e.Message);
                }
            }

            spWorked = false;
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
                            string numberofitems = read["NumberOfItems"].ToString();
                            Console.WriteLine("Id: " + id + "| Purchase date: " + date + "| Number of items: " + numberofitems);
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Duuuhh " + e.Message);
                }
            }

            spWorked = false;
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
                            string numberofitems = read["NumberOfItems"].ToString();
                            Console.WriteLine("Id: " + id + "| Purchase date: " + dat + "| Number of items: " + numberofitems);
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Duuude " + e.Message);
                }
            }

            spWorked = false;
            return spWorked;
        }
    }
}