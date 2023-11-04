using Elastic.Apm;
using Elastic.Apm.Api;
using MassTransit;

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
        var transaction = Agent.Tracer.CurrentTransaction ?? CreateTransaction(context, "ConsumeDummyEvent");
        var span = transaction?.StartSpan("ConsumeDummyEvent", ApiConstants.TypeMessaging);

        _logger.LogInformation("DummyConsumer.Consume");
        throw new NotImplementedException();

        //span?.End();
        //transaction?.End();
    }
}