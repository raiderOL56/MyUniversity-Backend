var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// CORS => 1.- Add configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAlllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5000", "https://localhost:5000");
        policy.WithMethods("GET", "POST", "PUT", "DELETE");
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS => 2.- Add service CORS | IMPORTANTE: Colocar siempre antes de app.UseAuthorization
app.UseCors("_myAlllowSpecificOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();
