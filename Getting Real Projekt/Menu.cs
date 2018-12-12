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

        private List<string> menuList = new List<string>()
        {
            "1. Opret reservation",
            "2. Køb af entre",
            "3. Vis data",
            "4. Vis specifik data",
            "5. Find reservation",
            "0. Afslut"
        };

        private Controller control = new Controller();

        private bool spWorked;

        public void Show()
        {
            bool running = true;
            Console.CursorVisible = false;

            while (running)
            {
                string selectedMenu = RunMenu(menuList);

                switch (selectedMenu)
                {
                    case "1. Opret reservation":
                        CreateReservation();
                        break;
                    case "2. Køb af entre":
                        CreateEntry();
                        break;
                    case "3. Vis data":
                        ReadData();
                        break;
                    case "4. Vis specifik data":
                        ShowSpecificData();
                        break;
                    case "5. Find reservation":
                        FindReservation();
                        break;
                    case "0. Afslut":
                        running = false;
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
                if (index < menu.Count -1)
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
            if (spWorked == true)
            {
                Console.WriteLine("Operationen udført uden fejl");
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("FEJL");
                Console.WriteLine("\n");
            }

            spWorked = false;
        }

        public void CreateEntry()
        {
            Console.WriteLine(">>> Køb af entre <<<");
            Console.WriteLine("\n");
            DateTime date = DateTime.Now;
            spWorked = control.InsertEntry(date);
            Console.WriteLine("Dato " + date);
            Console.WriteLine("\n");
            if (spWorked == true)
            {
                Console.WriteLine("Operationen udført uden fejl");
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("FEJL");
                Console.WriteLine("\n");
            }

            spWorked = false;
        }

        public void ReadData()
        {
            Console.WriteLine(">>> Vis data <<<");
            Console.WriteLine("\n");
            spWorked = control.ReadData();
            Console.WriteLine("\n");
            if (spWorked == true)
            {
                Console.WriteLine("Operationen udført uden fejl");
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("FEJL");
                Console.WriteLine("\n");
            }
                

            spWorked = false;
        }

        public void ShowSpecificData()
        {
            Console.WriteLine(">>> Vis specifik data <<<");
            Console.WriteLine("\n");
            Console.WriteLine("Indsæt dato");
            Console.WriteLine("yyyy-MM-dd hh:mm");
            Console.CursorVisible = true;
            time = Console.ReadLine();
            DateTime date = DateTime.Parse(time);
            Console.WriteLine("\n");
            spWorked=control.ReadSpecificData(date);
            Console.WriteLine("\n");
            Console.CursorVisible = false;
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

        public void FindReservation()
        {
            Console.WriteLine(">>> Find reservationer <<<");
            Console.WriteLine("\n");
            Console.WriteLine("Reservationer i dag? tryk 1, alle reservationer tryk 2");
            Console.CursorVisible = true;
            string input;
            string dat;
            input = Console.ReadLine();
            Console.WriteLine("\n");

            switch (input)
            {
                case "1":
                    dat = DateTime.Today.ToString("yyyy-MM-dd");
                    spWorked = control.FindReservation(DateTime.Parse(dat));
                    Console.WriteLine("\n");
                    break;
                case "2":
                    spWorked = control.FindReservation(DateTime.Parse("1910-01-01"));
                    Console.WriteLine("\n");
                    break;
            }
            Console.CursorVisible = false;
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
    }
}
