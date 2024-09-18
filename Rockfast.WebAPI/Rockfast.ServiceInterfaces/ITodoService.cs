using Rockfast.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockfast.ServiceInterfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoVM>> GetAllTodosAsync();
        Task<IEnumerable<TodoVM>> GetAllTodosByUserAsync(int userId);
        Task<TodoVM> GetTodoAsync(int id);
        Task<TodoVM> CreateTodoAsync(TodoVM todoVM);
        Task<TodoVM> UpdateTodoAsync(int id, TodoVM todoVM);
        Task<bool> DeleteTodoAsync(int id);
    }
}
