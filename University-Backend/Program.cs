using Microsoft.EntityFrameworkCore; // EF => 1.- Usings to work whit EF
using University_Backend.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("University-Backend/appsettings.json");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// EF => 2.- Get connection string
string? connectionString = builder.Configuration.GetConnectionString("UniversityDB");
// EF => 3.- Database connection
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
