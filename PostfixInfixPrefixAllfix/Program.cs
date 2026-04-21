using System.ComponentModel;
using System.Numerics;
using System.Text;
namespace PostfixInfixPrefixAllfix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string mode = "Menu";
            while (true)
            {
                Console.Clear();
                if (mode == "Menu")
                {
                    Console.WriteLine(Tex.T_0Title());
                    Console.WriteLine("┌" + new string('─', Tex.T_BoxWidth0()) + "┐\n│" + Tex.T_00() + new string(' ', Tex.T_BoxWidth0() - Tex.T_00().Length) + "│\n├" + new string('─', Tex.T_BoxWidth0()) + "┤\n│" + Tex.T_01() + new string(' ', Tex.T_BoxWidth0() - Tex.T_01().Length) + "│\n│" + Tex.T_02() + new string(' ', Tex.T_BoxWidth0() - Tex.T_02().Length) + "│\n└" + new string('─', Tex.T_BoxWidth0()) + "┘");
                    mode = Console.ReadLine();
                }
                else if (mode == "Post")
                {
                    Console.WriteLine(Tex.T_1Title());
                    Console.WriteLine("┌" + new string('─', Tex.T_10().Length) + "┐\n│" + Tex.T_10() + "│\n└" + new string('─', Tex.T_10().Length) + "┘");
                    string input = Console.ReadLine();
                    Queue<string> q = Calculator.ToQueue(input);
                    string result = Calculator.CalculatePostfix(q);
                    Console.WriteLine("┌" + new string('─', result.Length) + $"┐\n│{result}│\n└" + new string('─', result.Length) + "┘");

                    Console.WriteLine("┌" + new string('─', Tex.T_90().Length) + "┐\n│" + Tex.T_90() + "│\n└" + new string('─', Tex.T_90().Length) + "┘");
                    string command = Console.ReadLine();
                    if (command != null)
                    {
                        if (command.Length != 0)
                        {
                            mode = command;
                        }
                    }
                }
                else if (mode == "Pref")
                {
                    Console.WriteLine(Tex.T_2Title());
                    Console.WriteLine("┌" + new string('─', Tex.T_20().Length) + "┐\n│" + Tex.T_20() + "│\n└" + new string('─', Tex.T_20().Length) + "┘");
                    string input = Console.ReadLine();
                    Queue<string> q = Calculator.ToQueue(input);
                    string result = Calculator.CalculatePrefix(q);
                    Console.WriteLine("┌" + new string('─', result.Length) + $"┐\n│{result}│\n└" + new string('─', result.Length) + "┘");

                    Console.WriteLine("┌" + new string('─', Tex.T_90().Length) + "┐\n│" + Tex.T_90() + "│\n└" + new string('─', Tex.T_90().Length) + "┘");
                    string command = Console.ReadLine();
                    if (command.Length != 0)
                    {
                        mode = command;
                    }
                }
                else
                {
                    Console.WriteLine(Tex.T_9Title());
                    Console.WriteLine("┌─────────────────────┐\n│Unknown Command Error│\n└─────────────────────┘");

                    Console.Write("┌────────────────────────────┐\n│[<─┘] - zpět k hlavnímu MENU│\n└────────────────────────────┘\n");
                    string input = Console.ReadLine();
                    if (input == "")
                    {
                        mode = "Menu";
                    }
                    else
                    {
                        mode = input;
                    }
                }
            }
        }
        public static class Tex
        {
            public static string T_0Title()
            {
                return " ___ __ __   __  __   __     _________  ________  ______   ________  __     __   ______   __   __       ____      \r\n/__//_//_/\\ /_/\\/_/\\ /_/\\   /________/\\/_______/\\/_____/\\ /_______/\\/__/\\ /__/\\ /_____/\\ /_/\\ /_/\\     /___/|     \r\n\\::\\| \\| \\ \\\\:\\ \\:\\ \\\\:\\ \\  \\__.::.__\\/\\__.::._\\/\\::::_\\/_\\__.::._\\/\\ \\::\\\\:.\\ \\\\:::_ \\ \\\\:\\ \\\\ \\ \\   _|___|/_   \r\n \\:.      \\ \\\\:\\ \\:\\ \\\\:\\ \\    \\::\\ \\     \\::\\ \\  \\:\\/___/\\  \\::\\ \\  \\_\\::_\\:_\\/ \\:\\ \\ \\ \\\\:\\ \\\\ \\ \\ /_______/\\   \r\n  \\:.\\-/\\  \\ \\\\:\\ \\:\\ \\\\:\\ \\____\\::\\ \\    _\\::\\ \\__\\:::._\\/  _\\::\\ \\__ _\\/__\\_\\_/\\\\:\\ \\ \\ \\\\:\\_/.:\\ \\\\::: _  \\ \\  \r\n   \\. \\  \\  \\ \\\\:\\_\\:\\ \\\\:\\/___/\\\\::\\ \\  /__\\::\\__/\\\\:\\ \\   /__\\::\\__/\\\\ \\ \\ \\::\\ \\\\:\\_\\ \\ \\\\ ..::/ / \\:.(_)  \\ \\ \r\n    \\__\\/ \\__\\/ \\_____\\/ \\_____\\/ \\__\\/  \\________\\/ \\_\\/   \\________\\/ \\_\\/  \\__\\/ \\_____\\/ \\___/_(   \\__\\-\\__\\/     \r\n       ___   ___   ________   __       ___   ___   __  __   __       ________   __  __   ___   ___   ________      \r\n      /___/\\/__/\\ /_______/\\ /_/\\     /___/\\/__/\\ /_/\\/_/\\ /_/\\     /_______/\\ /_/\\/_/\\ /___/\\/__/\\ /_______/\\     \r\n      \\::.\\ \\\\ \\ \\\\::: _  \\ \\\\:\\ \\    \\::.\\ \\\\ \\ \\\\:\\ \\:\\ \\\\:\\ \\    \\::: _  \\ \\\\_\\|__\\/_\\::.\\ \\\\ \\ \\\\::: _  \\ \\    \r\n       \\:: \\/_) \\ \\\\::(_)  \\ \\\\:\\ \\    \\:: \\/_) \\ \\\\:\\ \\:\\ \\\\:\\ \\    \\::(_)  \\ \\ /_____/\\\\:: \\/_) \\ \\\\::(_)  \\ \\   \r\n        \\:. __  ( ( \\:: __  \\ \\\\:\\ \\____\\:. __  ( ( \\:\\ \\:\\ \\\\:\\ \\____\\:: __  \\ \\\\::___\\/_\\:. __  ( ( \\:: __  \\ \\  \r\n         \\: \\ )  \\ \\ \\:.\\ \\  \\ \\\\:\\/___/\\\\: \\ )  \\ \\ \\:\\_\\:\\ \\\\:\\/___/\\\\:.\\ \\  \\ \\\\:\\/___/\\\\: \\ )  \\ \\ \\:.\\ \\  \\ \\ \r\n          \\__\\/\\__\\/  \\__\\/\\__\\/ \\_____\\/ \\__\\/\\__\\/  \\_____\\/ \\_____\\/ \\__\\/\\__\\/ \\_____\\/ \\__\\/\\__\\/  \\__\\/\\__\\/ \r\n ";
            }
            public static string T_2Title()
            {
                return " ______   ______    ______             ______   ________  __     __     \r\n/_____/\\ /_____/\\  /_____/\\           /_____/\\ /_______/\\/__/\\ /__/\\    \r\n\\:::_ \\ \\\\:::_ \\ \\ \\::::_\\/_   _______\\::::_\\/_\\__.::._\\/\\ \\::\\\\:.\\ \\   \r\n \\:(_) \\ \\\\:(_) ) )_\\:\\/___/\\ /______/\\\\:\\/___/\\  \\::\\ \\  \\_\\::_\\:_\\/   \r\n  \\: ___\\/ \\: __ `\\ \\\\::___\\/_\\__::::\\/ \\:::._\\/  _\\::\\ \\__ _\\/__\\_\\_/\\ \r\n   \\ \\ \\    \\ \\ `\\ \\ \\\\:\\____/\\          \\:\\ \\   /__\\::\\__/\\\\ \\ \\ \\::\\ \\\r\n    \\_\\/     \\_\\/ \\_\\/ \\_____\\/           \\_\\/   \\________\\/ \\_\\/  \\__\\/\r\n                                                                         ";
            }
            public static string T_1Title()
            {
                return " ______   ______   ______   _________         ______   ________  __     __     \r\n/_____/\\ /_____/\\ /_____/\\ /________/\\       /_____/\\ /_______/\\/__/\\ /__/\\    \r\n\\:::_ \\ \\\\:::_ \\ \\\\::::_\\/_\\__.::.__\\/_______\\::::_\\/_\\__.::._\\/\\ \\::\\\\:.\\ \\   \r\n \\:(_) \\ \\\\:\\ \\ \\ \\\\:\\/___/\\  \\::\\ \\ /______/\\\\:\\/___/\\  \\::\\ \\  \\_\\::_\\:_\\/   \r\n  \\: ___\\/ \\:\\ \\ \\ \\\\_::._\\:\\  \\::\\ \\\\__::::\\/ \\:::._\\/  _\\::\\ \\__ _\\/__\\_\\_/\\ \r\n   \\ \\ \\    \\:\\_\\ \\ \\ /____\\:\\  \\::\\ \\          \\:\\ \\   /__\\::\\__/\\\\ \\ \\ \\::\\ \\\r\n    \\_\\/     \\_____\\/ \\_____\\/   \\__\\/           \\_\\/   \\________\\/ \\_\\/  \\__\\/\r\n                                                                                ";
            }
            public static string T_9Title()
            {
                return " ______   ______    _____    ______   ______    __    __    __       \r\n/_____/\\ /_____/\\  /_____/\\ /_____/\\ /_____/\\  /__/\\ /__/\\ /__/\\     \r\n\\:::_:\\ \\\\:::_ \\ \\ \\:::_:\\ \\\\:::_ \\ \\\\:::_ \\ \\ \\.:\\ \\\\.:\\ \\\\.:\\ \\    \r\n   /_\\:\\ \\\\:(_) ) )_   _\\:\\| \\:\\ \\ \\ \\\\:(_) ) )_\\::\\ \\\\::\\ \\\\::\\ \\   \r\n   \\::_:\\ \\\\: __ `\\ \\ /::_/__ \\:\\ \\ \\ \\\\: __ `\\ \\\\__\\/_\\__\\/_\\__\\/_  \r\n   /___\\:\\ '\\ \\ `\\ \\ \\\\:\\____/\\\\:\\_\\ \\ \\\\ \\ `\\ \\ \\ /__/\\ /__/\\ /__/\\ \r\n   \\______/  \\_\\/ \\_\\/ \\_____\\/ \\_____\\/ \\_\\/ \\_\\/ \\__\\/ \\__\\/ \\__\\/ \r\n                                                                      ";
            }
            public static int T_BoxWidth0()
            {
                return 45;
            }
            public static int T_BoxWidth1()
            {
                return 100;
            }
            public static string T_00()
            {
                return "Vyberte z jeden následujících módů:";
            }
            public static string T_01()
            {
                return "- Výpočet Postfix vstupu: [Post]";
            }
            public static string T_02()
            {
                return "- Výpočet Prefix vstupu:  [Pref]";
            }
            public static string T_10()
            {
                return "Napište výraz, který chcete spočítat(postfix s double operandy a operátory + ,- ,* ,/ ,^ , log)";
            }
            public static string T_20()
            {
                return "Napište výraz, který chcete spočítat(prefix s double operandy a operátory + ,- ,* ,/ ,^ , log)";
            }
            public static string T_90()
            {
                return "[<─┘] - AC, [Menu] - Zpět k hlavnímu Menu";
            }
        }
        public static class Calculator
        {
            public static Queue<string> ToQueue(string input)
            {
                char[] charrrayiezedInput = input.ToCharArray();
                Queue<string> q = new Queue<string>();
                StringBuilder mem = new StringBuilder();
                for (int i = 0;i < input.Length;i++)
                {
                    if (charrrayiezedInput[i] == ' ')
                    {
                        q.Enqueue(mem.ToString());
                        mem.Clear();
                    }
                    else
                    {
                        mem.Append(charrrayiezedInput[i]);
                    }
                }
                q.Enqueue(mem.ToString());
                return q;
            }
            public static string CalculatePostfix(Queue<string> q)
            {
                Stack<string> s = new Stack<string>();
                while (q.Count > 0)
                {
                    s.Push(q.Dequeue());
                    if (!double.TryParse(s.Peek(), out double d))
                    {
                        if (s.Count < 3)
                        {
                            return "Nedefinovaný výraz: Chybějící Operand(y)";
                        }
                        else
                            {
                                string o = s.Pop();
                                string r = s.Pop();
                                string l = s.Pop();
                                string result = Operation(o, r, l);
                                if (result[0] == 'O')
                                {
                                    return "Nedefinovaný výraz: Dělení 0";
                                }
                                else if (result[0] == 'Q')
                                {
                                    char[] charrayizedResult = result.ToCharArray();
                                    StringBuilder sb = new StringBuilder();
                                    sb.Append("Nedefinovaný výraz: Nelegální operand: \"");
                                    for (int i = 1; i < charrayizedResult.Length; i++)
                                    {
                                        sb.Append(charrayizedResult[i]);
                                    }
                                    return sb.Append('\"').ToString();
                                }
                                else if (result[0] == 'U')
                                {
                                    char[] charrayizedResult = result.ToCharArray();
                                    StringBuilder sb = new StringBuilder();
                                    sb.Append("Nedefinovaný výraz: Neznámý operátor: \"");
                                    for (int i = 1; i < charrayizedResult.Length; i++)
                                    {
                                        sb.Append(charrayizedResult[i]);
                                    }
                                    return sb.Append('\"').ToString();
                                }
                                s.Push(result);
                            }
                    }
                }
                if (s.Count > 1)
                {
                    return "Nedefinovaný výraz: Chybějící Operátor(y)";
                }
                return s.Pop();
            }
            public static string CalculatePrefix(Queue<string> q)
            {
                Stack<string> s = new Stack<string>();
                while (q.Count > 0)
                {
                    s.Push(q.Dequeue());
                    if (s.Count > 1)
                    {
                        while (double.TryParse(s.Peek(), out double d))
                        {
                            if (s.Count < 2)
                            {
                                break;
                            }
                            if (double.TryParse(s.ToArray()[1], out double t))
                            {
                                if (s.Count <3)
                                {
                                    return "Nedefinovaný výraz: Chybějící Operátor(y)";
                                }
                                string r = s.Pop();
                                string l = s.Pop();
                                string o = s.Pop();
                                string result = Operation(o, r, l);
                                if (result[0] == 'O')
                                {
                                    return "Nedefinovaný výraz: Dělení 0";
                                }
                                else if (result[0] == 'Q')
                                {
                                    char[] charrayizedResult = result.ToCharArray();
                                    StringBuilder sb = new StringBuilder();
                                    sb.Append("Nedefinovaný výraz: Nelegální operand: \"");
                                    for (int i = 1; i < charrayizedResult.Length; i++)
                                    {
                                        sb.Append(charrayizedResult[i]);
                                    }
                                    return sb.Append('\"').ToString();
                                }
                                else if (result[0] == 'U')
                                {
                                    char[] charrayizedResult = result.ToCharArray();
                                    StringBuilder sb = new StringBuilder();
                                    sb.Append("Nedefinovaný výraz: Neznámý operátor: \"");
                                    for (int i = 1; i < charrayizedResult.Length; i++)
                                    {
                                        sb.Append(charrayizedResult[i]);
                                    }
                                    return sb.Append('\"').ToString();
                                }
                                s.Push(result);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                if (s.Count > 1)
                {
                    return "Nedefinovaný výraz: Chybějící Operand(y)";
                }
                return s.Pop();
            }
            private static string Operation(string o, string right, string left)
            {
                if (!double.TryParse(right, out double d))
                {
                return "Q"+right;
                }
                if (!double.TryParse(left, out double t))
                {
                return "Q"+left;
                }
                double r = Convert.ToDouble(right);
                double l = Convert.ToDouble(left);
                double result = 0;
                if (o == "+")
                {
                    result = r + l;
                }
                else if (o == "-")
                {
                    result = l - r;
                }
                else if (o == "*")
                {
                    result = r * l;
                }
                else if (o == "/")
                {
                    if (r == 0)
                    {
                        return "O";
                    }
                    result = l / r;
                }
                else if (o == "^")
                {
                    result = Math.Pow(l,r);
                }
                else if (o == "log")
                {
                    result = Math.Log(l, r);
                }
                else
                {
                    return "U"+o;
                }
                return Convert.ToString(result);
            }
        }
    }
}
