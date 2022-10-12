using Microsoft.Extensions.Configuration;

namespace ETLProcess.APP.Models
{
    public class Initializer
    {
        public static IConfigurationRoot Configuration; //appsettings.json dosyasını okumak için oluşturduk
        public static IConfigurationBuilder builder;


        public static void Build()
        {
            builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }
    }
}
