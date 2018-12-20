using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getting_Real_Projekt
{
    public class Menu
    {
        private static int index = 0;
        List<Product> p = new List<Product>();

        private List<string> menuList = new List<string>()
        {
            "1. Køb af produkt",
            "2. Find reservation",
            "3. Nye priser",
            "4. Total solgt på en dag",
            "0. Afslut"
        };

        private List<string> buyProductMenu = new List<string>()
        {
            "1. Entre",
            "2. Restaurent menu",
            "3. Opret reservation",
            "0. Afslut"
        };
        private List<string> newPriceMenu = new List<string>()
        {
            "1. Entre",
            "2. Restaurent menu",
            "0. Afslut"
        };

        private Controller control = new Controller();

        private bool spWorked;

        public void Show()
        {
            if (p.Count == 0)
            {
                GetProducts();
            }
            bool running = true;
            Console.CursorVisible = false;
            
            while (running)
            {
                string selectedMenu = RunMenu(menuList);

                switch (selectedMenu)
                {
                    case "1. Køb af produkt":
                        secondMenu();
                        break;
                    case "2. Find reservation":
                        FindReservation();
                        break;
                    case "3. Nye priser":
                        thirdMenu();
                        break;
                    case "4. Total solgt på en dag":
                        FindTotalPurchases();
                        break;
                    case "0. Afslut":
                        running = false;
                        break;
                }
            }
        }
        public void thirdMenu()
        {
            double input;
            bool running = true;
            Console.CursorVisible = false;
            while (running)
            {
                string SelectedMenu = RunMenu(newPriceMenu);
                switch (SelectedMenu)
                {
                    case "1. Entre":
                        Console.WriteLine("Hvad skal den nye pris for entre være?");
                        Console.CursorVisible = true;
                        input = double.Parse(Console.ReadLine());
                        
                        ChangePrice(p[0], input);
                        Console.CursorVisible = false;
                        break;
                    case "2. Restaurent menu":
                        Console.WriteLine("Hvad skal den pris på menu'en være?");
                        Console.CursorVisible = true;
                        input = double.Parse(Console.ReadLine());
                        ChangePrice(p[1], input);
                        Console.CursorVisible = false;
                        break;
                    case "0. Afslut":
                        Show();
                        break;
                }
            }
        }
        
        public void secondMenu()
        {
            bool running = true;
            Console.CursorVisible = false;
            while (running)
            {
                string SelectedMenu = RunMenu(buyProductMenu);
                switch (SelectedMenu)
                {
                    case "1. Entre":
                        CreateEntry();
                        break;
                    case "2. Restaurent menu":
                        BuyProduct();
                        break;
                    case "3. Opret reservation":
                        CreateReservation();
                        break;
                    case "0. Afslut":
                        Show();
                        break;
                }
            }
        }

        private string RunMenu(List<string> menu)
        {
            for (int i = 0; i < menu.Count; i++)
            {
               if (i.Equals(index))
               {
                   Console.BackgroundColor = ConsoleColor.Gray;
                   Console.ForegroundColor = ConsoleColor.Black;
                   Console.WriteLine(menu[i]);
               }
               else
               {
                   Console.WriteLine(menu[i]);
               }
               Console.ResetColor();
            }
            Console.WriteLine("\n");
            Console.WriteLine("---------------");

            ConsoleKeyInfo ckey = Console.ReadKey();

            if (ckey.Key.Equals(ConsoleKey.DownArrow))
            {
                if (index < menu.Count - 1)
                {
                    index++;
                }
            }
            else if (ckey.Key.Equals(ConsoleKey.UpArrow))
            {
                if (index >= 1)
                {
                    index--;
                }
            }
            else if (ckey.Key.Equals(ConsoleKey.Enter))
            {
                return menu[index];
            }
            Console.Clear();
            return "";
        }

        string name;
        string tlf;
        string time;
        int numberOfPersons;

        public void CreateReservation()
        {
            Console.WriteLine(">>> Opret reservation <<<");
            Console.WriteLine("\n");
            Console.WriteLine("Kundens Navn");
            Console.CursorVisible = true;
            name = Console.ReadLine();
            Console.WriteLine("Kundens tlf nummer");
            tlf = Console.ReadLine();
            Console.WriteLine("Dato for reservation");
            Console.WriteLine("yyyy-MM-dd hh:mm");
            time = Console.ReadLine();
            DateTime date = DateTime.Parse(time);
            Console.WriteLine("Antal personer");
            numberOfPersons = int.Parse(Console.ReadLine());
            spWorked =  control.InsertReservation(name, tlf, date, numberOfPersons);
            Console.WriteLine("\n");
            Console.WriteLine("Reservation er nu oprettet til den: " + time);
            Console.CursorVisible = false;
            SpCheck();
        }

        public void CreateEntry()
        {
            Console.WriteLine(">>> Køb af entre <<<");
            Console.WriteLine("\n");
            DateTime date = DateTime.Now;
            Console.WriteLine("Hvor mange? ");
            Console.CursorVisible = true;
            int amount = int.Parse(Console.ReadLine());
            double total = 0.0;
            total = amount * p[0].Price;
            spWorked = control.InsertEntry(date, amount,total);
            Console.WriteLine(total.ToString());
            Console.WriteLine("\n");
            Console.WriteLine("Der er købt " + amount + " entre på dato'en " + date);
            Console.WriteLine("\n");
            Console.CursorVisible = false;
            SpCheck();
        }

        public void ReadData()
        {
            Console.WriteLine(">>> Vis data <<<");
            Console.WriteLine("\n");
            spWorked = control.ReadData();
            Console.WriteLine("\n");
            SpCheck();
        }

        public void ShowSpecificData()
        {
            Console.WriteLine(">>> Vis specifik data <<<");
            Console.WriteLine("\n");
            Console.WriteLine("Indsæt dato");
            Console.WriteLine("yyyy-MM-dd");
            Console.CursorVisible = true;
            time = Console.ReadLine();
            DateTime date = DateTime.Parse(time);
            Console.WriteLine("\n");
            spWorked=control.ReadSpecificData(date);
            Console.WriteLine("\n");
            Console.CursorVisible = false;
            SpCheck();
        }

        public void FindReservation()
        {
            Console.WriteLine(">>> Find reservationer <<<");
            Console.WriteLine("\n");
            Console.WriteLine("1.Søg Efter i Dag\n2.Søg Efter Navn\n3.Søg Efter Dato og Navn\n4.Søg Efter Dato\n5 Vis Alle Reservationer");
            Console.CursorVisible = true;
            string input;
            string dat;
            string name;
            input = Console.ReadLine();
            Console.WriteLine("\n");

            switch (input)
            {
                case "1":
                    dat = DateTime.Today.ToString("yyyy-MM-dd");
                    Console.Clear();
                    spWorked = control.FindReservation(DateTime.Parse(dat));
                    Console.WriteLine("\n");
                    break;
                case "2":
                    Console.WriteLine("Skriv et Navn");
                    name = Console.ReadLine();
                    Console.Clear();
                    spWorked = control.FindReservation(DateTime.Parse("1910-01-01"),name);
                    Console.WriteLine("\n");
                    break;
                case "3":
                    Console.WriteLine("yyyy-MM-dd");
                    dat = Console.ReadLine();
                    dat = DateTime.Today.ToString("yyyy-MM-dd");
                    Console.WriteLine("Skriv et Navn");
                    name = Console.ReadLine();
                    Console.Clear();
                    spWorked = control.FindReservation(DateTime.Parse(dat), name);
                    Console.WriteLine("\n");
                    break;
                case "4":
                    Console.WriteLine("yyyy-MM-dd");
                    dat = Console.ReadLine();
                    Console.Clear();
                    spWorked = control.FindReservation(DateTime.Parse(dat));
                    Console.WriteLine("\n");
                    break;
                case "5":
                    Console.Clear();
                    spWorked = control.FindReservation(DateTime.Parse("1910-01-01"));
                    Console.WriteLine("\n");
                    break;
            }
            Console.CursorVisible = false;
            SpCheck();
        }
        public void FindTotalPurchases()
        {
            Console.WriteLine(">>> Find Total Køb Ved Dato <<<");
            Console.WriteLine("\n");
            Console.WriteLine("yyyy-MM-dd");
            string dat;

            dat = Console.ReadLine();
            Console.Clear();
            spWorked = control.FindTotalPurchases(DateTime.Parse(dat));
            Console.WriteLine("\n");
            Console.CursorVisible = false;
            SpCheck();
        }

        public void ChangePrice(Product pro, double newPrice) => control.ChangePrices(pro, newPrice);
        
        public void SpCheck()
        {
            if (spWorked == true)
            {
                Console.WriteLine("Operation udført uden fejl");
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("FEJL");
                Console.WriteLine("\n");
            }
            spWorked = false;
        }

        public void GetProducts()
        {
            p = control.GetProducts();
        }

        public void BuyProduct()
        {
            if (p.Count == 0)
            {
                GetProducts();
            }
            Console.WriteLine(">>> Køb af Menu <<<");
            Console.WriteLine("\n");
            DateTime date = DateTime.Now;
            int productId = 0;
            for (int i = 0; i < p.Count; i++)
            {
                if (p[i].Name == "MENU")
                {
                    productId = i;
                }
                
            }
            
            Console.WriteLine("Hvor mange? ");
            
            Console.CursorVisible = true;
            int amount = int.Parse(Console.ReadLine());
            double total = 0.0;
            double test = p[productId].Price;
            total = amount * p[productId].Price;
            spWorked = control.BuyProduct(date, amount, total,5);
            Console.WriteLine("\n");
            Console.WriteLine("Der er købt " + amount +  " " + p[productId].Name + " på dato'en " + date);
            Console.WriteLine("\n");
            Console.CursorVisible = false;
            SpCheck();
        }
    }
}
