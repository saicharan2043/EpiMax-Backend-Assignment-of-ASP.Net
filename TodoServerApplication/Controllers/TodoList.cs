using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoServerApplication.Data;
using TodoServerApplication.Models;

namespace TodoServerApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListController : ControllerBase
    {
        private readonly TodoListDbContext _todoListDbContext;

        public TodoListController(TodoListDbContext todoListDbContext)
        {
            _todoListDbContext = todoListDbContext;
        }

        // Get all Todo List
        [HttpGet("GetAllData")]
        public async Task<IActionResult> GetAllData()
        {
            try
            {
                var alldata = await _todoListDbContext.Todos.ToListAsync();
                return Ok(alldata);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get specific TodoItem
        [HttpGet("GetSpecificTodoItem/{id}")]
        public async Task<IActionResult> GetSpecificTodoItem(Guid id)
        {
            

            try
            {
                var todos = await _todoListDbContext.Todos.FindAsync(id);
                if (todos == null)
                {
                    return NotFound($"Todo item not found with id {id}");
                }
                return Ok(todos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Add New Todo
        [HttpPost("PostNewItem")]
        public async Task<IActionResult> PostNewItem(TodoItem todo)
        {
            try
            {
                _todoListDbContext.Todos.Add(todo);
                await _todoListDbContext.SaveChangesAsync();
                return Ok("Successfully inserted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update Existing Todo
        [HttpPut("UpdateTodo")]
        public async Task<IActionResult> UpdateTodo(TodoItem todo)
        {
            if (todo == null)
            {
                return BadRequest("Todo data is invalid");
            }

            try
            {
                var result = await _todoListDbContext.Todos.FindAsync(todo.id);
                if (result == null)
                {
                    return NotFound($"Todo item not found with Id {todo.id}");
                }

                result.title = todo.title;
                result.description = todo.description;
                result.category = todo.category;
                result.status = todo.status;

                await _todoListDbContext.SaveChangesAsync();
                return Ok("Todo item updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Remove Todo Item
        [HttpDelete("DeleteTodoItem/{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {

            try
            {
                var result = await _todoListDbContext.Todos.FindAsync(id);
                if (result == null)
                {
                    return NotFound($"Todo item not found with Id {id}");
                }

                _todoListDbContext.Todos.Remove(result);
                await _todoListDbContext.SaveChangesAsync();
                return Ok("Todo item removed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //change the status
        [HttpPatch("UpdateIsChecked/{id}")]
        public async Task<IActionResult> UpdateIsChecked(Guid id, [FromBody] UpdateStatusModel model)
        {
            try
            {
                var item = await _todoListDbContext.Todos.FindAsync(id);

                if (item == null)
                {
                    return NotFound();
                }

                item.status = model.IsChecked;
                await _todoListDbContext.SaveChangesAsync();
                return Ok("Successfully inserted");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


    }
}