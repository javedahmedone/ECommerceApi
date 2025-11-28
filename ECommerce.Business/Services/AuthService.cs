using ECommerce.Business.DTOs;
using ECommerce.Business.ExceptionHandlers;
using ECommerce.Business.Interfaces;
using ECommerce.Business.Mapper;
using ECommerce.Repository.Interfaces;
using ECommerce.Repository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace ECommerce.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICommonUtility _commonUtilty;
        private readonly IConfiguration _config;
        private static readonly int[] rolesList = { 1, 2 };
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthService(IUserRepository userRepository, IConfiguration config, ICommonUtility commonUtilty, IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _config = config;
            _commonUtilty = commonUtilty;
            _contextAccessor = contextAccessor;
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                throw new AppBadRequestException("Email is required.");
            if (!rolesList.Contains(request.Role ))           
                throw new AppBadRequestException("Invalid role. Allowed values: 1= ADMIN, 2 =CUSTOMER");
            
            if (!Regex.IsMatch(request.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new AppBadRequestException("Invalid email format.");

            // PASSWORD VALIDATION
            if (string.IsNullOrWhiteSpace(request.Password))
                throw new AppBadRequestException("Password is required.");

            if (request.Password.Length < 6)
                throw new AppBadRequestException("Password must be at least 6 characters long.");

            if (!Regex.IsMatch(request.Password, @"\d"))
                throw new AppBadRequestException("Password must contain at least one numeric digit.");

            var exists = await _userRepository.GetByEmailAsync(request.Email);
            if (exists != null)
                throw new Exception("Email already registered");

            var hashed = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user  = DtoRequestMapper.UserDTOToDBModel(request, hashed);       

            await _userRepository.AddUserAsync(user);
            return DTOsResponseMapper.RegsisterToDto(user);
        
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                throw new AppBadRequestException("Invalid email or password");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Passwordhash))
                throw new AppBadRequestException("Invalid email or password");

            var token = await GenerateJwt(user);
            return DTOsResponseMapper.LoginToDto(user, token);
        }

        private async Task<string> GenerateJwt(User user)
        {
            var userRole = await _commonUtilty.UserRoleById(user.Role);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Role, userRole.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public long GetUserId()
        {
            var user = _contextAccessor.HttpContext?.User;
            
            var claim = user.Claims.ToList().Find(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("User ID not found in token");

            return long.Parse(claim.Value);
        }

        public string? GetEmail()
        {
            var user = _contextAccessor.HttpContext?.User;
            var claim = user.Claims.ToList().Find(c => c.Type == ClaimTypes.Role);
            if (claim == null)
                throw new UnauthorizedAccessException("User ID not found in token");
            return claim.Value;
        }

        public string? GetRole()
        {
            var user = _contextAccessor.HttpContext?.User;
            var claim = user.Claims.ToList().Find(c => c.Type == ClaimTypes.Role);
            if (claim == null)
                throw new UnauthorizedAccessException("User ID not found in token");
            return claim.Value;
        }



    }
}