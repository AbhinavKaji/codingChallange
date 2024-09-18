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
    public class TodoService : ITodoService
    {
        private readonly ApiDbContext _database;
        private readonly IUserService _userService;
        public TodoService(ApiDbContext db, IUserService userService)
        {
            this._database = db;
            this._userService = userService;
        }

        public async Task<TodoVM> CreateTodoAsync(TodoVM todoVM)
        {
            try
            {
                var user = await _userService.GetUserAsync(todoVM.UserId);
                if (user == null)
                {
                    throw new KeyNotFoundException($"user with id {todoVM.UserId} not found");
                }
                var todo = new Todo
                {
                    Name = todoVM.Name,
                    Complete = todoVM.Complete,
                    DateCompleted = DateTime.Parse(todoVM.DateCompleted),
                    DateCreated = todoVM.DateCreated,
                    UserId = todoVM.UserId,
                };

                _database.Todos.Add(todo);
                await _database.SaveChangesAsync();

                todoVM.Id = todo.Id;
                return todoVM;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating new todo.", ex);
            }
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            try
            {
                var todo = await _database.Todos.FindAsync(id);
                if (todo == null) return false;

                _database.Todos.Remove(todo);
                await _database.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting todo with ID {id}.", ex);
            }
        }

        public async Task<IEnumerable<TodoVM>> GetAllTodosAsync()
        {
            try
            {
                var todos = await _database.Todos.ToListAsync();
                return todos.Select(x => new TodoVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Complete = x.Complete,
                    DateCompleted = x.DateCompleted?.ToString("yyyy-MM-dd"),
                    DateCreated = x.DateCreated,
                    UserId = x.UserId,
                });
            }
            catch (Exception ex)
            {
                throw new Exception("error retriving all todos. ", ex);
            }
        }

        public async Task<IEnumerable<TodoVM>> GetAllTodosByUserAsync(int userId)
        {
            try
            {
                var todos = await _database.Todos.Where(x => x.UserId == userId).ToListAsync();
                return todos.Select(x => new TodoVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Complete = x.Complete,
                    DateCompleted = x.DateCompleted?.ToString("yyyy-MM-dd"),
                    DateCreated = x.DateCreated,
                    UserId = x.UserId,
                });
            }
            catch (Exception ex)
            {
                throw new Exception("error retriving all todos. ", ex);
            }
        }

        public async Task<TodoVM> GetTodoAsync(int id)
        {
            try
            {
                var todo = await _database.Todos.FindAsync(id);
                if (todo == null) return null;
                return new TodoVM
                {
                    Id = todo.Id,
                    Name = todo.Name,
                    Complete = todo.Complete,
                    DateCompleted = todo.DateCompleted?.ToString("yyyy-MM-dd"),
                    DateCreated = todo.DateCreated,
                    UserId = todo.UserId,
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving todo with ID {id}. ", ex);
            }
        }

        public async Task<TodoVM> UpdateTodoAsync(int id, TodoVM todoVM)
        {
            try
            {
                var user = await _userService.GetUserAsync(todoVM.UserId);
                if (user == null)
                {
                    throw new KeyNotFoundException($"user with id {todoVM.UserId} not found");
                }
                var todo = await _database.Todos.FindAsync(id);
                if (todo == null) return null;

                todo.Name = todoVM.Name;
                todo.DateCreated = todoVM.DateCreated;
                todo.DateCompleted = DateTime.Parse(todoVM.DateCompleted);
                todo.Complete = todoVM.Complete;
                todo.UserId = todoVM.UserId;

                await _database.SaveChangesAsync();

                return todoVM;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating todo with ID {id}.", ex);
            }
        }
    }
}
