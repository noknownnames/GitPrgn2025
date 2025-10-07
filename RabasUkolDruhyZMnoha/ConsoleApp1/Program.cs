using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.IO.IsolatedStorage;
using System.Linq;


namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //0,3,7,12,76,8,7,13,32,56,3,65,12,420,35,56,100,21 - test
            static int FindMax(int[] numArray)
            {
                int biggestNum = numArray[0];
                foreach (int num in numArray)
                {
                    if (num > biggestNum)
                    {
                        biggestNum = num;
                    }
                }
                return biggestNum;
            }

            static int[] SortArray(int[] numArray)
            {
                bool isSorted = false;
                while (isSorted == false)
                {
                    isSorted = true;
                    for (int num = 0; num < numArray.Length - 1; num++)
                    {
                        if (numArray[num] > numArray[num + 1])
                        {
                            int mem = numArray[num];
                            numArray[num] = numArray[num + 1];
                            numArray[num + 1] = mem;
                            isSorted = false;
                        }
                    }
                }
                return numArray;
            }

            static int BinarySearch(int[] numArray,int elementInQuestion)
            {
                int split = numArray.Length/2;
                bool isFound = false;
                List<int> lastSplits = new List<int> { -1, -1, -1 };
                int loopNumber = 1;
                while (isFound == false)
                {
                    Console.WriteLine(split);
                    int observedElement = numArray[split];
                    lastSplits.RemoveAt(0);
                    if (observedElement == elementInQuestion)
                    {
                        isFound = true;
                        return split;
                    }
                    else if (observedElement > elementInQuestion)
                    {
                        lastSplits.Add(split);
                        split -= (split / (2 * loopNumber))+1;
                        if (split < 0)
                        {
                            split = 0;
                        }
                    }
                    else if (observedElement < elementInQuestion)
                    {
                        lastSplits.Add(split);
                        split += (split / (2*loopNumber))+1;
                        if (split > numArray.Length-1)
                        {
                            split = numArray.Length-1;
                        }
                    }
                    if (lastSplits[0] == split || lastSplits[1] == split)
                    {
                        isFound = true;
                    }
                    loopNumber++;
                }
                return -1;
            }

            bool loop = true;
            Console.WriteLine("Napište list celých čísel n oddělených čárkami:");
            string inputString = Console.ReadLine();
            int[] input = Array.ConvertAll(inputString.Split(","), int.Parse);

            while (loop)
            {
                Console.Write("\n------------\n[r]Přepíše původní pole novým\r\n[i]Vypíše pole\r\n[1]Nalezne největší prvek z pole\r\n[2]Seřadí prvky v poli\r\n[3]Nalezne daný prvek v seřazeném poli, nebo příjde s nepořízenou. Ubožák\r\n[x]ukončí program\n------------\n");
                string booggleland = Console.ReadLine();
                Console.Write("\n");
                if (booggleland == "1")
                {
                    Console.Write(FindMax(input));
                }
                else if (booggleland == "2")
                {
                    SortArray(input);
                    Console.Write("nové pole: ");
                    foreach (int i in input)
                    {
                        Console.Write($"{i},");
                    }
                }
                else if (booggleland == "3")
                {
                    Console.Write("Jaký prvek hledáte ? : ");
                    int searchedElement = Convert.ToInt32(Console.ReadLine());
                    Console.Write("\n");
                    Console.Write($"prvek je na: {BinarySearch(input,searchedElement)}. místě");
                }
                else if (booggleland == "i")
                {
                    foreach (int i in input)
                    {
                        Console.Write($"{i},");
                    }
                }
                else if (booggleland == "r")
                {
                    Console.WriteLine("Napište nový list celých čísel n oddělených čárkami:");
                    inputString = Console.ReadLine();
                    input = Array.ConvertAll(inputString.Split(","), int.Parse);
                }
                else if (booggleland == "x")
                {
                    loop = false;
                }
            }

        }
    }
}