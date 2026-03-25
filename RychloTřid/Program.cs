namespace RychloTřid
{
    internal class Program
    {
        public class Sort
        {
            private static void _swap(int[] array, int i, int j)
            {
                int mem = array[i];
                array[i] = array[j];
                array[j] = mem;
            }
            private static void _quicksort(int[] array, int l, int r)
            {
                if (l < r)
                {
                    //vybíráme vhodný pivo(t):
                    int p = array[new Random().Next(l, r)];
                    int i = l, j = r;
                    
                    while (i <= j)
                    {
                        while (array[i] < p)
                        {
                            i++;
                        }
                        while (array[j] > p)
                        {
                            j--;
                        }
                        if (i < j)
                        {
                            Sort._swap(array, i, j);
                        }
                        if (i <= j)
                        {
                            i++;
                            j--;
                        }
                    }
                    Sort._quicksort(array, l, j);
                    Sort._quicksort(array, i, r);
                }
            }
            public static void Quicksort(int[] array)
            {
                Sort._quicksort(array, 0, array.Length - 1);
            }
        }
        static void Main(string[] args)
        {
            //zpracování vstupu
            Console.Write("Napište pole čarkami oddělených celých čísel, které chcete [Q U I C K S O R T]ovat:");
            string input = Console.ReadLine();
            string[] arrayiezedInput = input.Split(",");
            int[] array = new int[arrayiezedInput.Length];
            for (int i = 0; i < arrayiezedInput.Length; i++)
            {
                array[i] = Convert.ToInt32(arrayiezedInput[i]);
            }
            Console.WriteLine();
            //třídění a výpis
            Sort.Quicksort(array);
            Console.WriteLine("Setříděné pole:");
            for (int i = 0; i < array.Length-1; i++)
            {
                Console.Write($"{array[i]},");
            }
            Console.WriteLine($"{array[array.Length - 1]}");
        }
    }
}