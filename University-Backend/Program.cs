using Microsoft.AspNetCore.Mvc; // VERSION CONTROL => 1.1.- Usings to work with Version Control
using Microsoft.AspNetCore.Mvc.ApiExplorer; // VERSION CONTROL => 1.2.- Usings to work with Version Control
using University_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

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

app.UseAuthorization();

app.MapControllers();

app.Run();
