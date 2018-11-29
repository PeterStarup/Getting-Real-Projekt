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
            "4. Exit"
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
                        KøbAfEntreOgMenu();
                        break;
                    case "4. Exit":
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
            Console.WriteLine("Dato'en for køb eks. 2008, 5, 1, 8, 30, 52 hvor det er år, måned, dag, time, minutter og sekunder");
            Console.CursorVisible = true;
            time = Console.ReadLine();
            DateTime date = DateTime.Parse(time);
            control.InsertEntry(date);
            Console.ReadKey();
            Console.CursorVisible = false;
            Console.Clear();
        }

        public void KøbAfEntreOgMenu()
        {
            Console.WriteLine("Woot");
        }
    }
}
