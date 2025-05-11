using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
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
            [ServiceBusTrigger("sample-queue", Connection = "ServiceBusConnectionString")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
