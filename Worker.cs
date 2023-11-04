using Elastic.Apm.Api;
using System.ComponentModel.Design;
using System;
using MassTransit;
using PostsWorkerService.Notifications;

namespace ElasticFailingMasstransit
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public Worker(ILogger<Worker> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _publishEndpoint.Publish(new DummyEvent());

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}