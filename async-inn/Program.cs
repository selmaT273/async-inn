﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using async_inn.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace async_inn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            UpdateDatabase(host.Services);

            host.Run();
        }

        private static void UpdateDatabase(IServiceProvider services)
        {
            using (var serviceScope = services.CreateScope())
            {
                using (var db = serviceScope.ServiceProvider.GetService<AsyncInnDbContext>())
                {
                    db.Database.Migrate();
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
