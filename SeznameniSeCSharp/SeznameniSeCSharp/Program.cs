using System.Reflection.Metadata;
using static System.Net.Mime.MediaTypeNames;

// PROGRAMOVACÍ PARADIGMATA JAZYKA C#
//      - KOMPILOVANÝ = 



namespace SeznameniSeCSharp
//namespace = jmenný prostor.
//  Slouží k organizaci kódu a zabraňuje kolizím jmen.
//  Když máš více tříd se stejným názvem, namespace určí, kterou myslíš.
//  Tady se vše nachází v „balíčku“ SeznameniSeCSharp.

{ // složené závorky vymezují určité bloky kódu, omezují viditelnost a existenci tříd, funkcí, proměnných, ...
    internal class Program
        // class = definice třídy. V C# musí být veškerý kód uvnitř třídy.
        // Program = jméno třídy, obvykle obsahuje vstupní bod programu.
        // internal = přístupový modifikátor → znamená, že třída je dostupná jen v rámci stejného projektu/assembly(není veřejná mimo něj).
    {
        static void Main(string[] args)
        // static = metoda patří třídě, ne instanci → nemusíš vytvořit objekt Program, aby se dala spustit.
        //      ze statické metody/funkce můžeme volat pouze statickou metodu/funkci
        // void = metoda nic nevrací.
        //      void -> nevracíme nic -> mluvíme o METODĚ
        //      pokud vracíme konkrétní datový typ -> mluvíme o FUNKCI 
        // Main = speciální metoda → vstupní bod programu.Tady začíná běh aplikace.
        //      C# NOTACE PRO POJMENOVÁNÍ
        //          třídy, funkce, metody, veřejné vlastnosti: PascalCase notace
        //          privátní a lokální proměnné:  camelCasel notace
        //          v Pythonu: snake_case, v C# ne
        // string[] args = pole argumentů příkazové řádky, pokud program spouštíš s parametry (např.myApp.exe test.txt).
        {
            Console.WriteLine("Hello, World!");
            // Console = třída v .NET knihovně pro práci s konzolí (výstup/vstup textu).
            // WriteLine = metoda, která vypíše text na konzoli a ukončí řádek.
            //      Vypíše tedy Hello, World! na obrazovku.



            // ZÁKLADNÍ DATOVÉ TYPY 
            //      C# je staticky typovaný jazyk, takže pokud máte datový typ uveden špatně, program se ani nespustí
            // více na: https://learn.microsoft.com/cs-cz/cpp/cpp/data-type-ranges?view=msvc-170

            // malé celé číslo:     byte            1B paměti   rozsah: 0 až 255
            // celé číslo:          integer, int    4B          rozsah: -2 147 483 648 až 2 147 483 647 (-2^31 až 2^31 - 1)
            int i; // **deklarace** celočíselné proměnné 
            i = 0; // **inicializace** její hodnoty
            int j = 0; // lze spojit do jednoho řádku

            // desetinné číslo:     float / double  4B / 8B     rozsah: 3,4E +/- 38 (sedm číslic) / 1,7E +/- 308 (patnáct číslic)
            float f = 0.0f;
            f = float.PositiveInfinity; // float má i hodnotu nekonečna

            // pozor na míchání typů u aritmetických operací
            //int div = i / j; // vrátí celé číslo - zaokrouhluje dolů
            float fdiv = i / f; // je-li v operaci desetinné číslo, výsledkem je také desetinné číslo

            // bool - pravdivostní hodnota
            bool b = true;
            b = false;

            // textový řetězec - string: chová se jako neměnitelné pole znaků (charů)
            string s = "ahoj";
            char ch2 = 'A';
            char ch = s[2]; // v ch je teď uloženo 'o'
            // s[1] = 'a'; // toto nejde, string je neměnitelný

            // pole - obsahuje n prvků stejného typu, n je předem dané a neměnitelné!
            //      typ[] nazevPole = new typ[n]
            int[] cisla = new int[10]; // v paměti vytvoří pole pro 10 čísel typu int -> spotřebuje se 40B paměti
            cisla[0] = 3; // můžeme upravovat hodnotu prvků pole pomocí indexů
            char[] znaky = { 'a', 'h', 'o', 'j' }; // takto lze definovat pole s konkrétními prvky; má od teď neměnnou délku 4
            int delka = cisla.Length; // počet prvků v poli

            // List - obsahuje prvky stejného typu, ale prvky můžeme přidávat i odebírat -> má proměnlivou délku
            //      List<typ> nazevListu = new List<typ>();
            List<int> cisla2 = new List<int>();
            cisla2.Add(3);      // přidá prvek na konec listu
            cisla2.Add(4);
            cisla2.Remove(3);   // odebere první prvek s hodnotou 3 (hledá od začátku listu)
            cisla2.RemoveAt(0); // odebere prvek na daném indexu
            int delka2 = cisla2.Count; // počet prvků v listu


            // KLÍČOVÉ FUNKCE

            // Načtení vstupu:
            string s2 = Console.ReadLine(); // přečte řádek vstupu a vrátí string, který uloží do s2
            int vstupniCislo = Convert.ToInt32(Console.ReadLine()); // chceme-li načíst číslo, musíme ho překonvertovat ze stringu na int

            // Výpis na výstup:
            Console.Write("Ahoj"); // narozdíl od WriteLine automaticky NEzalomí řádek
            Console.WriteLine("Ahoj světe");
            Console.WriteLine($"hodnota i je {i}."); // lze to i formátovaně pomocí $ a {promenna}


            // KLÍČOVÁ SYNTAX
            // více na https://www.w3schools.com/cs/index.php
            
            // podmínky
            if (i > 0)
            {
                Console.WriteLine("i je větší než nula");
                
            }
            else if (i < 0 && i > -5) // && - AND, || - OR
            {
                Console.WriteLine("i je menší než nula, ale větší než -5");
            }
            else
            {
                Console.WriteLine("i je menší nebo rovno než -5");
            }
            
            // while cyklus
            while (i > 0)
            {
                i--;    // zmenšuj i o 1
                        // i++; zvyšuje i o 1
            }

            // for cyklus
            
            for (int i2 = 0; i2 < 10; i2++)
            {
                Console.WriteLine(i2); // postupně se vypíše 0 až 9, int je pomocí CWL překonvertován na sovu stringovou podobu a vypsán
            }

            // foreach
            foreach(int cislo in cisla2) // hezky čitelný způsob jak procházet prvky listu
            {
                Console.WriteLine(cislo);
            }
        }
    }
}
