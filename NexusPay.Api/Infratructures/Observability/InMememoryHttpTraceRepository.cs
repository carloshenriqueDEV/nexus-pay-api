using System.Collections.Concurrent;

namespace NexusPay.Api.Infratructures.Observability
{
    public class InMemoryHttpTraceRepository : IHttpTraceRepository
    {
        private static readonly ConcurrentBag<HttpTraceRecord> _database = new();

        public async Task SaveAsync(HttpTraceRecord record)
        {
            await Task.Delay(10);

            _database.Add(record);
        }
       
        public async Task<IEnumerable<HttpTraceRecord>> GetAllAsync(int page, int length)
        {
            var result = _database.Skip((page - 1) * length).Take(length).ToList();

            return await Task.FromResult(result);
        }
    }
}
