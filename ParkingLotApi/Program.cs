using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Repository;
using ParkingLotApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ParkingLotService>();

builder.Services.AddDbContext<ParkingLotDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Default");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ParkingLotDbContext>();

    // if (dbContext.Database.ProviderName.ToLower().Contains("mysql"))
    // {
    //     dbContext.Database.Migrate();
    // }
    dbContext.Database.EnsureDeleted(); // delete database
    dbContext.Database.EnsureCreated();

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}