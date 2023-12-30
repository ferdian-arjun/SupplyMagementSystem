using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace API.Configurations;

public class SwaggerConfiguration
{
    private IConfiguration? _configuration { get; set; }

    public SwaggerConfiguration Configuration(IConfiguration configuration)
    {
        _configuration = configuration;
        return this;
    }

    public SwaggerConfiguration SwaggerGen(IServiceCollection services)
    {
        services.AddSwaggerGen(x =>
        {
                 // Swagger documentation for API version v1
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Supply Management System APIs",
                });

                // Add JWT authentication
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                // Add security requirement for JWT authentication
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                // Map DateOnly to a string with the "date" format
                x.MapType<DateOnly>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "date",
                    Example = new OpenApiString("yyyy-mm-dd")
                });
        });
        
        return this;
    }
}