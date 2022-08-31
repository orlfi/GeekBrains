using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Net.Http.Utils.Options;

namespace Net.Http.Utils.Logging;

internal class HttpClientMessageHandlerBuilderFilter : IHttpMessageHandlerBuilderFilter
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IOptions<HttpClientLoggingOptions> _options;

    public HttpClientMessageHandlerBuilderFilter(ILoggerFactory loggerFactory,
        IOptions<HttpClientLoggingOptions> options)
    {
        _loggerFactory = loggerFactory;
        _options = options;
    }

    public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
    {
        return (HttpMessageHandlerBuilder builder) =>
        {
            // call next handler 
            next(builder);

            // create logger 
            string loggerName = !string.IsNullOrEmpty(builder.Name) ? builder.Name : "Default";
            ILogger logger = _loggerFactory.CreateLogger($"Net.Http.Utils.HttpClient.{loggerName}");

            LoggingHttpMessageHandler loggingHttpMessageHandler = new LoggingHttpMessageHandler(logger, _options);
            builder.AdditionalHandlers.Insert(0, loggingHttpMessageHandler);
        };
    }

}
