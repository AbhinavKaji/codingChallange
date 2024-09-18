using Microsoft.EntityFrameworkCore;
using Rockfast.ApiDatabase;
using Rockfast.ApiDatabase.DomainModels;
using Rockfast.ServiceInterfaces;
using Rockfast.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockfast.Dependencies
{
    public class UserService : IUserService
    {
        private readonly ApiDbContext _database;
        public UserService(ApiDbContext db)
        {
            this._database = db;
        }
        public async Task<UserVM> CreateUserAsync(UserVM userVM)
        {
            try
            {
                var user = new User
                {
                    Username = userVM.UserName
                };

                _database.Users.Add(user);
                await _database.SaveChangesAsync();

                userVM.Id = user.Id;
                return userVM;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating new user.", ex);
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _database.Users.FindAsync(id);
                if (user == null) return false;

                _database.Users.Remove(user);
                await _database.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting user with ID {id}.", ex);
            }
        }

        public async Task<IEnumerable<UserVM>> GetAllUsersAsync()
        {
            try
            {
                var users = await _database.Users.ToListAsync();
                return users.Select(x => new UserVM
                {
                    Id = x.Id,
                    UserName = x.Username
                });
            }
            catch (Exception ex)
            {
                throw new Exception("error retriving all users. ", ex);
            }
        }

        public async Task<UserVM> GetUserAsync(int id)
        {
            try
            {
                var user = await _database.Users.FindAsync(id);
                if (user == null) return null;
                return new UserVM
                {
                    Id = user.Id,
                    UserName = user.Username                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving user with ID {id}. ", ex);
            }
        }

        public async Task<UserVM> UpdateUserAsync(int id, UserVM userVM)
        {
            try
            {
                var user = await _database.Users.FindAsync(id);
                if (user == null) return null;

                user.Username = userVM.UserName;

                await _database.SaveChangesAsync();

                return userVM;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user with ID {id}.", ex);
            }
        }
    }
}
