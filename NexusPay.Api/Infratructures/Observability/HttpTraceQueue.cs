using System.Threading.Channels;

namespace NexusPay.Api.Infratructures.Observability
{
    public class HttpTraceQueue
    {
        private readonly Channel<HttpTraceRecord> _queue = Channel.CreateUnbounded<HttpTraceRecord>();

        public ChannelWriter<HttpTraceRecord> Writer => _queue.Writer;
        public ChannelReader<HttpTraceRecord> Reader => _queue.Reader;
    }
}
