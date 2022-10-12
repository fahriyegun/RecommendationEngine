using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StreamReaderApp.Consumer;
using System.Text;
using System.Text.Json;

Console.WriteLine("Waiting...");

var factory = new ConnectionFactory()
{
    HostName = CommonConstants.HostName,
    Port = CommonConstants.Port,
    UserName = CommonConstants.UserName,
    Password = CommonConstants.Password,
    VirtualHost = "/",
    ContinuationTimeout = new TimeSpan(10, 0, 0, 0)
};

using var connection = factory.CreateConnection();
var channel = connection.CreateModel();
channel.QueueDeclare(CommonConstants.QueueName, true, false, false);
channel.BasicQos(0, 1, true);
var consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(CommonConstants.QueueName, false, consumer);
consumer.Received += Consumer_Received;

Console.ReadLine();



void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    try
    {
        ProductView productView = JsonSerializer.Deserialize<ProductView>(message);

        AddProductView(message);
        Console.WriteLine($"message: {productView.messageid}");

        channel.BasicAck(e.DeliveryTag, false);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Occured error in {message}. Error Detail:  {ex.Message}");
    }
}

void AddProductView(string productView)
{
    Post("Historys", productView);
}

void Post(string requestUri, string body)
{
    var data = new StringContent(body, Encoding.UTF8, "application/json");

    var url = $"https://localhost:44365/{requestUri}";
    using var client = new HttpClient();

    var response = client.PostAsync(url, data).GetAwaiter().GetResult();

    try
    {
        string result = response.Content.ReadAsStringAsync().Result;
        Console.WriteLine(body + " is added.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("error" + ex.Message);
    }

}

