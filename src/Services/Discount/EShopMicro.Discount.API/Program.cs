using EShopMicro.Discount.API.Extensions;
using EShopMicro.Discount.API.Repositories;

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Discount.API", Version = "v1" });
});
var app = builder.Build();
await app.MigrateAndSeedDatabaseAsync();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Discount.API v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();

//async Task SeedDatabaseAsync(WebApplication app, int? retry =0)
//{
//    int retryForAvailability = retry.Value;
//    using (var scope = app.Services.CreateScope())
//    {
//        var services = scope.ServiceProvider;
//        var configuration = services.GetRequiredService<IConfiguration>();  
//        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
//        var logger = loggerFactory.CreateLogger<Program>();

//        try
//        {
//            logger.LogInformation("Migrating postgresql database");
//            using var connection = new NpgsqlConnection
//                       (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
//            connection.Open();

//            using var command = new NpgsqlCommand
//            {
//                Connection = connection
//            };

//            command.CommandText = "DROP TABLE IF EXISTS Coupon";
//            command.ExecuteNonQuery();

//            command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
//                                                                ProductName VARCHAR(24) NOT NULL,
//                                                                Description TEXT,
//                                                                Amount INT)";
//            command.ExecuteNonQuery();

//            command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
//            command.ExecuteNonQuery();

//            command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
//            command.ExecuteNonQuery();

//            logger.LogInformation("Migrated postresql database.");
//        }
//        catch (NpgsqlException ex)
//        {
//            logger.LogError(ex, "An error occurred while migrating the postresql database");

//            if (retryForAvailability < 50)
//            {
//                retryForAvailability++;
//                System.Threading.Thread.Sleep(2000);
//                await SeedDatabaseAsync(app, retryForAvailability);
//            }
//        }
//    }
//}



