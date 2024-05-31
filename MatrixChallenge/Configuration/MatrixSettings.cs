using MatrixChallenge.Domain.Configuration;

namespace MatrixChallenge.Host.Configuration
{
    public class MatrixSettings : IMatrixSettings
    {
        public int MaxRows { get; private set; }

        public int MaxColumns { get; private set; }

        private readonly IConfiguration _configuration;

        public MatrixSettings(IConfiguration configuration)
        {
            _configuration = configuration;
            MaxRows = _configuration.GetValue<int>("MatrixSettings:MaxRows");
            MaxColumns = _configuration.GetValue<int>("MatrixSettings:MaxColumns");
        }
    }
}
