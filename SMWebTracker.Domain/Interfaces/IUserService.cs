using SMWebTracker.Domain.Dtos;
using SMWebTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Domain.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(NewUserModel newUserModel, string loggedUserEmail);
        Task<TokenModel> Login(LoginModel loginModel);
        Task<bool> IsAdmin(string email);
    }
}
