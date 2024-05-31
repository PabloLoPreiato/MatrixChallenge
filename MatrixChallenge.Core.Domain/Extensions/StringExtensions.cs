namespace MatrixChallenge.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string? value) => 
            string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
    }
}
