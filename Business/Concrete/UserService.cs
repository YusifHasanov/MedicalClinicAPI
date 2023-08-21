using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using Business.Abstract;
using Core.Utils;
using Core.Utils.Constants;
using Core.Utils.Exceptions;
using DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Dto.Request;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Dto.Response;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Http;
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
    public class UserService : BaseService<User, UpdateUser, CreateUser, UserResponse>, IUserService
    {
        public UserService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILogService logService, Globals globals, IHttpContextAccessor httpContextAccessor) : base(unitOfWorkRepository, mapper, logService, globals, httpContextAccessor)
        {
        }

        public override async Task<User> AddAsync(CreateUser entity)
        {
            try
            {
                var newUser = _mapper.Map<User>(entity);
                var existUser = await _unitOfWorkRepository.UserRepository.GetOneAsync(user => user.UserName.Equals(newUser.UserName));
                if (existUser != null)
                    throw new Exception($"User Is Exist with Username {newUser.UserName}");

                newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
                var accessToken = GenerateJwtAccessToken(newUser);
                newUser.AccessToken = accessToken;
                await _unitOfWorkRepository.UserRepository.AddAsync(newUser);
                await SaveChangesAsync();

                await _logService.InfoAsync($"New user added successfully with username= {newUser.UserName}");

                return newUser;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Line :58 && UserService.cs");
                throw;
            }
        }
        public async Task<AuthResponse> Login(LoginUser loginUser)
        {
            try
            {
                var authUser = await _unitOfWorkRepository.UserRepository
                    .GetOneAsync(user => user.UserName.Equals(loginUser.UserName)
                    /*&& user.Role.Equals(loginUser.Role)*/) ?? throw new NotFoundException("User Not Found");
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
                await _logService.ErrorAsync(ex, "Line :85 && UserService.cs");
                throw;
            }
        }
        private string GenerateJwtAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_globals.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim("password", user.Password.ToString()),
            new Claim("name", user.UserName),
            new Claim("role", user.Role.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _globals.Issuer,
                audience: _globals.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: credentials
            ); ;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<User> GetUserByAccessToken(string accessToken)
        {
            try
            {
                var user = await _unitOfWorkRepository.UserRepository.GetOneAsync(user => user.AccessToken.Equals(accessToken));

                return user;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Line :120 && UserService.cs");
                throw;
            }
        }
        private void ValidateUserAuthorization(User authUser, bool checkRole = true)
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

                throw new UnauthorizedException("Username or password is wrong");
            }
        }
        public async override Task<User> DeleteAsync(int id)
        {
            try
            {
                var exist = await IsExistAsync(id);
                _unitOfWorkRepository.UserRepository.Delete(id);
                await SaveChangesAsync();
                await _logService.InfoAsync($"User Deleted With id {id}");
                return exist;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Line :120 && UserService.cs");
                throw;
            }
        }
        public override async Task<IQueryable<UserResponse>> GetAll()
        {
            try
            {
                var result = _unitOfWorkRepository.UserRepository.GetAll()
                    .ProjectTo<UserResponse>(_mapper.ConfigurationProvider);
                await _logService.InfoAsync($"All Users Selected");

                return result;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Line :172 && UserService.cs");
                throw;
            }
        }

        public override async Task<UserResponse> GetById(int id)
        {
            try
            {

                var user =await IsExistAsync(id);
                await _logService.InfoAsync($"Select User byId = {id}");
                var userResponse = _mapper.Map<UserResponse>(user);
                return userResponse;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Line :189 && UserService.cs");
                throw;
            }
        }

        public override async Task<User> IsExistAsync(int id)
        {
            var user = await _unitOfWorkRepository.UserRepository.GetByIdAsync(id);
            return user ?? throw new NotFoundException($"User not found with id = {id}");
        }

        public async override Task SaveChangesAsync()
        {
            await _unitOfWorkRepository.UserRepository.SaveChangesAsync();


        }

        public async override Task<User> UpdateAsync(int id, UpdateUser updateUser)
        {
            try
            {
                var authUser = await _unitOfWorkRepository.UserRepository.GetOneAsync(user => user.UserName.Equals(updateUser.UserName) && user.Role.Equals(updateUser.Role));

                ValidateUserAuthorization(authUser);
                VerifyPassword(authUser, updateUser.Password);

                var user = _mapper.Map(updateUser, authUser);
                _unitOfWorkRepository.UserRepository.Update(user);
                await SaveChangesAsync();
                await _logService.InfoAsync($"User updated with id = {id}");
                return user;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Line :224 && UserService.cs");
                throw;
            }
        }

        public async Task<AuthResponse> RefreshAccessTokenAsync(RefreshTokenDto token)
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
                    ValidIssuer = _globals.Issuer,
                    ValidAudience = _globals.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_globals.SecretKey)),
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token.AccessToken, tokenValidationParameters, out validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;

                string username = jwtToken.Claims.FirstOrDefault(x => x.Type == "name")?.Value ?? "";
                string password = jwtToken.Claims.FirstOrDefault(x => x.Type == "password")?.Value ?? "";
                string role = jwtToken.Claims.FirstOrDefault(x => x.Type == "role")?.Value ?? "";
                User user = await _unitOfWorkRepository.UserRepository
                    .GetOneAsync(user =>
                        user.UserName.Equals(username) &&
                        user.AccessToken.Equals(token.AccessToken));

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

                AuthResponse response = new()
                {
                    AccessToken = user.AccessToken
                };
                return response;
            }
            catch (Exception ex)
            {
                await _logService.ErrorAsync(ex, "Line :280 && UserService.cs");
                throw;
            }
        }
    }
}
