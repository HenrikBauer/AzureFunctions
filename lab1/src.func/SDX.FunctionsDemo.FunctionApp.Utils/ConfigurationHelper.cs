using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;

namespace SDX.FunctionsDemo.FunctionApp.Utils
{
    public static class ConfigurationHelper
    {
        static readonly object _lock = new object();
        static IConfiguration _configuration;

        public static IConfiguration GetConfiguration(this ExecutionContext context)
        {
            lock (_lock)
            {
                if (_configuration == null)
                {
                    _configuration = new ConfigurationBuilder()
                       .SetBasePath(context.FunctionAppDirectory)
                       .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                       .AddEnvironmentVariables()
                       .Build();
                }

                return _configuration;
            }
        }
    }
}
