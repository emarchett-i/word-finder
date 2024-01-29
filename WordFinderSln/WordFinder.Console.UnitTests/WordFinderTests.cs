using FluentAssertions;
using System.Text;

namespace WordFinder.Console.UnitTests
{
    public class WordFinderTests
    {
        #region Find Tests

        [Fact]
        public void Find_WithStandardMatrixFromChallenge_ShouldReturnThreeWords()
        {
            //Arrange
            IEnumerable<string> matrix = new List<string> { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy"};
            var wordFinder = new WordFinder(matrix);
            IEnumerable<string> wordStream = new List<string> { "cold", "wind", "snow", "chill" };

            //Act
            IEnumerable<string> result = wordFinder.Find(wordStream);

            //Assert
            result.Should().HaveCount(3);
            result.Should().Contain("cold");
            result.Should().Contain("wind");
            result.Should().Contain("chill");
        }

        [Fact]
        public void Find_WithValidData_ShouldReturnAnEmptyList()
        {
            //Arrange
            IEnumerable<string> matrix = new List<string> { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
            var wordFinder = new WordFinder(matrix);
            IEnumerable<string> wordStream = new List<string> { "xyz" };

            //Act
            IEnumerable<string> result = wordFinder.Find(wordStream);

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void Find_WithValidData_ShouldReturnThreeWords_AndChillShouldAppearFirst_AndOnlyOnce()
        {
            //Arrange
            IEnumerable<string> matrix = new List<string> { "abcdcc", "fgwioh", "chilli", "pqnsdl", "uvdxyl", "chillp" };
            var wordFinder = new WordFinder(matrix);
            IEnumerable<string> wordStream = new List<string> { "cold", "wind", "snow", "chill" };

            //Act
            IEnumerable<string> result = wordFinder.Find(wordStream);

            //Assert
            result.Should().HaveCount(3); //Checks that 'chill' is not duplicated
            result.Should().Contain("cold");
            result.Should().Contain("wind");
            result.Should().Contain("chill");
            result.First().Should().Be("chill");
        }

        [Fact]
        public void Find_WithValidData_ShouldReturnTopTenOnly()
        {
            //Arrange
            IEnumerable<string> matrix = new List<string> 
            { 
                "coldabcdefg", 
                "windabcdefg", 
                "chillabcdef", 
                "snowabcdefg", 
                "freedomabcd",
                "computerabc",
                "laptopabcde",
                "vocabularya",
                "centerabcde",
                "plasticabcd",
                "coffeeabdef"
            };
            var wordFinder = new WordFinder(matrix);

            //there are 11 matches
            IEnumerable<string> wordStream = new List<string> { "cold", "wind", "snow", "chill", "freedom", "computer", "laptop", "vocabulary", "center", "plastic", "coffee" };

            //Act
            IEnumerable<string> result = wordFinder.Find(wordStream);

            //Assert
            result.Should().HaveCount(10);
        }

        #endregion Find Tests

        #region Constructor Tests
        [Fact]
        public void WordFinderConstructor_WithDistinctNumberOfRowsAndColumns_ShouldReturnAnArgumentException()
        {
            //Arrange
            IEnumerable<string> matrix = new List<string> { "abcdcc" };

            //Act & Assert
            Assert.Throws<ArgumentException>(() => new WordFinder(matrix));

        }

        [Fact]
        public void WordFinderConstructor_WithMatrixMaxSize_ShouldWork()
        {
            //Arrange
            IEnumerable<string> matrix = GetMatrix(64);

            //Act
            var wordFinder = new WordFinder(matrix);

            //Assert
            wordFinder.Should().NotBeNull();
        }

        [Fact]
        public void WordFinderConstructor_WithMatrixBiggerThanExpected_ShouldReturnAnArgumentException()
        {
            //Arrange
            IEnumerable<string> matrix = GetMatrix(65);

            //Act & Assert
            Assert.Throws<ArgumentException>(() => new WordFinder(matrix));

        }

        private IEnumerable<string> GetMatrix(int size)
        {
            var list = new List<string>();
            for (int i = 0; i < size; i++)
            {
                var sb = new StringBuilder();
                for (int j = 0; j < size; j++)
                {
                    sb.Append("a");
                }
                list.Add(sb.ToString());
            }

            return list;
        }
        #endregion Constructor Tests
    }
}