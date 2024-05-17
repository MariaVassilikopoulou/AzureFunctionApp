using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheFunctionAppAzure;
using TheFunctionAppAzure.Db;
using TheFunctionAppAzure.Interfaces;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context,services) =>
    {
        var configuration = context.Configuration;
        string? connectionString = configuration["TheConnectionString"];

        services.AddDbContext<MyDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
  
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<IProductRepository, ProductRepository>();
    })
         .Build();

host.Run();
