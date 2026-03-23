using Microsoft.VisualStudio.TestPlatform.TestHost;
using MergeSort;

namespace MergeSort.Tests
{
    public class UnitTest1
    {

        [Fact]  // Tím označujeme, že jde o testovací metodu      

        public void Merge_EqualLengthArrays_ReturnsMergedSortedArray()         // Naming convention pro testy: ClassName_FunctionName_ExpectedResult nebo FunctionName_TestSpecifics_ExpectedResult
        {
            // Arrange - nastavme vše co potřebujeme, aby mohla běžet testovaná funkce
            int[] array = { 1, 3, 5, 2, 3, 6 };
            int[] expectedArray = { 1, 2, 3, 3, 5, 6 };
            int left = 0;
            int right = array.Length-1;
            int middle = left + (right - left) / 2;

            // Act - zavoláme testovanou funkci
            MergeSortClass.Merge(array, left, middle, right);

            // Assert - zkontrolujeme to, co nám funkce vrátila
            Assert.Equal(expectedArray, array);
        }
        [Fact]   
        public void Merge_NegativeNumberArrays_ReturnsMergedSortedArray()         
        {
            int[] array = { -5, -3, -1, -6, -3, -2 };
            int[] expectedArray = { -6, -5, -3, -3, -2, -1 };
            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;
            MergeSortClass.Merge(array, left, middle, right);
            Assert.Equal(expectedArray, array);
        }
        [Fact]
        public void Merge_WholeLottaNothin_ReturnsMergedSortedArray()
        {
            int[] array = { 0, 0, 1, 0, 0, 0 };
            int[] expectedArray = { 0, 0, 0, 0, 0, 1 };
            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;
            MergeSortClass.Merge(array, left, middle, right);
            Assert.Equal(expectedArray, array);
        }
        [Fact]
        public void Merge_AlreadySorted_ReturnsMergedSortedArray()         
        {
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 };
            int[] expectedArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 };
            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;
            MergeSortClass.Merge(array, left, middle, right);
            Assert.Equal(expectedArray, array);
        }
        [Fact]
        public void Merge_FibonnachiNumbers_ReturnsMergedSortedArray()         
        {
            int[] array = { 0, 1, 2, 5, 13, 34, 89, 1, 3, 8, 21, 55, 144 };
            int[] expectedArray = { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 };
            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;
            MergeSortClass.Merge(array, left, middle, right);
            Assert.Equal(expectedArray, array);
        }
        [Fact]
        public void MergeSort_FibboFunny_ReturnsMergedSortedArray()
        {
            int[] array = { 0, 1, 2, 13, 5, 34, 89, 1, 3, 8, 21, 55, 144 };
            int[] expectedArray = { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 };
            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;
            MergeSortClass.MergeSort(array, left, right);
            Assert.Equal(expectedArray, array);
        }
        [Fact]
        public void MergeSort_2Merge2Sort_ReturnsMergedSortedArray()
        {
            int[] array = { 2 };
            int[] expectedArray = { 2 };
            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;
            MergeSortClass.MergeSort(array, left, right);
            Assert.Equal(expectedArray, array);
        }
        [Fact]
        public void MergeSort_EpsteinIslandLatCoordinatePartsEplus7_ReturnsMergedSortedArray()
        {
            int[] array = { 83000, 2900000, 180000000, 560, 9 };
            int[] expectedArray = { 9, 560, 83000, 2900000, 180000000 };
            int left = 0;
            int right = array.Length - 1;
            int middle = left + (right - left) / 2;
            MergeSortClass.MergeSort(array, left, right);
            Assert.Equal(expectedArray, array);
        }
    }
}
