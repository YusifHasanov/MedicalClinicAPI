using AutoMapper;
using Azure.Core;
using Business.Abstract;
using Core.Utils;
using Core.Utils.Constants;
using Core.Utils.Exceptions;
using DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Dto;
using Entities.Dto.Request;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Dto.Response;
using Entities.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserService : BaseService<User, UpdateUser, CreateUser>, IUserService
    {
        public UserService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILogService logService, Globals globals) : base(unitOfWorkRepository, mapper, logService, globals)
        {
        }

        public override async Task<User> AddAsync(CreateUser entity)
        {
            try
            {
                var newUser = _mapper.Map<User>(entity);
                var existUser = _unitOfWorkRepository.UserRepository.GetOne(user => user.UserName.Equals(newUser.UserName));
                if (existUser != null)
                    throw new Exception($"User Is Exist with Username {newUser.UserName}"); 

                newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
                var accessToken = GenerateJwtAccessToken(newUser);
                newUser.AccessToken = accessToken;
                await _unitOfWorkRepository.UserRepository.AddAsync(newUser);
                await SaveChangesAsync();
                _logService.Log($"New user added successfully with username= {newUser.UserName}");
                return newUser;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }
        public async Task<AuthResponse> Login(LoginUser loginUser)
        {
            try
            {
                var authUser = _unitOfWorkRepository.UserRepository
                    .GetOne(user => user.UserName.Equals(loginUser.UserName)
                    && user.Role.Equals(loginUser.Role));
                if (authUser == null)
                {
                    throw new NotFoundException("User Not Found");
                }
                VerifyPassword(authUser, loginUser.Password);

                var accessToken = GenerateJwtAccessToken(authUser);

                authUser.AccessToken = accessToken;
                await SaveChangesAsync();

                AuthResponse response = new()
                {
                    AccessToken = accessToken
                };
                return response;

            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }
        private string GenerateJwtAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_globals.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Authentication, user.Password.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        };

            var token = new JwtSecurityToken(
                issuer: _globals.Issuer,
                audience: _globals.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(10),
                signingCredentials: credentials
            ); ;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public User GetUserByAccessToken(string accessToken)
        {
            try
            {
                var user = _unitOfWorkRepository.UserRepository.GetOne(user => user.AccessToken.Equals(accessToken));

                return user;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }
        private void ValidateUserAuthorization(User authUser,bool checkRole = true)
        {
            if (authUser == null)
            {
                throw new NotFoundException("User Not Found");
            }
            if (authUser.Role != Role.Admin && checkRole)
            {
                throw new UnauthorizedException("You don't have authorization");
            }

        }
        private void VerifyPassword(User authUser, string password)
        {
            if (!BCrypt.Net.BCrypt.Verify(password, authUser.Password))
            {
                _logService.Log($"User not found with username = {authUser.UserName} && password = {password}");
                throw new UnauthorizedException("Username or password is wrong");
            }
        }
        public async override Task<User> DeleteAsync(int id)
        {
            try
            {
                var exist = IsExist(id);
                _unitOfWorkRepository.UserRepository.Delete(id);
                await SaveChangesAsync();
                _logService.Log($"User Deleted With id {id}");
                return exist;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }
        public override IQueryable<User> GetAll()
        {
            try
            {
                var result = _unitOfWorkRepository.UserRepository.GetAll();
                _logService.Log($"All Users Selected");

                return result;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public override User GetById(int id)
        {
            try
            {
                _logService.Log($"Select User byId = {id}");
                return IsExist(id);
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public override User IsExist(int id)
        {
            var user = _unitOfWorkRepository.UserRepository.GetById(id);
            return user ?? throw new NotFoundException($"User not found with id = {id}");
        }

        public async override Task SaveChangesAsync()
        {
            try
            {
                await _unitOfWorkRepository.UserRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }

        }

        public async override Task<User> UpdateAsync(int id, UpdateUser updateUser)
        {
            try
            {
                var authUser = _unitOfWorkRepository.UserRepository.GetOne(user => user.UserName.Equals(updateUser.UserName) && user.Role.Equals(updateUser.Role));

                ValidateUserAuthorization(authUser);
                VerifyPassword(authUser, updateUser.Password);

                var user = _mapper.Map(updateUser, authUser);
                _unitOfWorkRepository.UserRepository.Update(user);
                await SaveChangesAsync();
                _logService.Log($"User updated with id = {id}");
                return user;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }

        public async Task<User> RefreshAccessTokenAsync(RefreshTokenDto token)
        {
            try
            {

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer =   _globals.Issuer, 
                    ValidAudience = _globals.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_globals.SecretKey)),
                };
      
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token.AccessToken, tokenValidationParameters, out validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
            
                string username = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? "";
                string password = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Authentication)?.Value ?? "";

                User user = _unitOfWorkRepository.UserRepository
                    .GetOne(user => user.UserName.Equals(username) && user.AccessToken.Equals(token.AccessToken));
                ValidateUserAuthorization(user, false);
                if (!user.Password.Equals(password))
                {
                    throw new UnauthorizedException("Password is wrong");
                }
                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    var newToken = GenerateJwtAccessToken(user);
                    user.AccessToken = newToken;
                    await SaveChangesAsync();

                }
                return user;
            }
            catch (Exception ex)
            {
                _logService.Log(ex.Message);
                throw;
            }
        }
    }
}
