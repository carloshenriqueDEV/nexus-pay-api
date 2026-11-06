using Microsoft.AspNetCore.Mvc;
using NexusPay.Api.Infratructures.Observability;

namespace NexusPay.Api.Controllers
{
    [ApiController]
    [Route("api/v1/diagnostics")]
    public class DiagnosticsController :ControllerBase
    {
        private readonly IHttpTraceRepository _httpTraceRepository;

        public DiagnosticsController(IHttpTraceRepository httpTraceRepository)
        {
            _httpTraceRepository = httpTraceRepository;
        }
        [HttpGet("http-traces")]
        public async Task<IActionResult> GetHttpTraces(int page = 1, int length = 1000)
        {
            var traces = await _httpTraceRepository.GetAllAsync(page, length);
            return Ok(traces);
        }
    }
}
