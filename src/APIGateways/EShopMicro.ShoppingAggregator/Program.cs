using EShopMicro.ShoppingAggregator.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<ICatalogService, CatalogService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ApiSettings:CatalogUrl"]);
});

builder.Services.AddHttpClient<IBasketService, BasketService>(b =>
{
    b.BaseAddress = new Uri(builder.Configuration["ApiSettings:BasketUrl"]);
});

builder.Services.AddHttpClient<IOrderService, OrderService>(o =>
{
    o.BaseAddress = new Uri(builder.Configuration["ApiSettings:OrderingUrl"]);
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopping.Aggregator", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
