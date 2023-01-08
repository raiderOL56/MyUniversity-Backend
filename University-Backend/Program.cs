using Microsoft.EntityFrameworkCore; // EF => 1.- Usings to work whit EF
using University_Backend.Data;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build(); // EF => 2.- Create object to get appsettings.json

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// EF => 3.- Get connection string
string? connectionString = configuration.GetConnectionString("UniversityDB");
// EF => 4.- Database connection
builder.Services.AddDbContext<UniversityContext>(options => options.UseSqlServer(connectionString));


var app = builder.Build();

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