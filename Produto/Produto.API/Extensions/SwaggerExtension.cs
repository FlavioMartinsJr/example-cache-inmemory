using Produto.Infrastructure.IOC;
using Microsoft.OpenApi.Models;

namespace Produto.API.Extensions
{
    public static class SwaggerExtension
    {
        private static readonly string[] Value = ["Bearer"];
        public static IServiceCollection AddSwaggerOpenAPI(this IServiceCollection services, Settings appSettings)
        {
            services.AddSwaggerGen(options =>
            {
                var openApiInfo = new OpenApiInfo
                {
                    Title = appSettings.ApplicationDetail.ApplicationName,
                    Version = "v1",
                    Description = appSettings.ApplicationDetail.Description,
                    Contact = new OpenApiContact
                    {
                        Email = "flaviojunior1070@gmail.com",
                        Name = "Desenvolvimento Desenrolado",
                        Url = new Uri(appSettings.ApplicationDetail.ContactWebsite),
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                };

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT é a sigla para JSON Web Token e trata-se de um formato compacto e autossuficiente para representar informações entre duas partes de maneira segura como um objeto JSON",
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
                        },
                        new string[] { }
                    }

                });
            });
            return services;
        }

        public static void UseSwagger(this IApplicationBuilder app, Settings appSettings)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.RoutePrefix = "swagger";
                setupAction.SwaggerEndpoint("/swagger/v1/swagger.json", "Produto.API v1");
            });
        }
    }
}
