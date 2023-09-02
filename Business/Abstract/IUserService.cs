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
        public Task<User> DeleteAsync(int id); 
        public Task<User> UpdateAsync(int id, UpdateUser entity);
        public Task<User> AddAsync(CreateUser entity);
        public Task<AuthResponse> Login(LoginUser loginUser);
        Task<AuthResponse> RefreshAccessTokenAsync(RefreshTokenDto refreshToken);
        public JwtSecurityToken  DecryptJWT(RefreshTokenDto token);

    }
}
