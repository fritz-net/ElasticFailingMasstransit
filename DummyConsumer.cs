using Elastic.Apm;
using Elastic.Apm.Api;
using MassTransit;
using System.Diagnostics;

namespace PostsWorkerService.Notifications;

public class DummyConsumer : IConsumer<DummyEvent>
{
    private readonly ILogger<DummyConsumer> _logger;

    public DummyConsumer(ILogger<DummyConsumer> logger)
    {
        _logger = logger;
    }

    // to show properly in kibana we need to create spans, else it is only shown in background service worker not in API service
    internal static ITransaction CreateTransaction<T>(ConsumeContext<T> context, string name, string type = "unknown") where T : class
    {
        var redeliveryCount = context.GetHeader(MessageHeaders.RedeliveryCount);
        if (redeliveryCount != null && redeliveryCount != "0") // workaround to fix issue with masstransit redelivery
        {
            return null;
        }
        if (Activity.Current != null /* || Activity.Current?.Baggage?.Any()*/)
        {
            //return null;
            var baggage = Activity.Current?.Baggage.ToList();
        }

        var distributedContext = context.GetHeader("x-distributed-tracing-context");

        var distributedTracingData = DistributedTracingData.TryDeserializeFromString(distributedContext);
        
        var transaction = Agent.Tracer.StartTransaction(
            name,
            type,
            distributedTracingData
        );
        return transaction;
    }

    public async Task Consume(ConsumeContext<DummyEvent> context)
    {
        //var transaction = Agent.Tracer.CurrentTransaction ?? CreateTransaction(context, "ConsumeDummyEvent");
        var transaction = CreateTransaction(context, "ConsumeDummyEvent");
        var span = transaction?.StartSpan("ConsumeDummyEvent", ApiConstants.TypeMessaging);

        _logger.LogInformation("DummyConsumer.Consume");
        throw new NotImplementedException();

        span?.End();
        transaction?.End();
    }
}