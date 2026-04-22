using System.ComponentModel.DataAnnotations;
using System.IO.Pipes;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
//TODO: 1) Kontorola vstupu ve f-ci PostfixToTree 2)
namespace VyrazovyStrom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("                                     ╔═════════════════════════╩═════════════════════════╗\r\n                                     ║                                        _          ║\r\n                   ╔═════════════════╩═════════════════╗                     / |_        ║\r\n                   ║                                   ║       _       .--. `| |-'_ .--.   .--.   _ .--..--. \r\n        ╔══════════╩═════════╗                         ║      / ]     ( (`\\] | | [ `/'`\\]/ .'`\\ \\[ `.-. .-. |\r\n        ║   _                ║               .--.   _   __  _'[/__     `'.'. | |, | |    | \\__. | | | | | | |  \r\n        ║  / ]               ║             / .'`\\ \\[ \\ [  ][ \\ [  ]   [\\__) )\\__/[___]    '.__.' [___||__||__]\r\n _   __  _'[/__     _ .--.  ,--.   ____    | \\__. | \\ \\/ /  \\ '/ /\r\n[ \\ [  ][ \\ [  ]   [ `/'`\\]`'_\\ : [_   ]    '.__.'   \\__/ [\\_:  /\r\n \\ \\/ /  \\ '/ /     | |    // | |, .' /_                   \\__.'  \r\n  \\__/ [\\_:  /     [___]   \\'-;__/[_____] \r\n        \\__.'\n");
            ExpressionTree tree = new ExpressionTree();
            Console.WriteLine("┌──────────────────────────────────────────────────────────────────────────────────────────────────────┐\n│Napište výraz v postfixu(operátory: +, -, *, /, ^, log, max, min, mod, %, zpd, dbz; konstanty: e, pi) │\n└──────────────────────────────────────────────────────────────────────────────────────────────────────┘");
            tree.PostfixToTree(Console.ReadLine());
            Console.WriteLine();
            tree.Print(false);
            Console.WriteLine();
            Console.WriteLine("Postfix: " + tree.ToPost());
            Console.WriteLine("Prefix: " + tree.ToPref());
            Console.WriteLine("Infix: " + tree.ToInfx());
            Console.WriteLine();
            Console.WriteLine(tree.ToInfx()+" = "+tree.Evaluate());
        }
    }
    public class ExpressionTree
    {
        public ExpressionTree()
        {
            string[] Tree = null; 
            int Depth = 0;
            string[,] Grid = null;
        }
        string[] Tree { get; set; }
        int Depth { get; set; }
        string[,] Grid { get; set; }
        public void PostfixToTree(string str)
        {
            string[] array = str.Split(' ');
            Stack<string> s = new Stack<string>(str.Split(' '));
            decimal numOfOperators = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (!double.TryParse(array[i], out double d) && array[i] != "e" && array[i] != "pi")//pokud je na uzlu operátor
                {
                    numOfOperators++;
                }
            }
            Depth = Convert.ToInt32(Math.Ceiling(numOfOperators + 1));
            Tree = new string[Convert.ToInt32(Math.Pow(2,Depth))];//inicializace pole, které nám bude sloužit jako strom
            _postFixToTree(1, s);
        }
        private void _postFixToTree(int i, Stack<string> s)
        {
            string operationElement = s.Pop();
            Tree[i-1] = operationElement;
            if(!double.TryParse(operationElement, out double d) && operationElement != "e" && operationElement != "pi")
            {
                _postFixToTree(i*2+1, s);
                _postFixToTree(i*2, s);
            }
        }
        public double FindMax()
        {
            return _findMax(1);
        }
        private double _findMax(int i)
        {
            if (!double.TryParse(Tree[i - 1], out double d))
            {
                if(Tree[i-1] == "e")
                {
                    return double.E;
                }
                if (Tree[i - 1] == "pi")
                {
                    return double.Pi;
                }
                double r = _findMax(i * 2);
                double l = _findMax(i * 2 + 1);
                if(r > l)
                {
                    return r;
                }
                return l;
            }
            else return Convert.ToDouble(Tree[i - 1]);
        }
        public void Print(bool isBoxed)
        {
            int max = 0;
            foreach (string s in Tree) 
            {
                if (s != null)
                {
                    if (s.Length > max)
                    {
                        max = s.Length;
                    }
                }
            }
            //najdeme nejdelší číslo, podle něj určíme mezery mezi znaky
            int k = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(max/2))*2+1);//<- k ∈ {2n-1;n∈ℕ}
            /*vytvoříme škálované elementy diagramu
            clear = new string(' ', k);
            lines = new string('─', k);
            lBend = '┌' + new string('─', k-1);
            rBend = new string('─', k-1)+ '┐';
            juncT = new string('─', (k - 1) / 2)+ '┴' + new string('─', (k - 1) / 2);
            */

            //Připravíme mřížku
            Grid = new string[Depth*2-1, Convert.ToInt32(Math.Pow(2,Depth)-1)];
            for(int i = 0; i < Depth * 2-1; i++)
            {
                for (int j = 0; j < Convert.ToInt32(Math.Pow(2, Depth) - 1); j++)
                {
                    Grid[i, j] = new string(' ', k);
                }
            }
            //Vložíme prvky do mřížky
            _print(1, k, 0, Convert.ToInt32(Math.Pow(2, Depth - 1)-1));

            //Sloučíme mřížku do stringu a vytiskneme ho
            StringBuilder sbInner = new StringBuilder();
            StringBuilder sbOuter = new StringBuilder();
            if (!isBoxed)
            {
                for (int i = 0; i < Grid.GetLength(0); i++)
                {
                    sbInner.Clear();
                    for (int j = 0; j < Grid.GetLength(1); j++)
                    {
                        sbInner.Append(Grid[i, j]);
                    }
                    sbOuter.Append(sbInner.ToString() + "\n");
                }
            }
            else
            {
                sbOuter.Append('┌' + new string('─', k - 1) + new string('─', k* Grid.GetLength(1)) + new string('─', k - 1) + '┐'+'\n');
                for (int i = 0; i < Grid.GetLength(0); i++)
                {
                    sbInner.Clear();
                    sbInner.Append(new string(' ', (k - 1) / 2)+ '│' + new string(' ', (k - 1) / 2));
                    for (int j = 0; j < Grid.GetLength(1); j++)
                    {
                        sbInner.Append(Grid[i, j]);
                    }
                    sbInner.Append(new string(' ', (k - 1) / 2) + '│' + new string(' ', (k - 1) / 2));
                    sbOuter.Append(sbInner.ToString() + "\n");
                }
                sbOuter.Append('└' + new string('─', k - 1) + new string('─', k * Grid.GetLength(1)) + new string('─', k - 1) + '┘'+'\n');
            }
            Console.Write(sbOuter.ToString());
        }
        private void _print(int i, int k, int y, int x)
        {
            Grid[y, x] = new string(' ', Convert.ToInt32(Math.Floor((k - Convert.ToDecimal(Tree[i-1].Length)) / 2))) + Tree[i-1] + new string(' ', Convert.ToInt32(Math.Ceiling((k - Convert.ToDecimal(Tree[i-1].Length)) / 2)));
            if (!double.TryParse(Tree[i-1], out double d) && Tree[i-1] != "e" && Tree[i-1] != "pi")//Pokud je na uzlu operátor
            {
                //Spojující čáry
                for(int j = 1; j< Convert.ToInt32(Math.Pow(2, Depth - 2 - y / 2)); j++)
                {
                    Grid[y + 1, x + j] = new string('─', k);
                    Grid[y + 1, x - j] = new string('─', k);
                }
                Grid[y + 1, x] = new string('─', (k - 1) / 2) + '┴' + new string('─', (k - 1) / 2);
                Grid[y + 1, x + Convert.ToInt32(Math.Pow(2, Depth - 2 - y / 2))] = new string('─', (k - 1) / 2) + '┐' + new string(' ', (k - 1) / 2);
                Grid[y + 1, x - Convert.ToInt32(Math.Pow(2, Depth - 2 - y / 2))] = new string(' ', (k - 1) / 2) + '┌' + new string('─', (k - 1) / 2);
                //Další vrstva
                _print(2*i, k, y+2, x-Convert.ToInt32(Math.Pow(2,Depth-2-y/2)));
                _print(2*i+1, k, y+2, x+Convert.ToInt32(Math.Pow(2, Depth - 2 - y / 2)));
            }
        }
        public string ToPost()
        {
           return _toPost(1); 
        }
        private string _toPost(int i)
        {
            if (!double.TryParse(Tree[i - 1], out double d) && Tree[i - 1] != "e" && Tree[i - 1] != "pi")
            {
            return _toPost(2 * i) + " " + _toPost(2 * i + 1) + " " + Tree[i - 1];
            }
            return Tree[i - 1];
        }
        public string ToPref()
        {
            return _toPref(1);
        }
        private string _toPref(int i)
        {
            if (!double.TryParse(Tree[i - 1], out double d) && Tree[i - 1] != "e" && Tree[i - 1] != "pi")
            {
                return Tree[i - 1] + " " +_toPref(2 * i) + " " + _toPref(2 * i + 1);
            }
            return Tree[i - 1];
        }
        public string ToInfx()
        {
            return _toInfx(1);
        }
        private string _toInfx(int i)
        {
            if (!double.TryParse(Tree[i - 1], out double d) && Tree[i - 1] != "e" && Tree[i - 1] != "pi")
            {
                return '('+_toInfx(2 * i) + " " + Tree[i - 1] + " " +_toInfx(2 * i + 1)+')';
            }
            return Tree[i - 1];
        }
        public double Evaluate()
        {
            return _evaluate(1);
        }
        private double _evaluate(int i)
        {
            if (double.TryParse(Tree[i - 1], out double d))
            {
                return Convert.ToDouble(Tree[i - 1]);
            }
            if (Tree[i - 1] == "e")
            {
                return double.E;
            }
            if (Tree[i - 1] == "pi")
            {
                return double.Pi;
            }
            if (Tree[i - 1] == "+")
            {
                return Convert.ToDouble(_evaluate(i*2)) + Convert.ToDouble(_evaluate(i * 2 + 1));
            }
            if (Tree[i - 1] == "-")
            {
                return Convert.ToDouble(_evaluate(i * 2)) - Convert.ToDouble(_evaluate(i * 2 + 1));
            }
            if (Tree[i - 1] == "*")
            {
                return Convert.ToDouble(_evaluate(i * 2)) * Convert.ToDouble(_evaluate(i * 2 + 1));
            }
            if (Tree[i - 1] == "/")
            {
                if (Convert.ToDouble(_evaluate(i * 2 + 1)) == 0)
                {
                    return double.NaN;
                }
                return Convert.ToDouble(_evaluate(i * 2)) / Convert.ToDouble(_evaluate(i * 2 + 1));
            }
            if (Tree[i - 1] == "^")
            {
                return Math.Pow(Convert.ToDouble(_evaluate(i * 2)), Convert.ToDouble(_evaluate(i * 2 + 1)));
            }
            if (Tree[i - 1] == "log")
            {
                return Math.Log(Convert.ToDouble(_evaluate(i * 2)), Convert.ToDouble(_evaluate(i * 2 + 1)));
            }
            if (Tree[i - 1] == "max")
            {
                if (Convert.ToDouble(_evaluate(i * 2)) > Convert.ToDouble(_evaluate(i * 2 + 1)))
                {
                    return Convert.ToDouble(_evaluate(i * 2));
                }
                return Convert.ToDouble(_evaluate(i * 2 + 1));
            }
            if (Tree[i - 1] == "min")
            {
                if (Convert.ToDouble(_evaluate(i * 2)) < Convert.ToDouble(_evaluate(i * 2 + 1)))
                {
                    return Convert.ToDouble(_evaluate(i * 2));
                }
                return Convert.ToDouble(_evaluate(i * 2 + 1));
            }
            if (Tree[i - 1] == "zpd" || Tree[i - 1] == "mod" || Tree[i - 1] == "%")
            {
                return Convert.ToDouble(_evaluate(i * 2)) % Convert.ToDouble(_evaluate(i * 2 + 1));
            }
            if (Tree[i - 1] == "dbz")
            {
                double l = Convert.ToDouble(_evaluate(i * 2));
                double r = Convert.ToDouble(_evaluate(i * 2 + 1));
                return (l - (l % r))/r;
            }
            return double.NaN;
        }
    }
}