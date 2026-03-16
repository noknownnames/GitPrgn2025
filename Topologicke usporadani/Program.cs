using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
/*
 ---BONUSOVÉ ZAMYŠLENÍ---
1. ---
2. vstup, který se nedá takto uspořádat: zyx xyx xyz xyx 
 */
namespace Topologicke_usporadani
{
    internal class Program
    {
        static List<int> TopologicalySortVertices(int[,] inputMatrix)
        {
            int NumberOfVerts = inputMatrix.GetLength(0);
            List<int> sortedVertices = new List<int>();
            char[] verticeStatus = new char[NumberOfVerts];//[o]pen,[u]ndiscovered,[c]losed
            for (int i = 0; i < sortedVertices.Count; i++)
                verticeStatus[i] = 'u';
            for (int i = 0; i < sortedVertices.Count; i++)
            {
                if (verticeStatus[i] == 'u')
                    _dfs(i);
            }
            void _dfs(int vertex)
            {
                verticeStatus[vertex] = 'o';
                for (int i = 0; i < NumberOfVerts; i++)
                {
                    if (inputMatrix[vertex, i] == -1)
                    {
                        if (verticeStatus[i] == 'o')
                            throw new Exception("Graf obsahuje cyklus, nelze seřadit!");
                        if (verticeStatus[i] == 'u')
                            _dfs(i);
                    }
                    verticeStatus[vertex] = 'c';
                    sortedVertices.Add(i);
                }
            }
            //otáčíme pole o 180
            List<int> reversedSortedVertices = new List<int>();
            for (int i = 0; i < sortedVertices.Count; i++)
            {
                sortedVertices[i] = reversedSortedVertices[sortedVertices.Count - i];
            }
            return reversedSortedVertices;
        }
        static List<char>[] TurnSringInputIntoListOfCharArrays(string input) 
        {
            string[] inputArray = input.Split();
            List<char>[] charArrayOfLists = new List<char>[inputArray.Length];
            for (int i = 0; i < inputArray.Length; i++)
            {
                charArrayOfLists[i] = [];
                char[] charArray = inputArray[i].ToCharArray();
                for (int j = 0; j < charArray.Length; j++)
                {
                    charArrayOfLists[i].Add(charArray[j]);
                }
            }

            return charArrayOfLists;
        }
        static List<char> SampleEachUniqueCharacter(List<char>[] charArrayOfLists)
        {
            List<char> samples = new List<char>();
            for (int i = 0; i < charArrayOfLists.Length; i++)
            {
                foreach (char character in charArrayOfLists[i])
                {
                    bool isCharSampled = false;
                    foreach (char sample in samples)
                    {
                        if (character == sample )
                        {
                            isCharSampled = true;
                        }
                    }
                    if (!isCharSampled)
                    {
                        samples.Add(character);
                    }
                }
            }
            return samples;
        }
        static List<char[]> SampleRelationshipsBetweenCharacters(List<char>[] listOfCharArrays)
        {
            List<char[]> earlierToLaterRelationshipsInAlphabet = new List<char[]>();
            for (int i = 0; i < listOfCharArrays.Length - 1; i++)
            {
                int arrayLen = listOfCharArrays[i].Count;
                int secondArrayLen = listOfCharArrays[i + 1].Count;
                if (arrayLen > secondArrayLen)
                {
                    arrayLen = secondArrayLen;
                }
                /*hledáme 2x2 prostor v našem poli listů 
                     A|A B| 
                     A|A C|
                     B A D
                    ve kterém můžeme porovnat, která písmena po sobě musí následovat 
                //doublecomment: ^^^počkat, to vlastně nedává smysl...
                */
                for (int j = arrayLen - 1; j > 0; j--)
                {
                    if (listOfCharArrays[i][j - 1] == listOfCharArrays[i + 1][j - 1] && listOfCharArrays[i][j] != listOfCharArrays[i + 1][j])
                    {
                        Console.WriteLine($"{listOfCharArrays[i][j]},{listOfCharArrays[i+1][j]}");
                        earlierToLaterRelationshipsInAlphabet.Add([Convert.ToChar(listOfCharArrays[i][j]), Convert.ToChar(listOfCharArrays[i+1][j])]);
                    }
                }
                if (listOfCharArrays[i][0] != listOfCharArrays[i + 1][0])//pro případ, kdy porovnáváme první písmeno v řádku
                {
                    Console.WriteLine($"{listOfCharArrays[i][0]};{listOfCharArrays[i+1][0]}");
                    earlierToLaterRelationshipsInAlphabet.Add([Convert.ToChar(listOfCharArrays[i][0]), Convert.ToChar(listOfCharArrays[i+1][0])]);
                }
            }
            return earlierToLaterRelationshipsInAlphabet;
        }
        static Dictionary<int, char> CreateNumberLetterPairs(List<char>uniqueCharacterList)
        {
            Dictionary<int,char> dictionary = new Dictionary<int,char>();
            for (int i = 0; i < uniqueCharacterList.Count; i++)
            {
                dictionary.Add(i, uniqueCharacterList[i]);
            }
            return dictionary;
        }
        static Dictionary<char, int> CreateReversedNumberLetterPairs(List<char> uniqueCharacterList)
        {
            Dictionary<char, int> dictionary = new Dictionary<char, int>();
            for (int i = 0; i < uniqueCharacterList.Count; i++)
            {
                dictionary.Add(uniqueCharacterList[i],i);
            }
            return dictionary;
        }
        static int[,] RelationshipDictionaryToIncidenceMatrix(List<char[]> earlierToLaterRelationshipsInAlphabet, int uniqueCharListLength, Dictionary<char, int> nameToNumberLookup)
        {
            int[,] incidenceMatrix = new int[uniqueCharListLength,uniqueCharListLength];
            foreach (char[] letterpair in earlierToLaterRelationshipsInAlphabet)
            {
                int letter1 = nameToNumberLookup[letterpair[0]];
                int letter2 = nameToNumberLookup[letterpair[1]];
                incidenceMatrix[letter1, letter2] = 1;
                incidenceMatrix[letter2, letter1] = -1;
            }
            return incidenceMatrix;
        }
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            List<char>[] charArrayOfLists = TurnSringInputIntoListOfCharArrays(input);
            List<char> uniqueCharList = SampleEachUniqueCharacter(charArrayOfLists);
            List<char[]> earlierToLaterCharacterRelationships = SampleRelationshipsBetweenCharacters(charArrayOfLists);
            Dictionary<int, char> numberToNameLookup = CreateNumberLetterPairs(uniqueCharList);
            Dictionary<char, int> nameToNumberLookup = CreateReversedNumberLetterPairs(uniqueCharList);
            int[,] relationshipIncidenceMatrix = RelationshipDictionaryToIncidenceMatrix(earlierToLaterCharacterRelationships, uniqueCharList.Count, nameToNumberLookup);
            
            for (int i = 0; i < relationshipIncidenceMatrix.GetLength(0);i++)// ヾ(≧▽≦*)o
            {
                for (int j = 0; j < relationshipIncidenceMatrix.GetLength(1); j++)
                {
                    Console.Write($"|{relationshipIncidenceMatrix[i,j]}");
                }
                Console.WriteLine("|");
            }

            List<int> relationshipsBetweenTheVertices = TopologicalySortVertices(relationshipIncidenceMatrix);
            Console.WriteLine();
            if (relationshipsBetweenTheVertices.Count > 0)
            {
                Console.Write(relationshipsBetweenTheVertices[0]);
                for (int i = 1; i < relationshipsBetweenTheVertices.Count; i++)
                {
                    Console.Write($" --> {relationshipsBetweenTheVertices[i]}");
                }
            }
            else
            {
                Console.WriteLine("Tyto znaky nemají žádné uspořádání; ó, jakýžto chaós!");
            }
        }
    }
}