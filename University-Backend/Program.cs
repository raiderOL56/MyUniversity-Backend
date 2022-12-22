using Microsoft.OpenApi.Models;
using University_Backend.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("University-Backend/appsettings.json").Build();

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
// JWT => 1.- Add service of JWT Authorization
builder.Services.AddJwtService(builder.Configuration);
// JWT => 2.- Add Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
});
// JWT => 3.- Config Swagger to take care of authorization of JWT
builder.Services.AddSwaggerGen(options =>
{
    // We define the Security for authorization
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header using Bearer Scheme"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }, new string[]{}
        }
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
