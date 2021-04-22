using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetsMicroservice;
using PetsMicroservice.Repositories;
using PetsMicroservice.Repositories.Context;
using PetsMicroservice.Services;

[assembly: FunctionsStartup(typeof(Startup))]
namespace PetsMicroservice
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // In-memory database for Development
            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("AppDbContext"));

            // Depedency Injection
            builder.Services.AddScoped<IPetService, PetService>();
            builder.Services.AddScoped<IPetRepository, PetRepository>();
        }
    }
}
