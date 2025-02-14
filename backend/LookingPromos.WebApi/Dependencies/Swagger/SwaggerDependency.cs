using Asp.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LookingPromos.Dependencies.Swagger;

/// <summary>
/// Represents an extension method to add the SwaggerGen configuration to the service collection.
/// </summary>
public static class SwaggerDependency
{
    /// <summary>
    /// This method is useful to configure Swagger with the versioning and the API key security definition.
    /// Also, we are adding the XML documentation of the endpoints so it can be displayed in the Swagger UI.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static void AddSwaggerGenConfiguration(this IServiceCollection services)
    {
        // Configure the swagger to add the xml documentation of their endpoints
        services.AddSwaggerGen(opt =>
        {
            var assembly = typeof(Program).Assembly.GetName().Name;
            var xmlFile = $"{assembly}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            opt.IncludeXmlComments(xmlPath);

            // Add API Key security definition
            opt.AddSecurityDefinition(
                "x-api-key",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "API key needed to access the endpoints",
                    Name = "x-api-key",
                    Type = SecuritySchemeType.ApiKey
                }
            );

            // Add security requirements for API Key
            opt.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "x-api-key"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            );
        });

        // Configure the swagger to add the versioning of their endpoints
        services
            .AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = new UrlSegmentApiVersionReader();
                opt.DefaultApiVersion = new ApiVersion(1, 0);
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        // Inject the swagger version configuration
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
    }
}
