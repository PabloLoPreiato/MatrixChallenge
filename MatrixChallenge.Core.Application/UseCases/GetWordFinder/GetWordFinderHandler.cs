using MatrixChallenge.Domain.Factories;
using CSharpFunctionalExtensions;
using MatrixChallenge.Application.Error;
using MatrixChallenge.Domain.Extensions;
using MatrixChallenge.Domain.Configuration;

namespace MatrixChallenge.Application.UseCases.GetWordFinder
{
    public class GetWordFinderHandler
    {
        private readonly IWordFinderFactory _wordFinderfactory;
        private readonly IMatrixSettings _matrixSettings;

        public GetWordFinderHandler(IWordFinderFactory wordFinderfactory, IMatrixSettings matrixSettings)
        {
            _wordFinderfactory = wordFinderfactory;
            _matrixSettings = matrixSettings;
        }

        public Result<GetWordFinderResponse, ResultError> Handle(GetWordFinderQuery request)
        {
            if (request.Matrix.IsNullOrEmptyOrWhiteSpace())
                return Result.Failure<GetWordFinderResponse, ResultError>(new ResultError("The matrix stream cannot be empty"));

            if (request.WordStream.IsNullOrEmptyOrWhiteSpace())
                return Result.Failure<GetWordFinderResponse, ResultError>(new ResultError("The word stream cannot be empty"));
            
            var matrixStream = (IEnumerable<string>) request.Matrix.Split(",");

            if(matrixStream.Count() > _matrixSettings.MaxRows || matrixStream.First().Length > _matrixSettings.MaxColumns)
                return Result.Failure<GetWordFinderResponse, ResultError>(new ResultError($"The matrix size cannot exceed {_matrixSettings.MaxRows}x{_matrixSettings.MaxColumns}"));

            var orderedMatrixStream = matrixStream.OrderByDescending(w => w.Length);

            if (orderedMatrixStream.First().Length != orderedMatrixStream.Last().Length)
                return Result.Failure<GetWordFinderResponse, ResultError>(new ResultError("All words should be of the same lenght"));

            var wordStream = (IEnumerable<string>)request.WordStream.Split(",");

            var wordFinderService = _wordFinderfactory.CreateService(matrixStream);

            return new GetWordFinderResponse() { topFoundWords = wordFinderService.Find(wordStream) };
        }
    }
}
