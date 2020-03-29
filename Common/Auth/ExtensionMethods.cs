using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Auth
{
    public static class ExtensionMethods
    {
        public static void AddJwt(this IServiceCollection service, IConfiguration configuration)
        {
            var jwtOptions = new JwtOptions();
            var section = configuration.GetSection("jwt");
            section.Bind(jwtOptions);
            service.Configure<JwtOptions>(section);
            service.AddSingleton<IJwtHandler, JwtHandler>();
            service.AddAuthentication()
            .AddJwtBearer( cfg =>
                    {
                        cfg.RequireHttpsMetadata = false;
                        cfg.SaveToken = true;
                        cfg.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = false,
                            ValidIssuer = jwtOptions.Issuer,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                        };
                    });
        }
    }
}
