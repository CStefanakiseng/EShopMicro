using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOcelot()
    .AddCacheManager(settings => settings.WithDictionaryHandle());
builder.Host.ConfigureAppConfiguration((hostingContext, cfg) =>
{
    cfg.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
});

builder.Host.ConfigureLogging((hostingContext, loggingBuilder) =>
{
    loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

await app.UseOcelot();
app.Run();
