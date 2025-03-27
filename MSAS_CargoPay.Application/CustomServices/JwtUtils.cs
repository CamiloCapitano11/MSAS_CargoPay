using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using MSAS_CargoPay.Core.Entities.V1._0;
using MSAS_CargoPay.Core.Interfaces;
using MSAS_CargoPay.Core.Resources;
using MSAS_CargoPay.Infrastracture.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MSAS_CargoPay.Application.CustomServices
{
    public class JwtUtils : IJwtUtils
    {
        private readonly IConfiguration _configuration;
        private readonly DatabaseLogger _loggerError;
        public JwtUtils(IConfiguration configuration)
        {
            _configuration = configuration;
            _loggerError = new DatabaseLogger(configuration);
        }
        public async Task<object> ValidateJwtTokenAsync(string token)
        {
            if (token == null)
                return null;
            try
            {
                var TokenInfo = new Dictionary<string, string>();
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var claims = jwtSecurityToken.Claims.ToList();
                foreach (var claim in claims)
                {
                    TokenInfo.Add(claim.Type, claim.Value);
                }
                var keySecret = "SKey Producto API Management";
                var audience = "ClientID Producto API Management";
                var Tenant = _configuration["JwtSettings:Tenant"];
                var Issuer = claims[1].Value;
                var rsa = RSA.Create();
                rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(keySecret), out _);
                var mySecurityKey = new RsaSecurityKey(rsa);
                var stsDiscoveryEndpoint = string.Format(CultureInfo.InvariantCulture, "https://login.microsoftonline.com/{0}/.well-known/openid-configuration", Tenant);
                var configManager = new ConfigurationManager<OpenIdConnectConfiguration>(stsDiscoveryEndpoint, new OpenIdConnectConfigurationRetriever());
                var config = await configManager.GetConfigurationAsync();
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ClockSkew = TokenValidationParameters.DefaultClockSkew,
                    ValidAudience = audience,
                    ValidIssuer = Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKeys = config.SigningKeys,
                    ValidateLifetime = true,
                    IssuerSigningKey = mySecurityKey,
                    ValidateIssuer = true,
                };
                var validatedToken = (SecurityToken)new JwtSecurityToken();
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                string jsonValidacion = JsonSerializer.Serialize((JwtSecurityToken)validatedToken);
                return validatedToken;
            }
            catch (Exception ex)
            {
                var Error = new
                {
                    Status = "error",
                    Code = 0,
                    Message = "Error del servicio",
                    Description = ex.Message,
                };
                LogApi _logApi = new LogApi(Messages.Microservice, "ValidateJwtTokenAsync", "", "", Error, string.Empty);
                _loggerError.Log(_logApi, string.Empty, "0");
                return ex.Message;
            }
        }
    }
}
