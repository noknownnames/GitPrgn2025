using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Z_Rima_Do_Koncin_Arabskych_a_zpet//TODO: Zkontrolovat, zadali je vstup opravdu římské číslo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("┌───────────────────────────────────────────────────────────────────────────────────────────────────────────┐\r\n│ __\\/_,  _/,   __, _,  ____,  __, ,  __/_,               ____,  ____,    ____,  ____   ____,  __, ,  __/_, │\r\n│(-|__)  (-|   (-|\\/|  (-(__  ( |_/  (-|_,   /___,____\\  (-/_|  (-|__)   (-/_|  (-|__) (-(__  ( |_/  (-|_,  │\r\n│ _|  \\,  _|_,  _| _|,  ____)  _| \\,  _|__,  \\        /  _/  |,  _|  \\,  _/  |,  _|__)  ____)  _| \\,  _|__, │\r\n├───────────────────────────────────────────────────────────────────────────────────────────────────────────┤\r\n│                                            PŘEVONDNÍK ČÍSLIC                                              │\r\n└───────────────────────────────────────────────────────────────────────────────────────────────────────────┘\r\n");
                string input = Console.ReadLine();
                if (Int32.TryParse(input, out int result))
                {
                    Console.WriteLine(ConvertVal.ToRoman(Convert.ToInt32(input)));
                }
                else if (ConvertVal.IsRomanNumeral(input))
                {
                    Console.WriteLine(ConvertVal.ToArabic(input));
                }
                else
                {
                    Console.WriteLine("To...Ani není číslo. Co s tím mám dělat? Zmáčkni libovolnou klávesu a zkus to znovu.");
                }
                Console.ReadKey();
            }
        }
        public static class ConvertVal
        {
            public static int ToArabic(string input)
            {
                Dictionary<char, int> arabic = new Dictionary<char, int>() { ['M'] = 1000, ['D'] = 500, ['C'] = 100, ['L'] = 50, ['X'] = 10, ['V'] = 5, ['I'] = 1 };
                StringBuilder sb = new StringBuilder();
                int result = 0;
                for (int i = 0; i < input.Length-1; i++)
                {
                    if (arabic[input[i]] < arabic[input[i + 1]])
                    { 
                        result -= arabic[input[i]];
                    }
                    else
                    {
                        result += arabic[input[i]];
                    }
                }
                result += arabic[input[input.Length-1]];
                return result;
            }
            public static string ToRoman(int input)
            {
                if (input <= 0)
                {
                    return "range errorum: numerus maior quam nulla esse debet!";
                }
                else if (input >= 4000)
                {
                    return "range errorum: Numerus minor quam MMMCMXCIX plus I esse debet!";
                }
                StringBuilder sb = new StringBuilder();
                sb.Append(new string('M',(input-input%1000)/1000));
                input = input % 1000;

                sb.Append(new string('C', (input - input % 900) / 900));
                sb.Append(new string('M', (input - input % 900) / 900));
                input = input % 900;

                sb.Append(new string('D', (input - input % 500) / 500));
                input = input % 500;

                sb.Append(new string('C', (input - input % 400) / 400));
                sb.Append(new string('D', (input - input % 400) / 400));
                input = input % 400;

                sb.Append(new string('C', (input - input % 100) / 100));
                input = input % 100;

                sb.Append(new string('X', (input - input % 90) / 90));
                sb.Append(new string('C', (input - input % 90) / 90));
                input = input % 90;

                sb.Append(new string('L', (input - input % 50) / 50));
                input = input % 50;

                sb.Append(new string('X', (input - input % 40) / 40));
                sb.Append(new string('L', (input - input % 40) / 40));
                input = input % 40;

                sb.Append(new string('X', (input - input % 10) / 10));
                input = input % 10;

                sb.Append(new string('I', (input - input % 9) / 9));
                sb.Append(new string('X', (input - input % 9) / 9));
                input = input % 9;

                sb.Append(new string('V', (input - input % 5) / 5));
                input = input % 5;

                sb.Append(new string('I', (input - input % 4) / 4));
                sb.Append(new string('V', (input - input % 4) / 4));
                input = input % 4;

                sb.Append(new string('I', input));
                return sb.ToString();
            }
            public static bool IsRomanNumeral(string input)
            {
                string strRegEx = @"^M{0,3}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$";
                Regex re = new Regex(strRegEx);
                if (re.IsMatch(input))
                    return (true);
                else
                    return (false);
            }
        }
    }
}
