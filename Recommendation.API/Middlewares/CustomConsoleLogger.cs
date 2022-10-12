using Recommendation.API.Interfaces;

namespace Recommendation.API.Middlewares
{
    public class CustomConsoleLogger : ICustomLogger
    {
        public void Write(string message)
        {
            Console.WriteLine("[ Console logger ] - " + message);
        }
    }
}
