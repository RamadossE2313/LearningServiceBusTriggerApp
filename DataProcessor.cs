using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LearningServiceBusTriggerApp
{
    public class DataProcessor
    {
        private readonly ILogger<DataProcessor> _logger;

        public DataProcessor(ILogger<DataProcessor> logger)
        {
            _logger = logger;
        }

        [Function(nameof(DataProcessor))]
        public async Task Run(
            [ServiceBusTrigger("sample-queue", Connection = "ServiceBusConnectionString")] string queueItem, int deliveryCount, string sessionId)
        {
            var item = JsonSerializer.Deserialize<dynamic>(queueItem);
            _logger.LogInformation("Message processed");
        }
    }
}
