using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthorizationSample.Configuration;
using AuthorizationSample.Constants;
using AuthorizationSample.Dtos;
using AuthorizationSample.Models;
using AuthorizationSample.Results;
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

        public ILoginResult Login(UserCredentialDataDto userCredentialDataDto)
        {
            if (userCredentialDataDto == null)
            {
                return new FailedLoginResult();
            }

            var userRole = GetUserRole(userCredentialDataDto.Password);

            if (string.IsNullOrEmpty(userRole))
            {
                return new FailedLoginResult();
            }

            var jwtSecurityToken = GenerateToken(userCredentialDataDto.UserName, userRole);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return new SuccessfulLoginResult(token);
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

        private string GetUserRole(string password)
        {
            if (password == AdministratorRolePassword)
            {
                return Roles.AdministratorRole;
            }

            if (password == EmployeeRolePassword)
            {
                return Roles.EmployeeRole;
            }

            return string.Empty;
        }
    }
}
