using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Auth
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;
        private readonly JwtOptions jwtOptions;
        private readonly SecurityKey issuerSigningKey;
        private readonly SigningCredentials signingCredentials;
        private readonly JwtHeader jwtHeader;
        private readonly TokenValidationParameters tokenValidationParameters;

        public JwtHandler(IOptions<JwtOptions> jwtOptions)
        {
            jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            this.jwtOptions = jwtOptions.Value;
            issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtOptions.SecretKey));
            signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
            jwtHeader = new JwtHeader(signingCredentials);
            tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuer = this.jwtOptions.Issuer,
                IssuerSigningKey = issuerSigningKey
            };
        }
        public CustomJsonWebToken Create(Guid userId)
        {
            var nowUtc = DateTimeOffset.UtcNow;
            var expiration = nowUtc.AddMinutes(jwtOptions.ExpiryMinutes);
            var exp = expiration.ToUnixTimeSeconds();
            var now = nowUtc.ToUnixTimeSeconds();
            var payload = new JwtPayload
            {
                {"sub", userId},
                {"iss", jwtOptions.Issuer},
                {"iat", now},
                {"exp", exp},
                {"unique_name", userId}
            };
            var jwt = new JwtSecurityToken(jwtHeader, payload);
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new CustomJsonWebToken
            {
                Token = token,
                Expire = exp
            };
        }
    }
}
