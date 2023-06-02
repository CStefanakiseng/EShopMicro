using EShopMicro.EventBus.Messages.Common;
using EShopMicro.Ordering.API.EventBusConsumers;
using EShopMicro.Ordering.API.Extensions;
using EShopMicro.Ordering.Application;
using EShopMicro.Ordering.Infrastructure;
using EShopMicro.Ordering.Infrastructure.Persistence;
using MassTransit;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<BasketCheckoutConsumer>();

//Masstransit - RabbitMQ Configuration
builder.Services.AddMassTransit(cfg =>
{
    cfg.AddConsumer<BasketCheckoutConsumer>();
    cfg.UsingRabbitMq((ctx, cfgr) =>
    {
        cfgr.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfgr.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, endpoint =>
        {
            endpoint.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
        });
    });
});
builder.Services.AddMassTransitHostedService();
//builder.Services.AddOptions<MassTransitHostOptions>()
//    .Configure(options =>
//    {
//        options.WaitUntilStarted = true;
//        options.StartTimeout = TimeSpan.FromSeconds(30);
//        options.StopTimeout = TimeSpan.FromMinutes(1);
//    });





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ordering.API", Version = "v1" });
});


var app = builder.Build();

await app.MigrateDatabaseAsync<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed.SeedAsync(context, logger).Wait();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering.API v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
