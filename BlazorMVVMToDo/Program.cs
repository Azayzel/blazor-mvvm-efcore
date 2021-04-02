using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlazorMVVMToDo.Data;
using BlazorMVVMToDo.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BlazorMVVMToDo
{
    public class Program
    {
        public static async Task Main(string[] args) {
            var host = CreateHostBuilder(args).Build();

            // this section sets up and seeds the database. It would NOT normally
            // be done this way in production. It is here to make the sample easier,
            // i.e. clone, set connection string and run.
            var sp = host.Services.GetService<IServiceScopeFactory>()
                .CreateScope()
                .ServiceProvider;
            var options = sp.GetRequiredService<DbContextOptions<JASContext>>();

            //await SeedDBAsync(options, 500);

            // back to your regularly scheduled program

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        /// <summary>
        /// Method to see the database. Should not be used in production: demo purposes only.
        /// </summary>
        /// <param name="options">The configured options.</param>
        /// <param name="count">The number of contacts to seed.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        private static async Task SeedDBAsync(DbContextOptions<JASContext> options, int count) {
            // empty to avoid logging while inserting (otherwise will flood console)
            var factory = new LoggerFactory();
            var builder = new DbContextOptionsBuilder<JASContext>(options)
                .UseLoggerFactory(factory);

            using var context = new JASContext(builder.Options);
            // result is true if the database had to be created
            if (await context.Database.EnsureCreatedAsync()) {
                var issueSeed = new SeedIssues();
                await issueSeed.SeedIssuesAsync(context);

            }

            RelationalDatabaseCreator databaseCreator =
    (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
            try {
                databaseCreator.CreateTables();
            }
            catch (Exception err) { }
            finally {
                Debug.WriteLine("Seeding Table");
                var issueSeed = new SeedIssues();
                await issueSeed.SeedIssuesAsync(context);
            }


        }
    }
}
