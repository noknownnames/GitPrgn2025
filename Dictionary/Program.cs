using System;
using System.Text;

namespace Dictionary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" ______     __         ______     __   __   __   __       __   __  __    \r\n/\\  ___\\   /\\ \\       /\\  __ \\   /\\ \\ / /  /\\ \"-.\\ \\     /_/  /\\ \\/ /    \r\n\\ \\___  \\  \\ \\ \\____  \\ \\ \\/\\ \\  \\ \\ \\'/   \\ \\ \\-.  \\   /\\ \\  \\ \\  _\"-.  \r\n \\/\\_____\\  \\ \\_____\\  \\ \\_____\\  \\ \\__|    \\ \\_\\\\\"\\_\\  \\ \\_\\  \\ \\_\\ \\_\\ \r\n  \\/_____/   \\/_____/   \\/_____/   \\/_/      \\/_/ \\/_/   \\/_/   \\/_/\\/_/ \r\n                                                                         \r\n");
            using (StreamReader sr = new StreamReader(@"..\..\..\..\..\dict_inputs.txt"))
            {
                //čtení dat
                int[] inputArray = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                StringBuilder sb = new StringBuilder();
                for(int i = 0; i< inputArray.Length-1;i++)
                {
                    sb.Append(inputArray[i]+" ");
                }
                int targetValue = Convert.ToInt32(sr.ReadLine());
                sb.Append(inputArray[inputArray.Length - 1] + "\n" + targetValue);
                int[] ans = { -1 ,-1 };

                //Porovnávání hodnot
                Dictionary<int, int> d = new Dictionary<int, int>();
                int x;
                for (int i = 0; i < inputArray.Length; i++)
                {
                    x = targetValue - inputArray[i];
                    if (d.TryGetValue(x, out int k))
                    {
                        ans = [k,i];
                    }

                    d[inputArray[i]] = i;

                }

                //Výpis vstupu odpovědi
                Console.WriteLine(sb.ToString());
                if (ans[0] != -1)
                {
                    Console.WriteLine($"{ans[0]} {ans[1]}");
                }
                else
                {
                    Console.WriteLine($"-1");
                }
            }
        }
    }
}
/* ┌- Hloupé Řešení (¬🧠)
 * ↓
Dictionary<int, int> dict = new Dictionary<int, int>();
for (int i = 0; i < inputArray.Length; i++)
{
    dict.Add(i, inputArray[i]);
}
for (int i = 0; i < dict.Count; i++)
{
    for (int j = 0; j < dict.Count - 1; j++)
    {
        if (dict[i] + dict[(i + j + 1) % dict.Count] == targetValue)
        {
            ans = [i, (i + j + 1) % dict.Count];
        }
    }
}
*/
