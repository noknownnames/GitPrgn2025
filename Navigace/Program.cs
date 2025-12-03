using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Šetrný k přírodě! tento kód je ze 70% z recyklovaných domácích úkolů");
            Console.WriteLine("PS. Testovací vstupní hodnoty ve správném formátu jsou vloženy do složky 'Navigace'");
            Console.WriteLine();
            Console.Write("Přepsat výchozí hodnoty? (true/false prosím): ");
            bool zadatHodnoty = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine();
            int pocetLidi = 7;
            string kamaradickovaniMest = "1;2;40;0 2;3;12;0 3;4;30;0 4;5;20;0 5;6;23;1 6;7;16;0";
            int A = 0;
            int B = 6;
            int M = 1;
            if (zadatHodnoty)
            {
                Console.Write("Napište počet měst (int prosím): ");
                pocetLidi = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                Console.Write("Vypište tratě mezi městy, a jestli jsou placené(1) nebo neplacené(0) (formát Atovice;Btov;L1;1 Atovice;Cťany;L2;0 ... Ytky;ZnadVltavou;Ln;0 prosím): ");
                kamaradickovaniMest = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Napište města, mezi nimiž města cestu (formát Atovice-Btov prosím): ");
                string ABstring = Console.ReadLine();
                string[] ABarray = ABstring.Split("-");
                A = Convert.ToInt32(ABarray[0]) - 1;
                B = Convert.ToInt32(ABarray[1]) - 1;
                Console.WriteLine();
                Console.Write("Napište maximální počet placených silnic, přes které jste ochotni projet (int prosím): ");
                M = Convert.ToInt32(Console.ReadLine());
            }
            string[] poleKamaradickovaniMest = kamaradickovaniMest.Split(" ");
            double[,,] graf = new double[pocetLidi, pocetLidi, 2];
            for (int i = 0; i < poleKamaradickovaniMest.Length; i++)
            {
                for (int j = 0; j < poleKamaradickovaniMest.Length; j++)
                {
                    graf[i, j, 0] = double.PositiveInfinity;
                }
            }
            for (int i = 0; i < poleKamaradickovaniMest.Length; i++)
            {
                string[] nejakDlouhaSilniceOdNekudNekamZaNejakyPrachy = poleKamaradickovaniMest[i].Split(";");
                int V1 = Convert.ToInt32(nejakDlouhaSilniceOdNekudNekamZaNejakyPrachy[0]) - 1;
                int V2 = Convert.ToInt32(nejakDlouhaSilniceOdNekudNekamZaNejakyPrachy[1]) - 1;
                graf[V1, V2, 0] = Convert.ToInt32(nejakDlouhaSilniceOdNekudNekamZaNejakyPrachy[2]);
                graf[V2, V1, 0] = Convert.ToInt32(nejakDlouhaSilniceOdNekudNekamZaNejakyPrachy[2]);
                graf[V1, V2, 1] = Convert.ToInt32(nejakDlouhaSilniceOdNekudNekamZaNejakyPrachy[3]);
                graf[V2, V1, 1] = Convert.ToInt32(nejakDlouhaSilniceOdNekudNekamZaNejakyPrachy[3]);
            }
            NeighbourMatrix GrafSousedu = new NeighbourMatrix(graf);
            //GrafSousedu.VykreslitGraf();Console.WriteLine();
            List<int> list = GrafSousedu.NajitCestuMeziMestyZaPrijatelnouCenuAtSeToClovekuNeprodrazi(A, B, M);
            if (list.Count > 0)
            {
                Console.WriteLine($"minimální cesta od města '{A + 1}.' do města '{B + 1}.' za cenu, která neublíží peněžence vypadá takto:");
                for (int i = list.Count - 1; i > 0; i--)
                {
                    Console.Write($"{list[i]}->");
                }
                Console.WriteLine(list[0]);
            }
            else
            {
                Console.WriteLine($"Neexistuje žádná cesta spojující město '{A + 1}.' a  město '{B + 1}.' za cenu přijatelnou pro skrblíka jako jste vy");
            }
        }
        class NeighbourMatrix
        {
            public NeighbourMatrix(double[,,] cleny)
            {
                Cleny = cleny;
                Size = cleny.GetLength(0);
            }
            public double[,,] Cleny { get; set; }
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
                            Console.Write($"{Convert.ToInt16(Cleny[i, j, 0])}");
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
                            Console.Write($"{Convert.ToInt16(Cleny[i, j, 0])}");
                            if (j < Size - 1) { Console.Write(","); }
                        }
                        Console.WriteLine();
                    }
                }
            }
            public List<int>? NajitCestuMeziMestyZaPrijatelnouCenuAtSeToClovekuNeprodrazi(int start, int finish, int maxPlacenychCest)
            {
                int mult = true ? 1 : -1;// true = ascending, použito v tom kódním ekvivalentu zrádného pseudokódního "řadku 8"
                int priority = 0;
                PriorityQueue<int,int> queue = new PriorityQueue<int,int>(Comparer<int>.Create((a, b) => a - b));
                bool[] nalezeno = new bool[Size];
                nalezeno[start] = true;
                double[] celkovaVzdalenost = new double[Size];
                double[] celkovyPocetPlacenychSilnic = new double[Size];
                Array.Fill<double>(celkovaVzdalenost, double.PositiveInfinity);
                int?[] predchudce = new int?[Size];
                queue.Enqueue(start,0);
                while (queue.Count > 0)
                {
                    int vrchol = queue.Dequeue();
                    List<double[]> nastupci = new List<double[]>();
                    for (int i = 0; i < Size; i++)
                    {
                        if (Cleny[vrchol, i, 0] < double.PositiveInfinity && !nalezeno[i] && celkovyPocetPlacenychSilnic[vrchol] <= maxPlacenychCest)
                        {
                            nalezeno[vrchol] = true;
                            if (celkovaVzdalenost[vrchol] + Cleny[vrchol, i, 0] < celkovaVzdalenost[i])
                            {
                                celkovaVzdalenost[i] = celkovaVzdalenost[vrchol] + Cleny[vrchol, i, 0];
                            }
                            celkovyPocetPlacenychSilnic[i] = celkovyPocetPlacenychSilnic[vrchol] + Cleny[vrchol, i, 1];
                            predchudce[i] = vrchol;
                            nastupci.Add([i,celkovaVzdalenost[i]]);
                        }
                    }
                    nastupci.Sort((a, b) => mult * a[1].CompareTo(b[1]));
                    foreach (double[] i in nastupci)
                    {
                        queue.Enqueue(Convert.ToInt16(i[0]), priority);
                        priority--;
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