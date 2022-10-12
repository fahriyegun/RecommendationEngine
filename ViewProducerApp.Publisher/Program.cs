
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using ViewProducerApp.Publisher;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
var productViews = LoadJson();

productViews.ForEach(productView =>
{
    productView.ViewedDate = DateTime.Now;
    string message = JsonSerializer.Serialize(productView);
    var messageBody = Encoding.UTF8.GetBytes(message);
    Thread.Sleep(1000);

    channel.BasicPublish(string.Empty, CommonConstants.QueueName, null, messageBody);

    Console.WriteLine($"Message is send. - {productView.messageid}");
});


Console.ReadLine();

List<ProductView> LoadJson()
{
    using (StreamReader r = new StreamReader("product-views.json"))
    {
        string json = r.ReadToEnd();
        List<ProductView> items = JsonConvert.DeserializeObject<List<ProductView>>(json);

        return items;
    }
}

