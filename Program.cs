using ElasticFailingMasstransit;
using MassTransit;
using PostsWorkerService.Notifications;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        IConfiguration Configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<DummyConsumer>();


            x.UsingRabbitMq((context, cfg) =>
            {
                var options = Configuration.GetSection(RabbitMqSettings.KeyName).Get<RabbitMqSettings>() ?? new RabbitMqSettings();
                cfg.Host(options.Server, options.VirtualHost, h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });

                // https://masstransit.io/documentation/concepts/exceptions
                //cfg.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30))); // unknown exchange type 'x-delayed-message'
                cfg.UseDelayedRedelivery(r =>
                {
                    r.ReplaceMessageId = true;
                    //r.Handle<Exception>(t =>
                    //{
                    //    t.
                    //});
                    r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30));
                }); // unknown exchange type 'x-delayed-message'
                //cfg.UseMessageRetry(r => r.Immediate(2));
                cfg.UseMessageRetry(r => r.None());

                cfg.ConfigureEndpoints(context);
            });
        });
    })
    .Build();

var scope = host.Services.CreateScope();
var publishEndpoint = scope.ServiceProvider.GetService(typeof(IPublishEndpoint)) as IPublishEndpoint;
publishEndpoint.Publish(new DummyEvent());

host.Run();
