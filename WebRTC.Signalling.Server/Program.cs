using System.Net;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

using WebRTC.Signalling.Server.Hubs;

namespace WebRTC.Signalling.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(opts =>
                    {
                        opts.Listen(IPAddress.Loopback, port: 5002);
                        opts.ListenAnyIP(5004);
                        opts.ListenLocalhost(5001);
                        opts.ListenLocalhost(5000, opts => opts.UseHttps());
                    });
                });
    }
}
