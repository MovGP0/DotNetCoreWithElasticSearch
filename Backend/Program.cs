using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Backend
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("http://localhost:81")
                .UseStartup<Startup>()
                .Build();
            host.Run();
        }
    }
}
