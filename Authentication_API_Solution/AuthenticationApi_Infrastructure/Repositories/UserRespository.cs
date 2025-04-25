using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationApi_Application.DTOs;
using AuthenticationApi_Application.Interfaces;
using AuthenticationApi_Domain.Entites;
using AuthenticationApi_Infrastructure.Data;
using AutoMapper;
using BCrypt.Net;
using E_Commerce.SharedLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationApi_Infrastructure.Repositories
{
    public class UserRespository(AuthenticationDbContext context, IConfiguration configuration , IMapper mapper ) : IUser
    {
        private async Task<AppUser>GetUserByEmail(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.Email == email);
            return user!;
        }
        public async Task<GetUserDTO> GetUser(int userId)
        {
            var user = await context.Users.FindAsync(userId);
            return  mapper.Map<GetUserDTO>(user); 
        }

        public async Task<Response> Login(LoginDTO loginDTO)
        {
            var getUser = await GetUserByEmail(loginDTO.Email);
            if (getUser == null)
                return new Response(false, "Invalid credentials");

            bool verfiedPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password , getUser.Password);
            if (!verfiedPassword) return new Response(false, "Invalid credentials");
            string token = GenerateToken(getUser);
            return new Response(true , token);
        }   
        
        public  string GenerateToken(AppUser user)
        {
            var key = Encoding.UTF8.GetBytes(configuration.GetSection("Authentication:Key").Value!);
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey , SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
              new (ClaimTypes.Name , user.Name),
              new (ClaimTypes.Email , user.Email),
              
            
            };
            if(!string.IsNullOrEmpty(user.Role) || !Equals("string" , user.Role ))
                 claims.Add(new Claim(ClaimTypes.Role, user.Role));
            var token = new JwtSecurityToken(
                issuer: configuration["Authentication:Issuer"],
                audience: configuration["Authentication:Audience"],
                claims: claims,
                expires: null,
                signingCredentials: credentials

                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<Response> Regsiter(AppUserDTO appUserDTO)
        {
            var getUser = await GetUserByEmail(appUserDTO.Email);
            if(getUser is not null) 
                return new Response(false , $"you can not use this email for registration");
            var result = context.Users.Add(new AppUser()
            { Name = appUserDTO.Name,
             Email = appUserDTO.Email,
             Password = BCrypt.Net.BCrypt.HashPassword(appUserDTO.Password),
             TelephoneNumber = appUserDTO.TelephoneNumber,
             Address = appUserDTO.Address,
             Role = appUserDTO.Role
            });

            await context.SaveChangesAsync();
            return result.Entity.Id > 0 ? new Response(true, "User registered successfully") : new Response(false, "Invalid data provided");

        }
    }
}
