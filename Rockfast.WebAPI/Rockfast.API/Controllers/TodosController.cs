using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockfast.ServiceInterfaces;
using Rockfast.ViewModels;

namespace Rockfast.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private ILogger<TodosController> _logger;
        public TodosController(ITodoService todoService, ILogger<TodosController> logger)
        {
            this._todoService = todoService;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<TodoVM>> GetAll()
        {
            try
            {
                return await _todoService.GetAllTodosAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Enumerable.Empty<TodoVM>();
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IEnumerable<TodoVM>> GetAllByUser(int userId)
        {
            try
            {
                return await _todoService.GetAllTodosByUserAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Enumerable.Empty<TodoVM>();
            }
        }

        [HttpGet("{id}")]
        public async Task<TodoVM> Get(int id)
        {
            try
            {
                var todo = await _todoService.GetTodoAsync(id);
                if (todo == null)
                {
                    throw new KeyNotFoundException($"Todo with ID {id} not found.");
                }
                return todo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }


        [HttpPost]
        public async Task<TodoVM> Post(TodoVM model)
        {
            try
            {
                return await _todoService.CreateTodoAsync(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        [HttpPut]
        public async Task<TodoVM> Put(TodoVM model)
        {
            try
            {
                var updateTodo = await _todoService.UpdateTodoAsync(model.Id, model);
                if(updateTodo == null)
                {
                    throw new KeyNotFoundException($"Todo with ID {model.Id} not found.");
                }
                return updateTodo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }

        }
        [HttpDelete]
        public async Task Delete(int id)
        {
            try
            {
                var result = await _todoService.DeleteTodoAsync(id);
                if (!result)
                {
                    throw new KeyNotFoundException($"Todo with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
