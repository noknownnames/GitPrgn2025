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
            int pocetLidi = 3;
            string kamaradickovani = "1-2 2-3";
            int A = 1;
            int B = 3;  
            if (zadatHodnoty)
            {
                Console.Write("Napište počet členů (int prosím): ");
                pocetLidi = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                Console.Write("Napište spoje mezi členy (formát A-B A-C ... Y-Z prosím): ");
                kamaradickovani = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Napište členy, mezi nimiž hledáte cestu (formát A-B prosím): ");
                string ABstring = Console.ReadLine();
                string[] ABarray = ABstring.Split("-");
                A = Convert.ToInt32(ABarray[0]);
                B = Convert.ToInt32(ABarray[1]);
                Console.WriteLine();
            }
            bool[,] graf = new bool[pocetLidi, pocetLidi];
            string[] poleKamaradickovani = kamaradickovani.Split(" ");
            for (int i = 0; i < poleKamaradickovani.Length; i++)
            {
                string[] dvojiceKamaradu = poleKamaradickovani[i].Split("-");
                int V1 = Convert.ToInt32(dvojiceKamaradu[0])-1;
                int V2 = Convert.ToInt32(dvojiceKamaradu[1])-1;
                graf[V1, V2] = true;
                graf[V2, V1] = true;
            }
            NeighbourMatrix GrafSousedu = new NeighbourMatrix(graf);
            GrafSousedu.VykreslitGraf();
            List<int> list = GrafSousedu.NajitCestuMeziBody(A,B);
            if (list.Count > 0)
            {
                for (int i = list.Count - 1; i > 0; i--)
                {
                    Console.Write($"{list[i]},");
                }
                Console.WriteLine(list[0]);
            }
            else
            {
                Console.WriteLine($"Neexistuje žádný řetěz spojující '{A}' a '{B}'");
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
            public void PostupovatVeHledani(int step, DiscoveryMatrix discoveryMatrix)
            {
                    for (int i = 0; i < Size; i++)
                    {
                        if (Cleny[step, i] && !discoveryMatrix.Cleny[step, i])
                        {
                            if (i == discoveryMatrix.Finish)
                            {
                                discoveryMatrix.FoundFinish = true;
                            }
                            discoveryMatrix.Cleny[step,i] = true;
                            discoveryMatrix.Cleny[i,step] = true;
                            PostupovatVeHledani(i, discoveryMatrix);
                            discoveryMatrix.PathFromFinish.Add(step);
                        }
                    }
            }
            public List<int>? NajitCestuMeziBody(int start, int finish)
            {
                bool[,] x = new bool[Size, Size];
                DiscoveryMatrix discoveryMatrix = new DiscoveryMatrix(x, finish);
                PostupovatVeHledani(start, discoveryMatrix);
                return discoveryMatrix.PathFromFinish;
            }
            public class DiscoveryMatrix
            {
                public DiscoveryMatrix(bool[,] cleny, int finish)
                {
                    Cleny = cleny;
                    Finish = finish;
                    FoundFinish = false;
                    PathFromFinish = new List<int>();
                }
                public bool[,] Cleny { get; set; }
                public int Finish { get; set; }
                public bool FoundFinish { get; set; }
                public List<int> PathFromFinish { get; set; }
            }
        }
    }
}