using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GAB.DistanceCalculation.API
{
    public class TokenManager
    {
        const string domain = "http://localhost:45754"; 
        const string audience = "http://localhost:45754";
        const string secret = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";

        public static string CreateToken(string userName)
        {
            try
            {
                DateTime issuedAt = DateTime.UtcNow;
                DateTime expires = DateTime.UtcNow.AddMinutes(1);

                var tokenHandler = new JwtSecurityTokenHandler();
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userName)
                });
                
                var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secret));
                var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                                
                var token = tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:45754",
                                                            audience: "http://localhost:45754",
                                                            subject: claimsIdentity,
                                                            notBefore: issuedAt,
                                                            expires: expires,
                                                            signingCredentials: signingCredentials);

                var tokenString = tokenHandler.WriteToken(token);

                return tokenString;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ValidateToken(string token)
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secret));
            TokenValidationParameters validationParameters =
                new TokenValidationParameters
                {
                    ValidIssuer = domain,
                    ValidAudiences = new[] { audience },
                    IssuerSigningKey = securityKey
                };

            SecurityToken validatedToken;
            try
            {                
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var user = handler.ValidateToken(token, validationParameters, out validatedToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return validatedToken != null;
        }
    }
}
