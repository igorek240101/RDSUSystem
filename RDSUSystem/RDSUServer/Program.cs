using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDSUServer.Controllers;

namespace RDSUServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            UsersController.timerstart.Interval = (DateTime.Today.AddDays(1) - DateTime.Now).TotalMilliseconds;
            UsersController.timerstart.Elapsed += new System.Timers.ElapsedEventHandler(UsersController.TimerStart);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
