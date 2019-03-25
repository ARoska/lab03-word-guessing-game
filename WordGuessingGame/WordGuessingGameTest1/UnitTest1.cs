using System;
using System.IO;
using Xunit;
using static WordGuessingGame.Program;

namespace WordGuessingGameTest1
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("../../../word-list-test.txt", "apple", true)]
        [InlineData("../../../word-list-test.txt", "SeventhHeaven", true)]
        [InlineData("../../../word-list-test.txt", "Nagisa", true)]
        [InlineData("../../../word-list-test.txt", "apple ", false)]
        [InlineData("../../../word-list-test.txt", "Zero-Two", false)]
        [InlineData("../../../word-list-test.txt", "99", false)]
        [InlineData("../../../word-list-test.txt", " ", false)]
        [InlineData("../../../word-list-test.txt", ".gitignore", false)]
        [InlineData("../../../word-list-test.txt", "root/file", false)]
        public void CanAddOneWordToListButCannotIncludeSpacesOrSpecialCharacters(string path, string addWordInput, bool expectedResult)
        {
            bool newWord = AddWord(path, addWordInput);
            DeleteWord(path, addWordInput);

            Assert.Equal(expectedResult, newWord);
        }

        [Theory]
        [InlineData("apple", 'a', new int[] { 0 })]
        [InlineData("apple", 'p', new int[] { 1, 2 })]
        [InlineData("giggling", 'g', new int[] { 0, 2, 3, 7 })]
        [InlineData("pizza", 'z', new int[] { 2, 3 })]
        [InlineData("apple", 'b', new int[0])]
        [InlineData("johnny", 'z', new int[0])]
        [InlineData("style", 'a', new int[0])]
        [InlineData("extraordinary", 'q', new int[0])]
        public void IfTheLetterIsFoundAnywhereInTheWordThenReturnAllIndexesOfThatLetterOtherwiseReturnEmptyArray(string word, char letter, int[] expectedArray)
        {
            int[] answerArray = Guess(word, letter);

            Assert.Equal(expectedArray, answerArray);
        }

        [Fact]
        public void AddingThreeWordsShouldReturnThreeItemArray()
        {
            string path = "../../../word-list-test.txt";
            string firstWord = "apple";
            string secondWord = "dog";
            string thirdWord = "plant";
            AddWord(path, firstWord);
            AddWord(path, secondWord);
            AddWord(path, thirdWord);

            string[] wordList = File.ReadAllLines(path);

            DeleteWord(path, firstWord);
            DeleteWord(path, secondWord);
            DeleteWord(path, thirdWord);

            Assert.Equal(3, wordList.Length);
        }
    }
}
