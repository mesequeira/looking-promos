using Polly;
using Polly.Extensions.Http;

namespace LookingPromos.SharedKernel.Infrastructure.Abstractions.Handlers;

public static class ResilienceHandler
{
    /// <summary>
    /// This method creates a retry policy for HttpClient useful to manage transient errors and 429 status code.
    /// </summary>
    /// <returns>A new instance with the configuration of the retry policy.</returns>
    public static IAsyncPolicy<HttpResponseMessage> CreateRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError() // Manage 5xx, 408 and exceptions from HttpClient
            .OrResult(msg => (int)msg.StatusCode == 429) // Manage 429 http status code requests
            .WaitAndRetryAsync(
                3, // Number of retries
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // Exponential time to waiting
                onRetry: (outcome, timespan, retryAttempt, context) =>
                {
                    Console.WriteLine($"Retrying {retryAttempt} after {timespan.TotalSeconds}s due to {outcome.Result?.StatusCode}");
                });
    }
}