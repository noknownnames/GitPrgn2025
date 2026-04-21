using System.ComponentModel;
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
            Console.WriteLine("╔═══════════════════════════════════════▀▀══════════════════════▀▀═════════════════════▀▄▀══════════════════════════════════════╗");
            Console.WriteLine("║ █▀▀▀▄ █▀▀▀▄ ▄▀▀▀▄ █▀▀▀▄ ▄▀▀▀▄ █▀▀▀▄ ▄▀▀▀▄ █   █ ▄▀▀▀▄ ▄▀▀▀▄ ▀▀█▀▀       █▀▀▀▄ ▀▀█▀▀ ▄▀▀▀▄ █  ▄▀ █   █ ▄▀▀▀▄ █▀▀▀▄ █  ▄▀ ▀▄ ▄▀ ║");
            Console.WriteLine("║ █▀▀▀  █▀█▀  █   █ █▀▀▀  █▀▀▀█ █   █ █▀▀▀█ █   █ █▀▀▀█ █       █         █▀▀▀    █    ▀▀▀▄ █▀▀▄  █   █ █   █ █▀█▀  █▀▀▄    █   ║");
            Console.WriteLine("║ █     █  ▀▄ ▀▄▄▄▀ █     █   █ █▄▄▄▀ █   █  ▀▄▀  █   █ ▀▄▄▄▀ ▄▄█▄▄       █     ▄▄█▄▄ ▀▄▄▄▀ █   █  ▀▄▀  ▀▄▄▄▀ █  ▀▄ █   █   █   ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine("Gamemaster (-■_■): Vítejte ve hře 'Propadávací piškvorky', než budeme hrát, zadejte prosím parametry pro hru");
            Console.Write("[Any Key] --> OK");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Gamemaster (-■_■): Kolik políček v řadě má mít herní pole?:");
            string inputSirka = Console.ReadLine();
            while (!ReadAndCheckLine(inputSirka))
            {
                inputSirka = Console.ReadLine();
            }
            int sirka = Convert.ToInt32(inputSirka);
            Console.Clear();

            Console.WriteLine("Gamemaster (-■_■): Kolik políček ve sloupci má mít herní pole?:");
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
                    //kontrola výhry
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
                CharacterMap = ['o', ' ', '¤'];
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
                        return true;
                    }
                }
            }
        }
    }
}

