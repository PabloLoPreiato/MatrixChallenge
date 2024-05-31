namespace MatrixChallenge.Domain.Services
{
    public interface IWordFinderService
    {
        IEnumerable<string> Find(IEnumerable<string> wordStream);
    }
}
