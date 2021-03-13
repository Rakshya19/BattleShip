using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BattleShip.webAPI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var boardPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "board.json");
                    System.IO.File.WriteAllText(boardPath, "");

                    var shipPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ship.json");
                    System.IO.File.WriteAllText(shipPath, "");

                    var shipPlacementPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "shipPlacement.json");
                    System.IO.File.WriteAllText(shipPlacementPath, "");
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while initialising application");
                }
            }
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
