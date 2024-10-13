using Microsoft.OpenApi.Models;

namespace Store.Web.Extentions
{
    public static class SwagerServiceExtension
    {
        public static IServiceCollection AddSwagerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title = "Store Api ",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "John Walker",
                        Email = "John.Walker@gmail.com",
                        Url = new Uri("https://twitter.com/jwalker"),

                    }

                });
                var securityScheme = new OpenApiSecurityScheme
                {
                Description="Jwt Authorization header using the Bearer scheme.Example:Bearer {token}\"",
                Name="Authorization",
                In=ParameterLocation.Header,
                Type=SecuritySchemeType.ApiKey,
                Scheme="bearer",
                Reference=new OpenApiReference
                {
                    Id="bearer",
                    Type=ReferenceType.SecurityScheme
                },
                };
                options.AddSecurityDefinition("bearer", securityScheme);

                var securityRequirements = new OpenApiSecurityRequirement
                {
                    {securityScheme,new []{"bearer"} }
                
                };

                options.AddSecurityRequirement(securityRequirements);
            });
            return services;
        }
    }
}
