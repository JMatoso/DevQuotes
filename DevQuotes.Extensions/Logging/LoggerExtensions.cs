using Microsoft.Extensions.Logging;

namespace DevQuotes.Extensions.Logging;

public static class LoggerExtensions
{
    public static void LogHttpResponse(this ILogger logger, HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            logger.LogDebug("Received a success response from {Url}", response.RequestMessage.RequestUri);
            return;
        }

        logger.LogWarning("Received a non-success status code {StatusCode} from {Url}",
                (int)response.StatusCode, response.RequestMessage.RequestUri);
    }
}
