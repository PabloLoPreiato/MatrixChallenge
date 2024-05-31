using MatrixChallenge.Domain.Services;

namespace MatrixChallenge.Domain.Factories
{
    public class WordFinderFactory : IWordFinderFactory
    {
        public IWordFinderService CreateService(IEnumerable<string> matrix)
        {
            return new WordFinderService(matrix);
        }
    }
}
