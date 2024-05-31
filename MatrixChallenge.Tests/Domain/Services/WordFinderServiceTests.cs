using FluentAssertions;
using MatrixChallenge.Domain.Services;

namespace MatrixChallenge.Tests.Domain.Services
{
    public class WordFinderServiceTests
    {
        [Fact]
        public void Find_WhenCalledWithNotRepeatedWordsAndLessThan10Words_ReturnFoundWords()
        {
            //Arrange
            IEnumerable<string> matrix = new List<string> { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
            IEnumerable<string> wordStream = new List<string> { "cold", "wind", "snow", "chill" };
            IEnumerable<string> foundWords = new List<string> { "cold", "wind", "chill" };

            WordFinderService service = new WordFinderService(matrix);

            //Act
            var response = service.Find(wordStream);

            //Assert
            response.Should().BeEquivalentTo(foundWords);
        }

        [Fact]
        public void Find_WhenCalledWithNotRepeatedWordsButMoreThan10Words_ReturnTenMostRepeatedFoundWords()
        {
            //Arrange
            IEnumerable<string> matrix = new List<string> { "breatheappendspreadl", "applausearchlatencyo", "lovesightingunrulyxv", "angelscienceaerobice", "newstorylineriversis", "cloudyequatorinvitingx", "horizonshufflehorizonx", "animalsappendixsurprisx", "nightmaresstarsyouthfx", "destinylandrainbowxx", "rainbowstaryouthfulx" };
            IEnumerable<string> wordStream = new List<string> { "cold", "wind", "snow", "chill", "breathe", "applause", "loves", "chill", "angel", "new", "cloudy", "horizon", "animals", "nightmares", "destiny", "rainbows", "youthful", "balance", "appendix", "latency", "unruly", "science", "equator", "aerobics", "riverside", "inviting", "surprise", "elevator", "shuffle" };
            IEnumerable<string> foundWords = new List<string> { "loves", "breathe", "applause", "angel", "new", "cloudy", "horizon", "animals", "nightmares", "destiny" };

            WordFinderService service = new WordFinderService(matrix);

            //Act
            var response = service.Find(wordStream);

            //Assert
            response.Should().BeEquivalentTo(foundWords);
        }

        [Fact]
        public void Find_WhenCalledWithRepeatedWordsInTheSameColumn_CountThatWordOnlyOneTimeAndReturnTop10MostRepeatedWords()
        {
            //Arrange
            IEnumerable<string> matrix = new List<string> { "breatheappendspreadlc", "applausearchlatencyoo", "lovesightingunrulyxvl", "angelscienceaerobiced", "newstorylineriversisc", "cloudyequatorinvitingxo", "horizonshufflehorizonxl", "animalsappendixsurprisxd", "nightmaresstarsyouthfxx", "destinylandrainbowxxx", "rainbowstaryouthfulxx" };
            IEnumerable<string> wordStream = new List<string> { "cold", "wind", "snow", "breathe", "applause", "loves", "chill", "angel", "new", "cloudy", "horizon", "animals", "nightmares", "destiny", "rainbows", "youthful", "balance", "appendix", "latency", "unruly", "science", "equator", "aerobics", "riverside", "inviting", "surprise", "elevator", "shuffle", "chill" };
            IEnumerable<string> foundWords = new List<string> { "loves", "cold", "breathe", "applause", "angel", "new", "cloudy", "horizon", "animals", "nightmares" };

            WordFinderService service = new WordFinderService(matrix);

            //Act
            var response = service.Find(wordStream);

            //Assert
            response.Should().BeEquivalentTo(foundWords);
        }

        [Fact]
        public void Find_WhenCalledWithRepeatedWordsInTheSameRow_CountThatWordOnlyOneTimeAndReturnTop10MostRepeatedWords()
        {
            //Arrange
            IEnumerable<string> matrix = new List<string> { "breatheappendspreadlc", "applausearchlatencyoo", "lovesightingunrulyxvl", "angelscienceaerobiced", "newstorylineriversisc", "cloudyequatorinvitingxo", "horizonshufflehorizonxl", "animalsappendixsurprisxd", "nightmaresstarsyouthfxx", "destinylandrainbowxxx", "rainbowstaryouthfulxx", "chillchillchillchillx" };
            IEnumerable<string> wordStream = new List<string> { "cold", "wind", "snow", "breathe", "applause", "loves", "chill", "angel", "new", "cloudy", "horizon", "animals", "nightmares", "destiny", "rainbows", "youthful", "balance", "appendix", "latency", "unruly", "science", "equator", "aerobics", "riverside", "inviting", "surprise", "elevator", "shuffle", "chill" };
            IEnumerable<string> foundWords = new List<string> { "loves", "cold", "breathe", "applause", "chill", "angel", "new", "cloudy", "horizon", "animals" };

            WordFinderService service = new WordFinderService(matrix);

            //Act
            var response = service.Find(wordStream);

            //Assert
            response.Should().BeEquivalentTo(foundWords);
        }

        [Fact]
        public void Find_WhenCalledAndNoWordIsFound_ReturnEmptySetOfStrings()
        {
            //Arrange
            IEnumerable<string> matrix = new List<string> { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
            IEnumerable<string> wordStream = new List<string> { "xcold", "xwind", "xsnow", "xchill" };
            IEnumerable<string> foundWords = new List<string> { };

            WordFinderService service = new WordFinderService(matrix);

            //Act
            var response = service.Find(wordStream);

            //Assert
            response.Should().BeEquivalentTo(foundWords);
        }

    }
}
