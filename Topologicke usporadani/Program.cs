using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Topologicke_usporadani
{
    internal class Program
    {
        static List<int> TopologicalySortVertices(int[,] inputMatrix)
        {
            int NumberOfVerts = inputMatrix.GetLength(0);
            List<int> sortedVertices = new List<int>;
            char[] verticeStatus = new char[NumberOfVerts];//[o]pen,[u]ndiscovered,[c]losed
            foreach (int i in sortedVertices)
                verticeStatus [i] = 'u';
            foreach (int i in sortedVertices)
            {
                if (sortedVertices[i] == 'u')
                    _dfs(i);
            }
            List<int> reversedSortedVertices = new List<int>;
            for  
            void _dfs(int vertex)
            {
                verticeStatus[vertex] = 'o';
                for (int i = 0; i < NumberOfVerts;i++)
                {
                    if (inputMatrix[vertex,i] == -1)
                    {
                        if (verticeStatus[i] == 'o')
                            throw new Exception("Graf obsahuje cyklus, nelze seřadit!");
                        else if (verticeStatus[i] == 'u')
                            _dfs(i);
                    }
                    verticeStatus[vertex] = 'c';
                    sortedVertices.Add(i);
                }
            }
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
                    foreach (char sample in samples)
                    {
                        if (character == sample )
                        {
                            break;
                        }
                        samples.Add(sample);
                    }
                }
            }
            return samples;
        }
        static List<char[]> SampleRelationshipsBetweenCharacters(List<char>[] charArrayOfLists)
        {
            List<char[]> earlierToLaterRelationshipsInAlphabet = new List<char[]>();
            for (int i = 0; i < charArrayOfLists.Length - 1; i++)
            {
                int arrayLen = charArrayOfLists[i].Count;
                int secondArrayLen = charArrayOfLists[i + 1].Count;
                if (arrayLen > secondArrayLen)
                {
                    arrayLen = secondArrayLen;
                }
                /*hledáme 2x2 prostor v našem poli listů
                     A|A B| 
                     A|A C|
                     B A D
                    ve kterém můžeme porovnat, která písmena po sobě musí následovat
                */
                for (int j = arrayLen - 1; j > 0; j--)
                {
                    if (charArrayOfLists[i][j - 1] == charArrayOfLists[i + 1][j - 1] && charArrayOfLists[i][j] != charArrayOfLists[i + 1][j])
                    {
                        foreach (char[] realtionship in earlierToLaterRelationshipsInAlphabet)
                        {
                            if (realtionship[0] == charArrayOfLists[i][j] && realtionship[1] == charArrayOfLists[i - 1][j])
                            {
                                break;
                            }
                            earlierToLaterRelationshipsInAlphabet.Add([Convert.ToChar(charArrayOfLists[i][j]), Convert.ToChar(charArrayOfLists[i - 1][j])]);
                        }
                    }
                }
                if (charArrayOfLists[i][0] != charArrayOfLists[i + 1][0])//pro případ, kdy porovnáváme první písmeno v řádku
                {
                    foreach (char[] realtionship in earlierToLaterRelationshipsInAlphabet)
                    {
                        if (realtionship[0] == charArrayOfLists[i][0] && realtionship[1] == charArrayOfLists[i - 1][0])
                        {
                            break;
                        }
                        earlierToLaterRelationshipsInAlphabet.Add([Convert.ToChar(charArrayOfLists[i][0]), Convert.ToChar(charArrayOfLists[i - 1][0])]);
                    }
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
        }
    }
}