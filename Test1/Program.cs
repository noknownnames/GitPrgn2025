using System.Text;

namespace ConsoleApp2
{
    internal class Program
    {
        class Parser
        {
            public char[,] StringToCharraysquared(string input)
            {
                string[] firstCutInput = input.Split(" ");
                char[,] charraysquared = new char[firstCutInput.Length,firstCutInput[0].Length];
                for (int i = 0; i < firstCutInput.Length; i++)
                {
                    char[] line = firstCutInput[i].ToCharArray();
                    for (int j = 0; j < firstCutInput[0].Length; j++)
                    {
                        charraysquared[i, j] = line[j];
                    }
                }
                return charraysquared;
            }
        }
        class Monster
        {
            public Monster(char[] spriteSheet, string name, bool isSinistrous)
            {
                IsSinistrous = isSinistrous;
                SpriteSheet = spriteSheet;
                Name = name;
            }
            public bool IsSinistrous{ get; set; }
            public char[] SpriteSheet { get; set; }
            public string Name { get; set; }
        }
        class Labyrint
        {
            public Labyrint(char[,] charraysquared)
            {
                Bestiary = [new Monster(['>', '^', '<', 'v'], "Špičoun", false),new Monster(['3', 'm', 'ε', 'ω'], "Dvoják", true)];
                Charraysquared = charraysquared;
                MazeSize = [charraysquared.GetLength(0), charraysquared.GetLength(1)];
                MonsterTypes = new List<Monster>();
                MonsterPositions = new List<int[]>();
                MonsterVectors = new List<int[]>();

                int[] monsterCoords = new int[2];
                int[] monsterDirection = new int[2];
                for (int i = 0; i < MazeSize[0]; i++)
                {
                    for (int j = 0; j < MazeSize[1]; j++)
                    {
                        if (charraysquared[i, j] != 'X' && charraysquared[i, j] != '.')
                        {
                            for (int m = Bestiary.Count - 1; m >= 0; m--)
                            {
                                for (int d = 0; d < 4; d++)
                                {
                                    if (charraysquared[i, j] == Bestiary[m].SpriteSheet[d])
                                    {
                                        MonsterPositions.Add([i, j]);
                                        MonsterVectors.Add([Convert.ToInt16(Math.Cos((Math.PI * d) / 2)), Convert.ToInt16(Math.Sin((Math.PI * d) / 2))]);
                                        MonsterTypes.Add(Bestiary[m]);
                                    }
                                }
                            }
                        }
                    }
                }

            }
            char[,] Charraysquared { get; set; }
            List<Monster> Bestiary { get; set; }
            List<int[]> MonsterPositions { get; set; }
            List<int[]> MonsterVectors { get; set; }
            List<Monster> MonsterTypes {  get; set; }
            int[] MazeSize { get; set; }
            public void PrintMaze()
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < Charraysquared.GetLength(0); i++)
                {
                    for (int j = 0; j < Charraysquared.GetLength(1); j++)
                    {
                        sb.Append(Charraysquared[i, j]);
                    }
                    sb.Append('\n');
                }
                sb.Append('\n');
                Console.Write(sb.ToString());
            }
            public void Tick()
            {
                for (int m = 0; m < MonsterPositions.Count; m++)
                {
                    //Konverze: JeLevák(true/false) --> h[andedness](1/-1)
                    int h = Convert.ToInt16(MonsterTypes[m].IsSinistrous) * 2 - 1;
                    //Pohyb vpřed
                    Console.WriteLine($"{MonsterVectors[m][0]}, {MonsterVectors[m][1]}");
                    if (Charraysquared[MonsterPositions[m][0] + Convert.ToInt32(Math.Sqrt(2) * (Math.Cos(h * Math.PI / 4) * MonsterVectors[m][0] + Math.Sin(h * Math.PI / 4) * MonsterVectors[m][1])), MonsterPositions[m][1] + Convert.ToInt32(Math.Sqrt(2) * (Math.Sin(h * Math.PI / 4) * MonsterVectors[m][0] + Math.Cos(h * Math.PI / 4) * MonsterVectors[m][1]))] == 'X' && Charraysquared[MonsterPositions[m][0] + MonsterVectors[m][1], MonsterPositions[m][1] + MonsterVectors[m][0]] == '.')
                    {
                        Charraysquared[MonsterPositions[m][0], MonsterPositions[m][1]] = '.';
                        MonsterPositions[m][0] += MonsterVectors[m][1];
                        MonsterPositions[m][1] += MonsterVectors[m][0];
                    }
                    else if()//rotace o π/2
                    {

                    }
                    else//rotace o 3π/2
                    {

                    }
                    Charraysquared[MonsterPositions[m][0], MonsterPositions[m][1]] = MonsterTypes[m].SpriteSheet[Convert.ToInt32(Math.Atan2(Convert.ToDouble(MonsterVectors[m][1]), Convert.ToDouble(MonsterVectors[m][0])))];
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("┐   ┌┐  ┬┐  ┐┌  ┬┐  ┬  ┬┐  ┌┬┐\n│   ├┤  ├┤  └┤  ├┤  │  ││   │ \n└┘  ┘└  ┴┘  └┘  ┘└  ┴  ┘└   ┴ \nRozkresli řady labyrintu oddélené mezerami:\n");
            string input = Console.ReadLine();
            //Console.Clear();
            Parser parser = new Parser();
            Labyrint l = new Labyrint(parser.StringToCharraysquared(input));

            l.PrintMaze();
            for (int i = 0; i < 20; i++)
            {
                Console.Write($"{i+1}.Krok\n\n");
                l.Tick();
                l.PrintMaze();
            }
        }
    }
}
