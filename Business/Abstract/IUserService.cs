using Entities.Dto.Request;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Dto.Response;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService 
    {
        public IQueryable<UserResponse> GetAll();
        public Task<UserResponse> GetByIdAsync(int id);
        public Task<User> DeleteAsync(int id); 
        public Task<User> UpdateAsync(int id, UpdateUser entity);
        public Task<User> AddAsync(CreateUser entity);
        Task<AuthResponse> Login(LoginUser loginUser);
        Task<User> GetUserByAccessToken(string accessToken);
        Task<AuthResponse> RefreshAccessTokenAsync(RefreshTokenDto refreshToken);
        JwtSecurityToken  DecryptJWT(RefreshTokenDto token);

    }
}
