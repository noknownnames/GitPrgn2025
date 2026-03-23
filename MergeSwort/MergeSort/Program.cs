namespace MergeSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[] { 1, 2, 4, 2, 7, 5, 9, 2, 3, 1, 1, 3, 2, 6 };
            MergeSortClass.Sort(arr);
            for (int i =0;i<arr.Length-1 ;i++)
            {
                Console.Write($"{arr[i]},");
            }
            Console.Write($"{arr[arr.Length-1]}");
        }
    }

    public class MergeSortClass
    {
        public static void Sort(int[] array)
        {
            if (array == null || array.Length <= 1)
                return;

            MergeSort(array, 0, array.Length - 1);
        }

        public static void MergeSort(int[] array, int left, int right)
        {
            /* TODO: Dopište tělo funkce a otestujte ji (alespoň 3 testy) */
            if (left < right)
            {
                int m = left + (right - left) / 2;
                MergeSort(array, left, m);
                MergeSort(array, m + 1, right);
                Merge(array, left, m, right);
            }
        }
        /* TODO: Proveďte alespoň 5 různých testů funkce Merge */
        /* Pozn. Testované funkce musí být public, aby byly vidět i v projektu s testy. Po řádném otestování je zrovna zde vhodné, udělat tuto funkci privátní (je to funkce pouze pomocná). */
        public static void Merge(int[] array, int left, int middle, int right)
        {
            // Velikosti dočasných polí
            int n1 = middle - left + 1;
            int n2 = right - middle;

            // Vytvoření dočasných polí
            int[] leftArray = new int[n1];
            int[] rightArray = new int[n2];

            // Kopírování dat do dočasných polí
            Array.Copy(array, left, leftArray, 0, n1);
            Array.Copy(array, middle + 1, rightArray, 0, n2);

            // Indexy pro procházení polí
            int i = 0, j = 0;
            int k = left;

            // Porovnávání prvků a vkládání zpět do původního pole
            while (i < n1 && j < n2)
            {
                if (leftArray[i] <= rightArray[j])
                {
                    array[k] = leftArray[i];
                    i++;
                }
                else
                {
                    array[k] = rightArray[j];
                    j++;
                }
                k++;
            }

            // Zkopírování zbývajících prvků (pokud nějaké zbyly)
            while (i < n1)
            {
                array[k] = leftArray[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                array[k] = rightArray[j];
                j++;
                k++;
            }
        }
    }
}

