using Entities.Dto.Request;
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Dto.Response;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService:IService<User,UpdateUser,CreateUser,UserResponse>
    {
        Task<AuthResponse> Login(LoginUser loginUser);
        Task<User> GetUserByAccessToken(string accessToken);
        Task<AuthResponse> RefreshAccessTokenAsync(RefreshTokenDto refreshToken);


    }
}
