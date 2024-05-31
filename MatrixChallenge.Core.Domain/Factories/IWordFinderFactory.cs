using MatrixChallenge.Domain.Services;

namespace MatrixChallenge.Domain.Factories
{
    public interface IWordFinderFactory
    {
        IWordFinderService CreateService(IEnumerable<string> matrix);
    }
}
