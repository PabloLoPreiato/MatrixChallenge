using Xunit;
using FluentAssertions;
using Moq;
using MatrixChallenge.Domain.Factories;
using MatrixChallenge.Application.UseCases.GetWordFinder;
using MatrixChallenge.Domain.Services;
using MatrixChallenge.Application.Error;
using MatrixChallenge.Domain.Configuration;

namespace MatrixChallenge.Tests.ApplicationTests.UseCases.Queries
{
    public class GetWordFinderHandlerTests
    {
        [Fact]
        public void Handle_WhenCalledWithEmptyMatrixstream_ReturnValidationMessage()
        {
            //Arrange
            var mockFactory = new Mock<IWordFinderFactory>();
            var mockSettings = new Mock<IMatrixSettings>();
            var mockService = new Mock<IWordFinderService>();
            var query = new GetWordFinderQuery()
            {
                Matrix = "",
                WordStream = ""
            };

            GetWordFinderHandler handler = new GetWordFinderHandler(mockFactory.Object, mockSettings.Object);

            //Act

            var result = handler.Handle(query);

            //Assert
            Assert.True(result.IsFailure);
            result.Error.Should().BeEquivalentTo(new ResultError("The matrix stream cannot be empty"));
        }

        [Fact]
        public void Handle_WhenCalledWithEmptyWordStream_ReturnValidationMessage()
        {
            //Arrange
            var mockFactory = new Mock<IWordFinderFactory>();
            var mockSettings = new Mock<IMatrixSettings>();
            var mockService = new Mock<IWordFinderService>();
            var query = new GetWordFinderQuery()
            {
                Matrix = "abcd,efgh",
                WordStream = ""
            };

            GetWordFinderHandler handler = new GetWordFinderHandler(mockFactory.Object, mockSettings.Object);

            //Act

            var result = handler.Handle(query);

            //Assert
            Assert.True(result.IsFailure);
            result.Error.Should().BeEquivalentTo(new ResultError("The word stream cannot be empty"));
        }

        [Fact]
        public void Handle_WhenCalledWithMatrixStreamThatExceedTheMaxRowsSizeAllowed_ReturnValidationMessage()
        {
            //Arrange
            var mockFactory = new Mock<IWordFinderFactory>();
            var mockSettings = new Mock<IMatrixSettings>();
            var mockService = new Mock<IWordFinderService>();
            var query = new GetWordFinderQuery()
            {
                Matrix = "abc,def,ghi",
                WordStream = "ab"
            };

            mockSettings.Setup(p => p.MaxRows).Returns(2);
            mockSettings.Setup(p => p.MaxColumns).Returns(3);

            GetWordFinderHandler handler = new GetWordFinderHandler(mockFactory.Object, mockSettings.Object);

            //Act

            var result = handler.Handle(query);

            //Assert
            Assert.True(result.IsFailure);
            result.Error.Should().BeEquivalentTo(new ResultError("The matrix size cannot exceed 2x3"));
        }

        [Fact]
        public void Handle_WhenCalledWithMatrixStreamThatExceedTheMaxColumnsSizeAllowed_ReturnValidationMessage()
        {
            //Arrange
            var mockFactory = new Mock<IWordFinderFactory>();
            var mockSettings = new Mock<IMatrixSettings>();
            var mockService = new Mock<IWordFinderService>();
            var query = new GetWordFinderQuery()
            {
                Matrix = "abc,def,ghi",
                WordStream = "ab"
            };

            mockSettings.Setup(p => p.MaxRows).Returns(3);
            mockSettings.Setup(p => p.MaxColumns).Returns(2);

            GetWordFinderHandler handler = new GetWordFinderHandler(mockFactory.Object, mockSettings.Object);

            //Act

            var result = handler.Handle(query);

            //Assert
            Assert.True(result.IsFailure);
            result.Error.Should().BeEquivalentTo(new ResultError("The matrix size cannot exceed 3x2"));
        }

        [Fact]
        public void Handle_WhenCalledWithMatrixStreamThatContainsWordsOfDifferentLenght_ReturnValidationMessage()
        {
            //Arrange
            var mockFactory = new Mock<IWordFinderFactory>();
            var mockSettings = new Mock<IMatrixSettings>();
            var mockService = new Mock<IWordFinderService>();
            var query = new GetWordFinderQuery()
            {
                Matrix = "abc,df,ghi",
                WordStream = "ab"
            };

            mockSettings.Setup(p => p.MaxRows).Returns(64);
            mockSettings.Setup(p => p.MaxColumns).Returns(64);

            GetWordFinderHandler handler = new GetWordFinderHandler(mockFactory.Object, mockSettings.Object);

            //Act

            var result = handler.Handle(query);

            //Assert
            Assert.True(result.IsFailure);
            result.Error.Should().BeEquivalentTo(new ResultError("All words should be of the same lenght"));
        }

        [Fact]
        public void Handle_WhenCalledWithValidMatrixStreamAndValidWordStream_FindWords()
        {
            //Arrange
            var mockFactory = new Mock<IWordFinderFactory>();
            var mockSettings = new Mock<IMatrixSettings>();
            var mockService = new Mock<IWordFinderService>();
            var query = new GetWordFinderQuery()
            {
                Matrix = "abcdc,fgwio,chill,pqnsd,uvdxy",
                WordStream = "cold,wind,snow,chill"
            };

            mockSettings.Setup(p => p.MaxRows).Returns(64);
            mockSettings.Setup(p => p.MaxColumns).Returns(64);

            IEnumerable<string> matrixEnumerable = new List<string> { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
            IEnumerable<string> foundWords = new List<string> { "cold", "wind", "chill" };
            mockService.Setup(m => m.Find(It.IsAny<IEnumerable<string>>())).Returns(foundWords);
            mockFactory.Setup(m => m.CreateService(It.IsAny<IEnumerable<string>>())).Returns(mockService.Object);

            GetWordFinderHandler handler = new GetWordFinderHandler(mockFactory.Object, mockSettings.Object);

            //Act

            var result = handler.Handle(query);

            //Assert
            Assert.True(result.IsSuccess);
            result.Value.Should().BeEquivalentTo(new GetWordFinderResponse() { topFoundWords = foundWords });
            mockFactory.Verify(m => m.CreateService(It.IsAny<IEnumerable<string>>()), Times.Once);
            mockService.Verify(m => m.Find(It.IsAny<IEnumerable<string>>()), Times.Once);
        }
    }
}

