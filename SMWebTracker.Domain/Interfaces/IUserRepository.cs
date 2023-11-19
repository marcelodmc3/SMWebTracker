using SMWebTracker.Domain.Dtos;
using SMWebTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindByEmailAsyn(string email);
        Task<User> FindActiveByEmailAsync(string email);
        Task AddUser(User user);        
    }
}
