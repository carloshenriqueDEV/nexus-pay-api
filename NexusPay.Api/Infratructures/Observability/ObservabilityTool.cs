using System.Diagnostics;

namespace NexusPay.Api.Infratructures.Observability
{
    public class ObservabilityTool : IObservabilityTool
    {
        private Stopwatch _sw;
        private readonly HttpTraceQueue _traceQueue;

        public ObservabilityTool(HttpTraceQueue traceQueue)
        {
            _traceQueue = traceQueue;
        }

        public long GetLatencyReading()
        {
            ValidateStopwatchState();
            return _sw.ElapsedMilliseconds;
        }

        public void InitiateLatencyReading()
        {
            _sw = Stopwatch.StartNew();
        }

        public void SaveLatencyReading(HttpContext httpContext)
        {
            StopLatencyReading();

            var routeData = httpContext.GetRouteData();
            string routeTemplate = httpContext.GetEndpoint()?.Metadata
                .GetMetadata<RouteEndpoint>()?.RoutePattern.RawText ?? httpContext.Request.Path;

            var httpTraceRecord = new HttpTraceRecord(
                Endpoint: httpContext.Request.Path,
                RouteTemplate: routeTemplate,
                HttpMethod: httpContext.Request.Method,
                StatusCode: httpContext.Response.StatusCode,
                LatencyInMs: _sw.ElapsedMilliseconds,
                Timestamp: DateTime.UtcNow
                );

            _traceQueue.Writer.TryWrite(httpTraceRecord);
        }

        public void StopLatencyReading()
        {
            ValidateStopwatchState();

            _sw.Stop();
        }

        private void ValidateStopwatchState()
        {
            if (_sw == null)
            {
                throw new InvalidOperationException("InitiateLatencyReading not invoked.");
            }
        }

    }
}
