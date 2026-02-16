using System.Data.Common;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int row = 8;
            int column = 10;
            int[] vipRow = [7, 8];
            int ticketPrice = 180;
            int vipTicketPrice = 250;
            SalKina Kino = new SalKina(row,column,vipRow,ticketPrice,vipTicketPrice);

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-!-|-Kino Rezervace 2026-|-!-\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Menu MainMenu = new Menu();
            while (true)
            {
                MainMenu.VypsatMoznosti();
                string input = Console.ReadLine();
                MainMenu.RunCommandFromInput(input);
            }
        }
        /*Program musí umožnit:
        TODO Zobrazit kinosál(řady × sedadla, volné / obsazené)
        TODO Rezervovat sedadlo/sedadla(jen to které existuje a je volné)
        done Ověřit, zda je sedadlo volné
        TODO Spočítat cenu lístku
        TODO Ukončit program
        TODO zobrazit hlavní menu akcí
        */
        class Menu
        {
            public Menu()
            {
                string[] CommandNames = new string[5];//{ "V", "R", "F", "C", "/" };
                CommandNames[0] = "V";
                CommandNames[0] = "R";
                CommandNames[0] = "F";
                CommandNames[0] = "C";
                CommandNames[0] = "/";
            }
            string[] CommandNames { get; set; }
            public void DefaultColors()
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            public void VypsatMoznosti()
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Dobrý den, chcete...\n[V] Zobrazit kinosál\n[R] Rezervovat sedadlo\n[F] Ověřit, zdali je sedadlo volné\n[C] Spočítat cenu lístku\n[/] Ukončit program\n");
                DefaultColors();
            }
            private bool IsInputValid(string input)
            {
                foreach (string command in CommandNames)
                    if (input == command)
                    {
                        return true;
                    }
                return false;
            }
            public void RunCommandFromInput(string input)
            {
            if (!IsInputValid(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"akce {input} neexistuje. Zkuste zadat akci, která existuje.\n");
                    DefaultColors();
                }
            }
        }
        class SalKina
        {
            public SalKina(int rady, int sloupce, int[]? vipRady, int listekCena, int vipListekCena)
            {
                bool[,] Sedadla = new bool[sloupce, rady];
                int PocetRad = rady;
                int PocetSloupcu = sloupce;
                int[]? VipRady = vipRady;
                int ListekCena = listekCena;
                int VipListekCena = vipListekCena;
            }
            public bool[,] Sedadla { get; set; }
            public int PocetRad { get; set; }
            public int PocetSloupcu { get; set; }
            public int[]? VipRady { get; set; }
            public int ListekCena { get; set; }
            public int VipListekCena { get; set; }

            public void PrintSalKina()
            {
                for (int i = 0; i < PocetSloupcu; i++)
                {
                    for (int j = 0; j < PocetRad; i++)
                    {
                        Console.Write($"|{Sedadla[i, j]}");
                    }
                }
                Console.WriteLine("|");
            }
            public bool isSedadloZabrane(int sloupec, int rada)
            {
                return Sedadla[sloupec, rada];
            }
        }
    }
}

