using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Přepsat výchozí hodnoty? (true/false prosím): ");
            bool zadatHodnoty = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine();
            int pocetLidi = 7;
            string kamaradickovani = "1-2 2-3 3-4 4-5 5-6 6-7";
            int A = 0;
            int B = 6;
            if (zadatHodnoty)
            {
                Console.Write("Napište počet členů (int prosím): ");
                pocetLidi = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                Console.Write("Napište spoje mezi členy (formát Atý-Btý Atý-Ctý ... Ytý-Ztý prosím): ");
                kamaradickovani = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Napište členy, mezi nimiž hledáte cestu (formát Atý-Btý prosím): ");
                string ABstring = Console.ReadLine();
                string[] ABarray = ABstring.Split("-");
                A = Convert.ToInt32(ABarray[0])-1;
                B = Convert.ToInt32(ABarray[1])-1;
                Console.WriteLine();
            }
            bool[,] graf = new bool[pocetLidi, pocetLidi];
            string[] poleKamaradickovani = kamaradickovani.Split(" ");
            for (int i = 0; i < poleKamaradickovani.Length; i++)
            {
                string[] dvojiceKamaradu = poleKamaradickovani[i].Split("-");
                int V1 = Convert.ToInt32(dvojiceKamaradu[0]) - 1;
                int V2 = Convert.ToInt32(dvojiceKamaradu[1]) - 1;
                graf[V1, V2] = true;
                graf[V2, V1] = true;
            }
            NeighbourMatrix GrafSousedu = new NeighbourMatrix(graf);
            GrafSousedu.VykreslitGraf();
            Console.WriteLine();
            List<int> list = GrafSousedu.NajitCestuMeziBody(A, B);
            if (list.Count > 0)
            {
                Console.WriteLine($"minimální řetěz spojující '{A+1}.' a '{B+1}.' člen vypadá takto:");
                for (int i = list.Count - 1; i > 0; i--)
                {
                    Console.Write($"{list[i]}->");
                }
                Console.WriteLine(list[0]);
            }
            else
            {
                Console.WriteLine($"Neexistuje žádný řetěz spojující '{A+1}.' a '{B+1}.' člen");
            }
        }
        class NeighbourMatrix //paměťová složitost n^2
        {
            public NeighbourMatrix(bool[,] cleny)
            {
                Cleny = cleny;
                Size = cleny.GetLength(0);
            }
            public bool[,] Cleny { get; set; }
            public int Size { get; set; }
            public void VykreslitGraf()
            {
                if (Size <= 9)
                {
                    Console.Write("|#|");
                    for (int i = 0; i < Size; i++)
                    {
                        Console.Write($"{i + 1}|");
                    }
                    Console.WriteLine();
                    for (int i = 0; i < Size; i++)
                    {
                        Console.Write($"|{i + 1}|");
                        for (int j = 0; j < Size; j++)
                        {
                            Console.Write($"{Convert.ToInt16(Cleny[i, j])}");
                            if (j < Size - 1) { Console.Write(","); }
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    for (int i = 0; i < Size; i++)
                    {
                        for (int j = 0; j < Size; j++)
                        {
                            Console.Write($"{Convert.ToInt16(Cleny[i, j])}");
                            if (j < Size - 1) { Console.Write(","); }
                        }
                        Console.WriteLine();
                    }
                }
            }
            public List<int>? NajitCestuMeziBody(int start, int finish)
            {
                Queue<int> queue = new Queue<int>();
                bool[] nalezeno = new bool[Size];
                nalezeno[start] = true;
                int[] vzdalenost = new int[Size];
                int?[] predchudce = new int?[Size];
                queue.Enqueue(start);
                while (queue.Count > 0)
                {
                    int vrchol = queue.Dequeue();
                    for (int i = 0; i < Size; i++)
                    {
                        if (Cleny[vrchol, i] && !nalezeno[i])
                        {
                            nalezeno[i] = true;
                            vzdalenost[i] = vzdalenost[vrchol] + 1;
                            predchudce[i] = vrchol;
                            queue.Enqueue(i);
                        }
                    }
                }
                List<int> cesta = new List<int>();
                if (nalezeno[finish] == true)
                {
                    while (predchudce[finish] != null)
                    {
                        cesta.Add(finish);
                        finish = Convert.ToInt32(predchudce[finish]);
                    }
                    cesta.Add(finish);
                }
                return cesta;
            }
        }
    }
}