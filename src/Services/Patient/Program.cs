using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace MedicalSystem.Services.Patient
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
                webBuilder.ConfigureKestrel(options =>
                {
                    // Setup a HTTP/2 endpoint without TLS.
                    options.ListenLocalhost(5001, o => o.Protocols =
                        HttpProtocols.Http2);
                });
                webBuilder.UseStartup<Startup>();
            });
    }
}
