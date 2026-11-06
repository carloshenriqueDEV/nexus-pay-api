using NexusPay.Api.Infratructures.Observability;

namespace NexusPay.Api.Middlewares
{
    public class RequestTimeLogging
    {
        private readonly RequestDelegate _next;
        public RequestTimeLogging(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IObservabilityTool observabilityTool)
        {
            observabilityTool.InitiateLatencyReading();

            try
            {
                await _next(httpContext);
            }
            finally
            {
                 observabilityTool.SaveLatencyReading(httpContext);
            }
        }
    }
}
