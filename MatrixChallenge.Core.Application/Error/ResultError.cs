namespace MatrixChallenge.Application.Error
{
    public class ResultError
    {
        public string Message { get; }

        public ResultError(string message)
        {
            Message = message;
        }
    }
}
