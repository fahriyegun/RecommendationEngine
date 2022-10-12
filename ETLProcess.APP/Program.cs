using ETLProcess.APP.Models;
using Newtonsoft.Json;
using System.Text;

Initializer.Build();


using (var context = new datadbContext())
{
    ETLProductsToDb(context.Products.ToList());
    Console.WriteLine("\n---------------------------------------------------\n");
    ETLOrdersToDb(context.Orders.ToList());
    Console.WriteLine("\n---------------------------------------------------\n");
    ETLOrderItemsToDb(context.OrderItems.ToList());
}

Console.ReadLine();


void ETLProductsToDb(List<Product> products)
{
    Console.WriteLine("Started ETL for Products....");

    products.ForEach(product =>
    {
        try
        {
            Console.WriteLine(product.ProductId);
            AddProducts(JsonConvert.SerializeObject(product));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error has been received while doing Product ETL.Exception Detail: {ex.Message}");
        }

    });

    Console.WriteLine("Finished ETL for Products...");

}



void ETLOrdersToDb(List<Order> orders)
{
    Console.WriteLine("Started ETL for Orders....");

    try
    {
        orders.ForEach(order =>
        {
            try
            {
                Console.WriteLine(order.OrderId);
                AddOrders(JsonConvert.SerializeObject(order));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error has been received while doing Product ETL.Exception Detail: {ex.Message}");
            }

        });

    }
    catch (Exception ex)
    {

        Console.WriteLine($"An error has been received while doing Order ETL.Exception Detail: {ex.Message}");
    }

    Console.WriteLine("Finished ETL for Orders...");

}


void ETLOrderItemsToDb(List<OrderItem> orderItems)
{
    Console.WriteLine("Started ETL for OrderItems....");

    try
    {
        orderItems.ForEach(orderItem =>
        {
            try
            {
                Console.WriteLine(orderItem.OrderId);
                AddOrderItems(JsonConvert.SerializeObject(orderItem));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error has been received while doing Product ETL.Exception Detail: {ex.Message}");
                Console.WriteLine(JsonConvert.SerializeObject(orderItem));
            }

        });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error has been received while doing OrderItem ETL.Exception Detail: {ex.Message}");
    }


    Console.WriteLine("Finished ETL for OrderItems...");

}

void AddProducts(string products)
{
    Post("Products", products);
}

void AddOrders(string orders)
{
    Post("Orders", orders);
}

void AddOrderItems(string orderItems)
{
    Post("OrderItems", orderItems);
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
