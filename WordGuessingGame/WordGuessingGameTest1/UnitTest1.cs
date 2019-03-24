using System;
using System.IO;
using Xunit;
using static WordGuessingGame.Program;

namespace WordGuessingGameTest1
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("../../../wordList.txt", "apple", true)]
        [InlineData("../../../wordList.txt", "tree", true)]
        [InlineData("../../../wordList.txt", "SeventhHeaven", true)]
        [InlineData("../../../wordList.txt", "Nagisa", true)]
        [InlineData("../../../wordList.txt", "apple ", false)]
        [InlineData("../../../wordList.txt", "Zero-Two", false)]
        [InlineData("../../../wordList.txt", "99", false)]
        [InlineData("../../../wordList.txt", " ", false)]
        [InlineData("../../../wordList.txt", ".gitignore", false)]
        [InlineData("../../../wordList.txt", "root/file", false)]
        public void CanAddOneWordToListButCannotIncludeSpacesOrSpecialCharacters(string path, string addWordInput, bool expectedResult)
        {
            bool newWord = AddWord(path, addWordInput);

            Assert.Equal(expectedResult, newWord);
        }

        [Theory]
        [InlineData("apple", 'a', new int[] { 0 })]
        [InlineData("apple", 'p', new int[] { 1, 2 })]
        [InlineData("giggling", 'g', new int[] {0, 2, 3, 7})]
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
    }
}
