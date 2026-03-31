namespace BogogSport
{
    internal class Program
    {
        public class Sort
        {
            public static int[] Bogosort(int[] array)
            {
                int r = 0;
                List<int> list = array.OfType<int>().ToList();
                bool sorted = false;
                while(!sorted)
                    {
                        List<int> rndList = new List<int>();
                        while(list.Count > 0)
                        {
                            if (list.Count > 1)
                            {
                                r = new Random(Guid.NewGuid().GetHashCode()).Next(0, list.Count - 1);//Random bez extra parametru má tendenci opakovat stejný seed, asi to bere ze systémového času či co...
                            }
                            else
                            {
                                r = 0;
                            }
                            rndList.Add(list[r]);
                            list.Remove(list[r]);
                        }
                        list = rndList;
                        foreach(int i in list)
                        sorted = true;
                        for (int j = 1; j < list.Count;j++)
                        {
                            if (list[j-1]>list[j])
                            {
                                sorted = false;
                            }
                        }
                    }
                return list.ToArray();
            }
        }
        static void Main(string[] args)
        {
            Console.Write("HéÉéJ, kÁmO! cO bYs ChTěL [B O G O]sOrTnOuT!:");
            string input = Console.ReadLine();
            string[] listiezedInput = input.Split(",");
            int[] list = new int[listiezedInput.Length];
            for (int i = 0; i < listiezedInput.Length; i++)
            {
                list[i] = Convert.ToInt32(listiezedInput[i]);
            }
            Console.WriteLine();
            //třídění a výpis
            list = Sort.Bogosort(list);
            Console.WriteLine("EpIcKy, BoGoTiCkY VyTřÍděNý, VyTřÍbEnÝ PoLe >:D :");
            for (int i = 0; i < list.Length - 1; i++)
            {
                Console.Write($"{list[i]},");
            }
            Console.WriteLine($"{list[list.Length - 1]}");
        }
    }
}
