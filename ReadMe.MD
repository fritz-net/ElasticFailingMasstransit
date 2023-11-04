

# Caused by 
`cfg.UseDelayedRedelivery(` in combination with creating a new Transaction in consumer

# Exception
```
fail: MassTransit.ReceiveTransport[0]
      T-FAULT rabbitmq://localhost/Dummy a4660000-5d0a-0015-47d4-08dbdd409ed2
      System.ArgumentException: An item with the same key has already been added. Key: messaging.message.conversation_id
         at System.Collections.Generic.Dictionary`2.TryInsert(TKey key, TValue value, InsertionBehavior behavior)
         at System.Collections.Generic.Dictionary`2.Add(TKey key, TValue value)
         at Elastic.Apm.Model.Span.CheckAndCaptureBaggage()
         at Elastic.Apm.Model.Span..ctor(String name, String type, String parentId, String traceId, Transaction enclosingTransaction, IPayloadSender payloadSender, IApmLogger logger, ICurrentExecutionSegmentsContainer currentExecutionSegmentsContainer, IApmServerInfo apmServerInfo, Span parentSpan, InstrumentationFlag instrumentationFlag, Boolean captureStackTraceOnStart, Nullable`1 timestamp, Boolean isExitSpan, String id, IEnumerable`1 links, Activity current)
         at Elastic.Apm.Model.Transaction.StartSpanInternal(String name, String type, String subType, String action, InstrumentationFlag instrumentationFlag, Boolean captureStackTraceOnStart, Nullable`1 timestamp, String id, Boolean isExitSpan, IEnumerable`1 links, Activity current)
         at Elastic.Apm.OpenTelemetry.ElasticActivityListener.CreateSpanForActivity(Activity activity, Int64 timestamp, List`1 spanLinks)
         at Elastic.Apm.OpenTelemetry.ElasticActivityListener.<get_ActivityStarted>b__17_0(Activity activity)
         at System.Diagnostics.SynchronizedList`1.EnumWithAction(Action`2 action, Object arg)
         at System.Diagnostics.Activity.Start()
         at MassTransit.Logging.LogContextActivityExtensions.PopulateSendActivity[T](SendContext context, Activity activity, ValueTuple`2[] tags) in /_/src/MassTransit/Logging/Diagnostics/LogContextActivityExtensions.cs:line 205
         at MassTransit.Logging.LogContextActivityExtensions.StartSendActivity[T](ILogContext logContext, SendTransportContext transportContext, SendContext`1 context, ValueTuple`2[] tags) in /_/src/MassTransit/Logging/Diagnostics/LogContextActivityExtensions.cs:line 32
         at MassTransit.Transports.SendTransport`1.SendPipe`1.Send(TContext context) in /_/src/MassTransit/Transports/SendTransport.cs:line 86
         at MassTransit.Agents.PipeContextSupervisor`1.Send(IPipe`1 pipe, CancellationToken cancellationToken) in /_/src/MassTransit/Agents/PipeContextSupervisor.cs:line 55
         at MassTransit.Agents.PipeContextSupervisor`1.Send(IPipe`1 pipe, CancellationToken cancellationToken) in /_/src/MassTransit/Agents/PipeContextSupervisor.cs:line 61
         at MassTransit.Agents.PipeContextSupervisor`1.Send(IPipe`1 pipe, CancellationToken cancellationToken) in /_/src/MassTransit/Agents/PipeContextSupervisor.cs:line 67
         at MassTransit.Transports.HostConfigurationRetryExtensions.Retry(IHostConfiguration hostConfiguration, Func`1 factory, CancellationToken cancellationToken, CancellationToken stoppingToken) in /_/src/MassTransit/Transports/HostConfigurationRetryExtensions.cs:line 39
         at MassTransit.Middleware.GenerateFaultFilter.GenerateFault(ExceptionReceiveContext context) in /_/src/MassTransit/Middleware/GenerateFaultFilter.cs:line 57
         at MassTransit.Middleware.GenerateFaultFilter.MassTransit.IFilter<MassTransit.ExceptionReceiveContext>.Send(ExceptionReceiveContext context, IPipe`1 next) in /_/src/MassTransit/Middleware/GenerateFaultFilter.cs:line 25
         at MassTransit.Middleware.RescueFilter`2.MassTransit.IFilter<TContext>.Send(TContext context, IPipe`1 next) in /_/src/MassTransit/Middleware/RescueFilter.cs:line 61
         at MassTransit.Middleware.DeadLetterFilter.MassTransit.IFilter<MassTransit.ReceiveContext>.Send(ReceiveContext context, IPipe`1 next) in /_/src/MassTransit/Middleware/DeadLetterFilter.cs:line 32
         at MassTransit.Transports.ReceivePipeDispatcher.Dispatch(ReceiveContext context, ReceiveLockContext receiveLock) in /_/src/MassTransit/Transports/ReceivePipeDispatcher.cs:line 65
         at MassTransit.Transports.ReceivePipeDispatcher.Dispatch(ReceiveContext context, ReceiveLockContext receiveLock) in /_/src/MassTransit/Transports/ReceivePipeDispatcher.cs:line 108
         at MassTransit.Transports.ReceivePipeDispatcher.Dispatch(ReceiveContext context, ReceiveLockContext receiveLock) in /_/src/MassTransit/Transports/ReceivePipeDispatcher.cs:line 115
         at MassTransit.RabbitMqTransport.RabbitMqBasicConsumer.<>c__DisplayClass21_0.<<HandleBasicDeliver>b__0>d.MoveNext() in /_/src/Transports/MassTransit.RabbitMqTransport/RabbitMqTransport/RabbitMqBasicConsumer.cs:line 168
```
