namespace NexusPay.Api.Infratructures.Observability
{
    public class HttpTraceProcessorService : BackgroundService
    {

        private readonly HttpTraceQueue _httpTraceQueue;
        private readonly IServiceProvider _serviceProvider;

        public HttpTraceProcessorService(IServiceProvider serviceProvider, HttpTraceQueue httpTraceQueue)
        {
            _httpTraceQueue = httpTraceQueue;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            await foreach (var httpTraceRecord in _httpTraceQueue.Reader.ReadAllAsync(stoppingToken))
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var telemetryRepository = scope.ServiceProvider.GetRequiredService<IHttpTraceRepository>();

                    await telemetryRepository.SaveAsync(httpTraceRecord);
                }
            }
        }
    }
}
