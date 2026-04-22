using System.ComponentModel;
using System.Text;
using static Connect4.Program;
namespace Connect4 //podporované znaky: ■▄▀█░▒▓┐┌─┘└│┤├┬┴┼││═║╒╓╔╕╖╗╘╙╚╛╜╝╞╟╠╡╢╣╤╥╦╧╨╩╪╫╬
{
    internal class Program
    {
        public static bool ReadAndCheckLine(string input)
        {
            try 
            {
                Convert.ToInt32(input);
                return true;
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Gamemaster (-■_■): Co sem píšeš? Zkus něco jiného!:");
                return false;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine(" _______________________________________________________________________________________________________________________\r\n \\      ________  ________  ________  ________  ________  ________     ____   ___      ___ ________  ________     ____  \\\r\n  \\    |\\   __  \\|\\   __  \\|\\   __  \\|\\   __  \\|\\   __  \\|\\   ___ \\   /___/| |\\  \\    /  /|\\   __  \\|\\   ____\\   /___/|  \\\r\n   \\   \\ \\  \\|\\  \\ \\  \\|\\  \\ \\  \\|\\  \\ \\  \\|\\  \\ \\  \\|\\  \\ \\  \\_|\\ \\  |___|/_\\ \\  \\  /  / | \\  \\|\\  \\ \\  \\___|\\  |___|/   \\\r\n    \\   \\ \\   ____\\ \\   _  _\\ \\  \\\\\\  \\ \\   ____\\ \\   __  \\ \\  \\ \\\\ \\|\\   __  \\ \\  \\/  / / \\ \\   __  \\ \\  \\    \\|\\  \\      \\\r\n     \\   \\ \\  \\___|\\ \\  \\\\  \\\\ \\  \\\\\\  \\ \\  \\___|\\ \\  \\ \\  \\ \\  \\_\\\\ \\ \\  \\|\\  \\ \\    / /   \\ \\  \\ \\  \\ \\  \\____\\ \\  \\      \\\r\n      \\   \\ \\__\\    \\ \\__\\\\ _\\\\ \\_______\\ \\__\\    \\ \\__\\ \\__\\ \\_______\\ \\   __  \\ \\__/ /     \\ \\__\\ \\__\\ \\_______\\ \\__\\      \\\r\n       \\   \\|__|     \\|__|\\|__|\\|_______|\\|__|     \\|__|\\|__|\\|_______|\\ \\__\\ \\__\\|__|/       \\|__|\\|__|\\|_______|\\|__|       \\\r\n        \\        ________  ___    __  __  ___  __    ___      ___ ______\\|__|_\\|__|__  ___  __       ___    ___                \\\r\n         \\      |\\   __  \\|\\  \\  |\\_\\|__\\|\\  \\|\\  \\ |\\  \\    /  /|\\   __  \\|\\   __  \\|\\  \\|\\  \\     |\\  \\  /  /|                \\\r\n          \\     \\ \\  \\|\\  \\ \\  \\ \\|_____|\\ \\  \\/  /|\\ \\  \\  /  / | \\  \\|\\  \\ \\  \\|\\  \\ \\  \\/  /|_   \\ \\  \\/  / /                 \\\r\n           \\     \\ \\   ____\\ \\  \\|\\   ____\\_\\   ___  \\ \\  \\/  / / \\ \\  \\\\\\  \\ \\   _  _\\ \\   ___  \\   \\ \\    / /                   \\\r\n            \\     \\ \\  \\___|\\ \\  \\ \\  \\____\\ \\  \\\\ \\  \\ \\    / /   \\ \\  \\\\\\  \\ \\  \\\\  \\\\ \\  \\\\ \\  \\   \\/  /  /                     \\\r\n             \\     \\ \\__\\    \\ \\__\\ \\_____  \\ \\__\\\\ \\__\\ \\__/ /     \\ \\_______\\ \\__\\\\ _\\\\ \\__\\\\ \\__\\__/  / /                        \\\r\n              \\     \\|__|     \\|__| |_____\\  \\|__| \\|__|\\|__|/       \\|_______|\\|__|\\|__|\\|__| \\|__|\\___/ /                          \\\r\n               \\                   |\\_________\\                                                    \\|___|/                            \\\r\n                \\                  \\|_________|                                                                                        \\\r\n                 \\______________________________________________________________________________________________________________________\\ ");
            Console.WriteLine("Gamemaster (-■_■): Vítejte ve hře 'Propadávací piškvorky', než budeme hrát, zadejte prosím parametry pro hru");
            Console.Write("[Any Key] --> OK");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Gamemaster (-■_■): Kolik sloupců má mít herní pole?:");
            string inputSirka = Console.ReadLine();
            while (!ReadAndCheckLine(inputSirka))
            {
                inputSirka = Console.ReadLine();
            }
            int sirka = Convert.ToInt32(inputSirka);
            Console.Clear();

            Console.WriteLine("Gamemaster (-■_■): Kolik řad má mít herní pole?:");
            string inputVyska = Console.ReadLine();
            while (!ReadAndCheckLine(inputVyska))
            {
                inputVyska = Console.ReadLine();
            }
            int vyska = Convert.ToInt32(inputVyska);
            Console.Clear();

            Console.WriteLine("Gamemaster (-■_■): Kolik žetonů musí ležet za sebou, aby hráč vyhrál:");
            string inputPocetKVyhre = Console.ReadLine();
            while (!ReadAndCheckLine(inputPocetKVyhre))
            {
                inputPocetKVyhre = Console.ReadLine();
            }
            int pocetKVyhre = Convert.ToInt32(inputPocetKVyhre);
            Console.Clear();

            Console.WriteLine("Gamemaster (-╬_╬): Děkuji za spolupráci, můžeme začít hrát");
            Console.Write("[Any Key] --> OK");
            Console.ReadKey();
            Console.Clear();
            Gameconsole gameconsole = new Gameconsole(new int[vyska,sirka], pocetKVyhre);

            while (true)
            {
                for (int i = -1; i <= 1; i += 2)
                {
                    //házení kamenů
                    gameconsole.Draw();
                    Console.WriteLine($"Gamemaster (-■_■): [Hraje Hráč ({gameconsole.PlayerSymbol(i)})] Na jaké pole chceš pustit žeton?:");
                    bool isMoveSuccessfull = false;
                    string input = Console.ReadLine();
                    while (!ReadAndCheckLine(input))
                    {
                        input = Console.ReadLine();
                    }
                    int x = Convert.ToInt32(input);
                    isMoveSuccessfull = gameconsole.TryMove(x, i);
                    while (!isMoveSuccessfull)
                    {
                        Console.Clear();
                        gameconsole.Draw();
                        Console.WriteLine("Gamemaster (-■_■): Kam to házíš? Zkus to jinam!:");
                        input = Console.ReadLine();
                        while (!ReadAndCheckLine(input))
                        {
                            input = Console.ReadLine();
                        }
                        x = Convert.ToInt32(input);
                        isMoveSuccessfull = gameconsole.TryMove(x, i);
                    }
                    Console.Clear();
                }
            }
        }

        public class Gameconsole
        {
            public Gameconsole(int[,] board, int wincount)
            {
                Width = board.GetLength(1);
                Height = board.GetLength(0);
                Board = board;
                Wincount = wincount;
                CharacterMap = ['o', ' ', 'x'];
            }
            int[,] Board {get;}
            int Wincount {get;}
            int Height {get;}
            int Width {get;}
            char[] CharacterMap {get;}
            public void Draw()
            {
                Console.WriteLine($"┌{new string('─',Width)}┐");
                for(int i = 0;i<Height;i++)
                {
                    Console.Write('│');
                    for(int j = 0;j<Width;j++)
                    {
                        Console.Write(CharacterMap[Board[i,j]+1]);
                    }
                    Console.WriteLine('│');
                }
                Console.WriteLine($"└{new string('─', Width)}┘");
            }
            public char PlayerSymbol(int i)
            {
                return CharacterMap[i+1];
            }
            public bool TryMove(int x, int playernum)
            {
                if (x > Width - 1 || x < 0)
                {
                    return false;
                }
                else
                {
                    if (Board[0, x] != 0)
                    {
                        return false;
                    }
                    else
                    {
                        int y = 1;
                        while (y <= Height)
                        {
                            if (y == Height)
                            {
                                break;
                            }
                            if (Board[y, x] != 0)
                            {
                                break;
                            }
                            y++;
                        }
                        Board[y - 1, x] = playernum;
                        for (int m = 0; m < 2; m++)
                        {
                            for (int n = 0; n < 2; n++)
                            {
                                Console.WriteLine(m+"|"+ n + "|" + x + "|" + y);
                                Console.ReadKey();
                                if (CheckWin([m, n],[x, y]))
                                {
                                    Console.WriteLine($"Gamemaster (-■_■): Vyhrál hráč {PlayerSymbol(playernum)}, gratuluji");
                                    Console.Write("[Any Key] --> OK");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                        }
                        return true;
                    }
                }
            }
            public bool CheckWin(int[] vector, int[] coords)
            {
                StringBuilder sb = new StringBuilder();
                for (int k = -Wincount + 1; k < Wincount; k++)
                {
                    if (coords[0] + k * vector[0] >= 0 && coords[0] + k * vector[0] < Height && coords[1] + k * vector[1] >= 0 && coords[1] + k * vector[1] < Width)
                    {
                        sb.Append(Board[coords[0] + k * vector[0], coords[1] + k * vector[1]]);
                    }
                }
                string line = sb.ToString();
                Console.WriteLine(line);
                if (line.Contains(new string(CharacterMap[0], Wincount)) || line.Contains(new string(CharacterMap[2], Wincount)))
                {
                    return true;
                }
                return false;
            }
        }
    }
}

