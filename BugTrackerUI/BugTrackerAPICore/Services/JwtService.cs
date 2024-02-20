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

            string cryptographyKey = _configuration["CryptographySettings:Key"];
            byte[] encyptedData = _encryptionService.EncryptData(dataToSend, cryptographyKey);
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            
            //Check if both thw data sent is correct in the ui and api

            string data = Convert.ToBase64String(encyptedData);

            var tokenclaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim("userId", data)
            };

            var token = new JwtSecurityToken(_configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"], 
                tokenclaims, 
                expires: DateTime.Now.AddSeconds(2), 
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
