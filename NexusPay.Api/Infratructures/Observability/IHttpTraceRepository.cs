namespace NexusPay.Api.Infratructures.Observability
{
    public interface IHttpTraceRepository
    {
        Task SaveAsync(HttpTraceRecord record);
        Task<IEnumerable<HttpTraceRecord>> GetAllAsync(int page, int length);
    }
}
