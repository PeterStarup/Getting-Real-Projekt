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
            "1. Opret Reservation",
            "2. Køb af entre",
            "3. Køb af entre og menu",
            "4. Vis Data",
            "5. Vis specifik data",
            "0. Exit"
        };

        Controller control = new Controller();

        public void Show()
        {
            bool running = true;
            Console.CursorVisible = false;

            while (running)
            {
                string selectedMenu = RunMenu(menuList);

                switch (selectedMenu)
                {
                    case "1. Opret Reservation":
                        Opretreservation();
                        break;
                    case "2. Køb af entre":
                        KøbAfEntre();
                        break;
                    case "3. Køb af entre og menu":
                        //KøbAfEntreOgMenu();
                        break;
                    case "4. Vis Data":
                        VisData();
                        break;
                    case "5. Vis specifik data":
                        VisSpecData();
                        break;
                    case "0. Exit":
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

        public void Opretreservation()
        {
            Console.WriteLine("Kundens Navn");
            Console.CursorVisible = true;
            name = Console.ReadLine();
            Console.WriteLine("Kundens tlf");
            tlf = Console.ReadLine();
            Console.WriteLine("Dato for reservation eks. 2008, 5, 1, 8, 30, 52 hvor det er år, måned, dag, time, minutter og sekunder");
            time = Console.ReadLine();
            DateTime date = DateTime.Parse(time);
            Console.WriteLine("Antal personer");
            numberOfPersons = int.Parse(Console.ReadLine());
            control.InsertReservation(name, tlf, date, numberOfPersons);
            Console.ReadKey();
            Console.CursorVisible = false;
            Console.Clear();
        }

        public void KøbAfEntre()
        {
            DateTime date = DateTime.Now;
            control.InsertEntry(date);
            Console.WriteLine("Dato " + date);
            Console.WriteLine("\n");
        }

        //public void KøbAfEntreOgMenu()
        //{
        //    control.InsertEntryAndReservation();
        //}

        public void VisData()
        {
            control.ReadData();
            Console.WriteLine("\n");
        }

        public void VisSpecData()
        {
            Console.WriteLine("Indsæt dato");
            Console.WriteLine("eks: 2008-01-01");
            Console.CursorVisible = true;
            string da = Console.ReadLine();
            DateTime date = DateTime.Parse(da);
            control.ReadSpecificData(date);
            Console.CursorVisible = false;
        }
    }
}
