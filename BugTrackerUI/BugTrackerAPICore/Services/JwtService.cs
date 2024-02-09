using BugTrackerAPICall.Interfaces;
using BugTrackerUICore.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerUICore.Services
{
    public class JwtService : IJwtService
    {
        private IConfiguration _configuration;
        private IEncryptionService _encryptionService;
        public JwtService(IConfiguration configuration, IEncryptionService encryptionService)
        {
            _configuration = configuration;
            _encryptionService = encryptionService;
        }
        public string GenerateJsonWebToken(string userName, string dataToSend)
        {
            byte[] encyptedData = _encryptionService.EncryptData(dataToSend);
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenclaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim("userId", encyptedData.ToString())
            };

            var token = new JwtSecurityToken(_configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"], 
                tokenclaims, 
                expires: DateTime.Now.AddMinutes(120), 
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
