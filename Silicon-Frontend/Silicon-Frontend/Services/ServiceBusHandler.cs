using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace Silicon_Frontend.Services;

public class ServiceBusHandler
{
    private readonly string _connectionString;
    private readonly string _queueName;

    public ServiceBusHandler(string connectionString, string queueName)
    {
        _connectionString = connectionString;
        _queueName = queueName;
    }

    public async Task SendMessageAsync(object messageObject)
    {
        var messageBody = JsonSerializer.Serialize(messageObject);

        try
        {
            await using var client = new ServiceBusClient(_connectionString);
            ServiceBusSender sender = client.CreateSender(_queueName);
            ServiceBusMessage message = new ServiceBusMessage(messageBody);
            await sender.SendMessageAsync(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending message: {ex.Message}");
        }
    }
}