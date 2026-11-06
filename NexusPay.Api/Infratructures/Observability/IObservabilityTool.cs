namespace NexusPay.Api.Infratructures.Observability
{
    public interface IObservabilityTool
    {
        void InitiateLatencyReading();
        void StopLatencyReading();
        long GetLatencyReading();
        void SaveLatencyReading(HttpContext httpContext);
    }
}
