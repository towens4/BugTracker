using BugTrackerAPICall.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Services
{
    public class JwtHandler : IJwtHandler
    {
        private JwtSecurityTokenHandler tokenHandler;
        private JwtSecurityToken parsedToken;

        public JwtHandler()
        {
            tokenHandler = new JwtSecurityTokenHandler();
        }

        public JwtHandler ParseToken(string token)
        {
            parsedToken = (JwtSecurityToken?)tokenHandler.ReadToken(token);

            return this;
        }

        public string GetTokenValue()
        {

            var token = parsedToken.Claims.FirstOrDefault(c => c.Type == "userId");

            if (token != null)
            {
                return token.Value;
            }
            else
            {
                return "";
            }

        }

        public byte[] ToTokenBytes()
        {
            string tokenValue = GetTokenValue();
            return Encoding.UTF8.GetBytes(tokenValue);
        }

        public bool isTokenExpired()
        {
            return parsedToken.ValidTo > DateTime.UtcNow;
        }
    }
}
