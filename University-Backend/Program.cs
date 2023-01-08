using Microsoft.AspNetCore.Mvc; // VERSION CONTROL => 1.1.- Usings to work with Version Control
using Microsoft.AspNetCore.Mvc.ApiExplorer; // VERSION CONTROL => 1.2.- Usings to work with Version Control
using Microsoft.OpenApi.Models;
using University_Backend.Services;
using Microsoft.EntityFrameworkCore; // EF => 1.- Usings to work whit EF
using University_Backend.Data;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build(); // EF => 2.- Create object to get appsettings.json

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// VERSION CONTROL => 2.- Add app versioning control
builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = new ApiVersion(1, 0); // Establecer la versión mayor y la versión menor
    setup.AssumeDefaultVersionWhenUnspecified = true; // Establecer la versión mayor si el cliente no especifíca una versión
    setup.ReportApiVersions = true;
});
// VERSION CONTROL => 3.- Add configuration to document versions
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV"; // 1.0.0, etc.
    setup.SubstituteApiVersionInUrl = true; // Add version in URL
});
// VERSION CONTROL => 4.- Configure options
builder.Services.ConfigureOptions<SwaggerOptions>();
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
builder.Services.AddJwtService(configuration);
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
// EF => 3.- Get connection string
string? connectionString = configuration.GetConnectionString("UniversityDB");
// EF => 4.- Database connection
builder.Services.AddDbContext<UniversityContext>(options => options.UseSqlServer(connectionString));


var app = builder.Build();

// VERSION CONTROL => 5.- Configure endpoints for swagger DOCS for each of the versions of our API
IApiVersionDescriptionProvider provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint
            (
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant()
            ); // Example URL: /swagger/v2/swagger.json
        }
    });
}

app.UseHttpsRedirection();

// CORS => 2.- Add service CORS | IMPORTANTE: Colocar siempre antes de app.UseAuthorization
app.UseCors("_myAlllowSpecificOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();