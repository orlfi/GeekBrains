using Microsoft.Extensions.Configuration;

namespace Timesheets.Infrastructure.Validation
{
    public class ErrorCodes : IErrorCodes
    {
        private readonly IConfiguration _configuration;

        public ErrorCodes(IConfiguration configuration) => _configuration = configuration;

        public string GetMessage(string key)
        {
            var result = _configuration.GetSection("ErrorCodes")[key];
            return result;
        }
    }
}