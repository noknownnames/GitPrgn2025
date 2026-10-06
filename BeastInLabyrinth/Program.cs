using System.Text;

namespace BeastInLabyrinth
{
    internal class Program
    {
        static char[,] ParseInput()
        {
            int width = Convert.ToInt32(Console.ReadLine());
            int height = Convert.ToInt32(Console.ReadLine());
            char[,] charraysquared = new char[width,height];
            for (int i = 0; i < height; i++)
            { 
                string line = Console.ReadLine();
                for (int j = 0; j < width; j++)
                {
                    charraysquared[j, i] = line[j];
                }
            }
            return charraysquared;
        }
        class Labyrint 
        {
            /// <summary>
            /// Řízení výpisu a logiky bludiště
            /// </summary>
            /// <param name="charraysquared">pole char-ů ^ 2, reprezentující bludiště</param>
            public Labyrint(char[,] charraysquared)
            {
                Dirs = [[1, 0], [1, -1], [0, -1], [-1, -1], [-1, 0], [-1, 1], [0, 1], [1, 1]];
                Charraysquared = charraysquared;
                BeastData = [new Tuple<int, char[], bool>(0,['>', '^', '<', 'v'],false),new Tuple<int, char[], bool>(1, ['3', 'M', 'W', 'E'],true)];
                Beasts = [];
                ;
                for (int i = 0; i < charraysquared.GetLength(0); i++)
                {
                    for (int j = 0; j < charraysquared.GetLength(1); j++)
                    {
                        if (charraysquared[i, j] != 'X' && charraysquared[i, j] != '.')
                        {
                            for (int m = BeastData.Length - 1; m >= 0; m--)
                            {
                                for (int d = 0; d < 4; d++)
                                {
                                    if (charraysquared[i, j] == BeastData[m].Item2[d])
                                    {
                                        Beasts.Add(new Beast(BeastData[m].Item1, BeastData[m].Item2, BeastData[m].Item3, i, j, d));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            public int[][] Dirs { get; set; }
            char[,] Charraysquared { get; set; }
            Tuple<int, char[],bool>[] BeastData { get; set; }
            List<Beast> Beasts { get; set; }
            public void PrintMaze()
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < Charraysquared.GetLength(1); i++)
                {
                    for (int j = 0; j < Charraysquared.GetLength(0); j++)
                    {
                        sb.Append(Charraysquared[j, i]);
                    }
                    sb.Append('\n');
                }
                sb.Append('\n');
                Console.Write(sb.ToString());
            }
            private bool TryMove(Beast beast)
            {
                //je cesta volná a je tam stěna na správné straně? je diagonálně vepředu od nás stěna? || je vedle nás stěna? <-- upřímě nevím jak se má rozhodovat, tak aby se hýbal podle příkladu v classroomu, každopádné to je vśechno na tomhle řádku jestli to chc(e/i)/musí(m) někdo/já změnit tečka.
                if ((Charraysquared[beast.X + Dirs[(2 * beast.Dir + Convert.ToInt16(beast.IsSinistrous) * 2 - 1 + 40)%8][0], beast.Y + Dirs[(2 * beast.Dir + Convert.ToInt16(beast.IsSinistrous) * 2 - 1 + 40) %8][1]] == 'X' 
                || Charraysquared[beast.X + Dirs[(2 * beast.Dir + (Convert.ToInt16(beast.IsSinistrous) * 2 -1)*2 + 40) %8][0], beast.Y + Dirs[(2 * beast.Dir + (Convert.ToInt16(beast.IsSinistrous) * 2 -1)*2 + 40) %8][1]] == 'X')
                && Charraysquared[beast.X + Dirs[2*beast.Dir][0], beast.Y + Dirs[2*beast.Dir][1]] == '.')
                {
                    return true;
                }
                return false;
            }
            private void Move(Beast beast)
            {
                Charraysquared[beast.X, beast.Y] = '.';
                beast.X += Dirs[2 * beast.Dir][0];
                beast.Y += Dirs[2 * beast.Dir][1];
                Charraysquared[beast.X, beast.Y] = BeastData[beast.Type].Item2[beast.Dir];
            }
            private bool TryTurnLeft(Beast beast)
            {
                if (Charraysquared[beast.X + Dirs[(2*beast.Dir + 4998)%8][0], beast.Y + Dirs[(2 * beast.Dir + 4998)%8][1]] == 'X' 
                    && Charraysquared[beast.X + Dirs[(2 * beast.Dir + 4000) % 8][0], beast.Y + Dirs[(2 * beast.Dir + 4000) % 8][1]] == 'X')
                {
                    return true;
                }
                return false;
            }
            private void Turn(Beast beast)
            {
                if (TryTurnLeft(beast))
                {
                    beast.Dir += 1;
                }
                else
                {
                    beast.Dir += 3;
                }
                beast.Dir %= 4;
                Charraysquared[beast.X, beast.Y] = BeastData[beast.Type].Item2[beast.Dir];
            }
            public void Tick()
            {
                for (int m = 0; m < Beasts.Count; m++) 
                {
                    if (TryMove(Beasts[m]))
                    {
                        Move(Beasts[m]);
                    }
                    else
                    {
                        Turn(Beasts[m]);
                    }
                }
            }
        }
        class Beast
        {
            public Beast(int type, char[] shapes, bool isSinistrous, int x, int y, int direction)
            {
                Type = type;
                Shapes = shapes;
                IsSinistrous = isSinistrous;
                X = x;
                Y = y;
                Dir = direction;
            }
            public int Type { get; set; }
            public char[] Shapes { get; set; }
            public bool IsSinistrous { get; set; }
            public int Dir {  get; set; }
            public int X { get; set; }
            public int Y { get; set; }

        }
            static void Main(string[] args)
        {
            Console.WriteLine("┐   ┌┐  ┬┐  ┐┌  ┬┐  ┬  ┬┐  ┌┬┐\n│   ├┤  ├┤  └┤  ├┤  │  ││   │ \n└┘  ┘└  ┴┘  └┘  ┘└  ┴  ┘└   ┴ \nvložte vstup:\n");
            Labyrint l = new Labyrint(ParseInput());
            Console.Clear();
            Console.WriteLine("┐   ┌┐  ┬┐  ┐┌  ┬┐  ┬  ┬┐  ┌┬┐ .\n│   ├┤  ├┤  └┤  ├┤  │  ││   │  \n└┘  ┘└  ┴┘  └┘  ┘└  ┴  ┘└   ┴  ˙\n");
            l.PrintMaze();
            for (int i = 0; i < 20; i++)
            {
                Console.Write($"{i + 1}.Krok\n\n");
                l.Tick();
                l.PrintMaze();
            }
        }
    }
}
