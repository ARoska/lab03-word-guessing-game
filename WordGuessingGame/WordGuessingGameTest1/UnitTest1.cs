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
    }
}
