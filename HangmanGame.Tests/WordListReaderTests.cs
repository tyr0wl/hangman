using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace HangmanGame.Tests
{
    [TestFixture]
    public class WordListReaderTests
    {
        [SetUp]
        public void SetUp()
        {
            _expectedWordList = new List<string>
            {
                "Hallo",
                "dies",
                "ist",
                "eine",
                "Wortliste!"
            };

            var rawWordListString = string.Empty;
            _expectedWordList.ForEach(word => rawWordListString += word + Environment.NewLine);

            var rawBytesOfWordList = GetRawBytesOfString(rawWordListString);

            _memoryStream = new MemoryStream(rawBytesOfWordList);
        }

        private List<string> _expectedWordList;
        private MemoryStream _memoryStream;

        private static byte[] GetRawBytesOfString(string rawWordListString)
        {
            var utf8Encoding = new UTF8Encoding();
            return utf8Encoding.GetBytes(rawWordListString);
        }

        [Test]
        public void Should_get_wordList_with_expected_number_of_words()
        {
            // Arrange
            var sut = new WordListReader();

            // Act
            var actualWordList = sut.GetWordList(_memoryStream);

            // Assert
            Assert.AreEqual(_expectedWordList.Count, actualWordList.Count);
        }

        [Test]
        public void Should_get_wordList_with_expected_words()
        {
            // Arrange
            var sut = new WordListReader();

            // Act
            var actualWordList = sut.GetWordList(_memoryStream);

            // Assert
            Assert.AreEqual(_expectedWordList, actualWordList);
        }
    }
}