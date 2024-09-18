using Rockfast.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockfast.ServiceInterfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserVM>> GetAllUsersAsync();
        Task<UserVM> GetUserAsync(int id);
        Task<UserVM> CreateUserAsync(UserVM userVM);
        Task<UserVM> UpdateUserAsync(int id, UserVM userVM);
        Task<bool> DeleteUserAsync(int id);
    }
}
