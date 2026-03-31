using System.Text;

namespace PololetniUloha
{
    internal class Program
    {
        static List<string> LexSortToNthChar(List<string> list, int len)
        {
            for (int i = len-1;i>=0;i--)
            {
                List<string> newList = new List<string>();
                Queue<string>[] buckets = new Queue<string>[10];
                for (int j = 0; j < 10; j++)
                {
                    buckets[j] = new Queue<string>();
                }
                for (int j = 0;j < list.Count;j++)
                {
                    buckets[(int)list[j][i]-48].Enqueue(list[j]);
                }
                for (int j = 0; j < 10;j++)
                {
                    while(buckets[j].Count != 0)
                    {
                        newList.Add(buckets[j].Dequeue());
                    }
                }
                list = newList;
            }
            return list;
        }
        static List<Student>[] AddStudentToBucket1To5(Student zak, List<Student>[] buckets)
        {
            buckets[zak.Znamka-1].Add(zak);
            return buckets;
        }
        static int[] AddNumToBucket1To5(int num, int[] buckets)
        {
            buckets[num - 1]++;
            return buckets;
        }
        static List<Student> ConvertBucketsToList(List<Student>[] buckets)
        { 
            List<Student> list = new List<Student>();
            for(int i  = 0; i < buckets.Length;i++)
            {
                list.AddRange(buckets[i]);
            }
            return list;
        }
        static string ConvertBucketsToString(int[] prihradky)
        {
            StringBuilder setridenySeznamZnamek = new StringBuilder();
            for (int i = 0; i < prihradky.Length; i++)
            {
                setridenySeznamZnamek.Append(Convert.ToChar(i + 49), prihradky[i]);
            }
            return setridenySeznamZnamek.ToString();
        }
        static void Main(string[] args)
        {
            // (20b) 1. Seřaďte známky ze souboru znamky.txt od 1 do 5 algoritmem s lineární časovou složitostí vzhledem k počtu známek.
            // Vypište je na řádek a pak vypište i četnosti jednotlivých známek.
            Console.WriteLine("-----[Tragické Známky... T-T:]-----");
            using (StreamReader sr = new StreamReader(@"..\..\..\..\..\znamky.txt")) // otevření souboru pro čtení
            {
                int[] prihradky = new int[5];
                while (!sr.EndOfStream) // dokud jsme nedošli na konec souboru
                {
                    int znamka = Convert.ToInt16(sr.ReadLine()); // čteme známky po řádcích a převádíme je na číslo
                    AddNumToBucket1To5(znamka, prihradky);
                }
                Console.WriteLine(ConvertBucketsToString(prihradky));
                //Vypis podle cetnosti
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i<prihradky.Length; i++)
                {
                    sb.Append($"|{i+1}");
                    for (int j = 0; j < Math.Ceiling(Math.Log10(prihradky[i]))-1; j++)
                    {
                        sb.Append(' ');
                    }
                }
                sb.Append('|');
                Console.WriteLine(sb.ToString());

                foreach (int znamka in prihradky)
                {
                    Console.Write($"|{znamka}");
                }
                Console.WriteLine("|");
            }
            // => to, co jste pravděpodobně stvořili se nazývá Counting Sort



            // (40b) 2. Ze souboru znamky_prezdivky.csv vytvořte objekty typu Student se správně přiřazenou známkou a přezdívkou.
            // Seřaďte je podle známek (stabilně = dodržte pořadí v souboru) a vypište seřazené dvojice (znamka: přezdívka) - na každý řádek jednu.
            Console.WriteLine("-----[Anonymní Alkoholici/Studenti:]-----");
            using (StreamReader sr = new StreamReader(@"..\..\..\..\..\znamky_prezdivky.csv"))
            {
                List<Student>[] barrels = new List<Student>[5];
                for(int i = 0;i < 5;i++)
                {
                    barrels[i] = new List<Student>();
                }
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(";");
                    Student zak = new Student(line[0], Convert.ToInt32(line[1]));
                    AddStudentToBucket1To5(zak, barrels);
                }
                List<Student> students = ConvertBucketsToList(barrels);
                for (int i = 0; i < students.Count; i++)
                {
                    Console.WriteLine($"{students[i].Prezdivka},{students[i].Znamka}");
                }
            }
            // => to, co jste pravděpodobně stvořili se nazývá Bucket Sort (přihrádkové řazení)




            // (10b) 3. Určete časovou a prostorovou složitost algoritmu z 2. úkolu
            /*
            O(n) časová složitost, O(n) prostorová složitost

            */
            // (+60b) 4. BONUS: Napište kód, který bude řadit lexikograficky velká čísla v lineárním čase. Využijte dat ze souboru velka_cisla.txt
            Console.WriteLine("-----[Velká čisla:]-----");
            using (StreamReader sr = new StreamReader(@"..\..\..\..\..\velka_cisla.txt"))
            {
                List<string> numbers = new List<string>();
                int len = 0;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if(line.Length>len)
                        len = line.Length;
                    numbers.Add(line);
                }
                numbers = LexSortToNthChar(numbers,len);
                for(int i =0; i < numbers.Count; i++)
                {
                    Console.WriteLine($"{numbers[i]}");
                }
            }
        }
    }

    class Student
    {
        public string Prezdivka { get; } // tím, že je zde pouze get říkáme, že tato vlastnost třídy Student jde mimo třídu pouze číst, nikoli upravovat
        public int Znamka { get; }
        public Student(string prezdivka, int znamka) // konstruktor třídy
        {
            // použitím samotného { get; } také říkáme, že tyto vlastnosti jdou nastavit nejpozději v konstruktoru - tedy v této metodě
            Prezdivka = prezdivka;
            Znamka = znamka;
        }
    }
}
