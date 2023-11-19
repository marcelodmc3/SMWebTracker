using Microsoft.EntityFrameworkCore;
using SMWebTracker.Domain.Entities;
using SMWebTracker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TrackerDB _trackerDB;

        public UserRepository(TrackerDB trackerDB)
        {
            _trackerDB = trackerDB;
        }

        public async Task AddUser(User user)
        {
            await _trackerDB.Users.AddAsync(user);
            await _trackerDB.SaveChangesAsync();
        }

        public async Task<User> FindByEmailAsyn(string email)
        {
            return await _trackerDB.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Login.Equals(email.ToLower()));
        }

        public async Task<User> FindActiveByEmailAsync(string email)
        {
            return await _trackerDB.Users
                .AsNoTracking()
                .Where(u => u.Active)
                .FirstOrDefaultAsync(u => u.Login.Equals(email.ToLower()));
        }
    }
}
