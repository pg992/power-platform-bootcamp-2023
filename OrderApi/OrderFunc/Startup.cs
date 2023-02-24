using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderFunctions.Data;
using System;

[assembly: FunctionsStartup(typeof(OrderFunc.Startup))]
namespace OrderFunc
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<PowerPlatformBootcampContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("PowerPlatfromDbConnString")));
        }
    }
}
