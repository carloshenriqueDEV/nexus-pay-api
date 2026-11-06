namespace NexusPay.Api.Infratructures.Observability
{
    public record HttpTraceRecord(
    string Endpoint,      
    string RouteTemplate,  
    string HttpMethod,     
    int StatusCode,        
    long LatencyInMs,     
    DateTime Timestamp     
    );
}
