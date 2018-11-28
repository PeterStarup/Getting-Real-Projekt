using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getting_Real_Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pro = new Program();
            pro.Run();
        }

        void Run()
        {
            Menu menu = new Menu();
            menu.Show();
        }
    }
}
