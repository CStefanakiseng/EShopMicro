using EShopMicro.Basket.API.GrpcServices;
using EShopMicro.Basket.API.Repositories;
using EShopMicro.Discount.Grpc.Protos;
using MassTransit;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//AutoMapper Configuration
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Redis Configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

//General Configuration
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

//Grpc Configuration
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o =>
    o.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]));
builder.Services.AddScoped<DiscountGrpcService>();

//Masstransit - RabbitMQ Configuration
builder.Services.AddMassTransit(cfg =>
{
    cfg.UsingRabbitMq((ctx, cfgr) =>
    {
        cfgr.Host(builder.Configuration["EventBusSettings:HostAddress"]);
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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket.API", Version = "v1" });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
