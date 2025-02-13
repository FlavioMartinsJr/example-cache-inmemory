using Produto.Infrastructure.IOC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Produto.API.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddAuth(this IServiceCollection services, Identity identitySettings)
        {
            var authenticationBuilder = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,/* aqui falso ele não valida a data de expiração*/
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = identitySettings.Issuer,
                    ValidAudience = identitySettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identitySettings.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
