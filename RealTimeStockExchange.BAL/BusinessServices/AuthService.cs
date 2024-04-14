using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealTimeStockExchange.BAL.IBusinessServices;
using RealTimeStockExchange.DAL.DTOs;
using RealTimeStockExchange.DAL.Entities;
using RealTimeStockExchange.DAL.IRepositories;
using RealTimeStockExchange.Helpers.Configurations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockExchange.BAL.BusinessServices
{
    public class AuthService : _BusinessService<ApplicationUser, AuthDTO>, IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWTSetting _JWT;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IOptions<JWTSetting> jWT) : base(unitOfWork, mapper)
        {
            _userManager = userManager;
            _JWT = jWT.Value;
        }

        public async Task<AuthDTO> LoginAsync(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return new AuthDTO { Message = "WrongEmail", Status = "Failed" };
            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                return new AuthDTO { Message = "WrongPassword", Status = "Failed" };

            var jwtSecurityTokenForAdmin = await CreateJwtToken(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            return new AuthDTO
            {
                UserId = user.Id,
                UserName = user.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityTokenForAdmin),
                IsAuthenticated = true,
                Message = "Loggedin Successfully",
                Status = "Success",
                IsAdmin = userRoles.Any(t => t.ToLower().Equals("admin")),
            };
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.UserData, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            if (roles.Any())
                claims.Append(new Claim("Role", roles.First()));

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWT.Key));
            var signingCardentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken
            (
                issuer: _JWT.Issuer,
                audience: _JWT.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_JWT.DurationInMinutes),
                signingCredentials: signingCardentials
            );

            return jwtSecurityToken;
        }
    }
}
