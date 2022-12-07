using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace Restauracja.MessageBus;

public interface IMessageBus
{
    Task PublishMessage<T>(T message, string topicName) where T : BaseMessage;
}

public class AzureServiceBusMessageBus : IMessageBus
{
    private string connectionString = "Endpoint=sb://comarchrestaurant.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=7xRrP5r4dGbkpv2rMku9Atekkl3YOsJEVsIWNb93xUI=";

    public async Task PublishMessage<T>(T message, string topicName) where T : BaseMessage
    {
        await using var client = new ServiceBusClient(connectionString);
        ServiceBusSender sender = client.CreateSender(topicName);

        string jsonMessage = JsonSerializer.Serialize(message);
        ServiceBusMessage finalMessage = new ServiceBusMessage(jsonMessage)
        {
            CorrelationId = Guid.NewGuid().ToString(),
        };

        await sender.SendMessageAsync(finalMessage);
        await client.DisposeAsync();
    }
}