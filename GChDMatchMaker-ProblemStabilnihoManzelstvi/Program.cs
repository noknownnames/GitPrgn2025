using System.Runtime.InteropServices;
using System.Text;
namespace GChDMatchMaker_ProblemStabilnihoManzelstvi
{
    internal class Program
    {
        static void PrintArraySquared(int[,] arraysquared)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < arraysquared.GetLength(0); i++)
            {
                for (int j = 0; j < arraysquared.GetLength(1); j++)
                {
                    sb.Append(arraysquared[i, j]);
                    sb.Append(' ');
                }
                sb.Append('\n');
            }
            Console.WriteLine(sb.ToString());
        }
        static (int,int)[] CreateOptimalShippingPairs(int[,] mPregMatrix, int[,] wPregMatrix)
        {
            
        } 
        public class Parser()
        {
            public (int[,],int[,]) Parse()
            {
                int n = Convert.ToInt32(Console.ReadLine());
                int[,] arraysquared1 = new int[n, n];
                int[,] arraysquared2 = new int[n, n];
                for (int i = 0; i < n; i++)
                {
                    string[] line = Console.ReadLine().Split(' ');
                    for (int j = 0; j < n; j++)
                    {
                        arraysquared1[i, j] = Convert.ToInt32(line[j]);
                    }
                }
                for (int i = n; i < n*2; i++)
                {
                    string[] line = Console.ReadLine().Split(' ');
                    for (int j = 0; j < n; j++)
                    {
                        arraysquared2[i, j] = Convert.ToInt32(line[j]);
                    }
                }
                return (arraysquared1, arraysquared2);
            }
        }
        static void Main(string[] args)
        {
            (int[,],int[,]) biArraySquared = new Parser().Parse();
        }
    }
}
