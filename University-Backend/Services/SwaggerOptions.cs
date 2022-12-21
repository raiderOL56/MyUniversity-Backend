using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace University_Backend.Services
{
    public class SwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        public SwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // Add swagger documentation for each API version
            foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
                options.SwaggerDoc(description.GroupName, CreateApiInfo(description));
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        private OpenApiInfo CreateApiInfo(ApiVersionDescription description)
        {
            OpenApiInfo openApiInfo = new OpenApiInfo()
            {
                Title = "My University Backend",
                Version = description.ApiVersion.ToString(),
                Description = "This is my first API",
                Contact = new OpenApiContact
                {
                    Email = "martin@test.com.mx",
                    Name = "Martin Carrera"
                }
            };

            if (description.IsDeprecated)
                openApiInfo.Description += " | This API has been deprecated";

            return openApiInfo;
        }
    }
}