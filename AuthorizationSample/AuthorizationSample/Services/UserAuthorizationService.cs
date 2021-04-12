using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthorizationSample.Configuration;
using AuthorizationSample.Constants;
using AuthorizationSample.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationSample.Services
{
    public class UserAuthorizationService : IUserAuthorizationService
    {
        private const string AdministratorRolePassword = "administrator";
        private const string EmployeeRolePassword = "employee";

        private readonly TokenConfiguration _tokenConfiguration;

        public UserAuthorizationService(IOptions<TokenConfiguration> tokenConfigurationOptions)
        {
            _tokenConfiguration = tokenConfigurationOptions.Value;
        }

        public string Login(UserCredentialData userCredentialData)
        {
            if (userCredentialData == null)
            {
                return null;
            }

            if (userCredentialData.Password == AdministratorRolePassword)
            {
                var token = GenerateToken(userCredentialData.UserName, Roles.AdministratorRole);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            if (userCredentialData.Password == EmployeeRolePassword)
            {
                var token = GenerateToken(userCredentialData.UserName, Roles.EmployeeRole);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return null;
        }

        private JwtSecurityToken GenerateToken(string userName, string role)
        {
            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role),
            };

            return new JwtSecurityToken(
                claims: claimList,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Key)), SecurityAlgorithms.HmacSha256)
            );
        }
    }
}
