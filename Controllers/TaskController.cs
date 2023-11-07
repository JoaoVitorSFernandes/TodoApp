using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Extensions;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("v1/task")]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync(
            [FromServices] TodoContext context)
        {
            try
            {
                var task = await context
                                    .Todos
                                    .AsNoTracking()
                                    .Include(x => x.SubTodo)
                                    .ToListAsync();

                return Ok(new ResultViewModel<List<Todo>>(task));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE1 - Internal Server Failure."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE2 - Internal Server Failure."));
            }
        }

        [HttpGet("favorite")]
        public async Task<IActionResult> GetAllFavoritesAsync(
            [FromServices] TodoContext context)
        {
            try
            {
                var task = await context
                                    .Todos
                                    .AsNoTracking()
                                    .Where(x => x.Favorite == true)
                                    .ToListAsync();

                return Ok(new ResultViewModel<List<Todo>>(task));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE1 - Internal Server Failure."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE2 - Internal Server Failure."));
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] TodoContext context)
        {
            try
            {
                var task = await context
                                    .Todos
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if (task == null)
                    return NotFound(new ResultViewModel<Todos>("02XE3 - Unable to find task in database"));

                return Ok(new ResultViewModel<Todo>(task));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE1 - Internal Server Failure."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE2 - Internal Server Failure."));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorTaskViewModel model,
            [FromServices] TodoContext context)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<Todo>(ModelState.GetErros()));

                var todo = new Todo
                {
                    ListTasksId = model.ListOrTasksId,
                    Title = model.Title,
                    Description = model.Description,
                    Date = model.Date
                };

                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();

                return Created($"v1/task/{todo.Id}", new ResultViewModel<Todo>(todo));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE3 - Internal Server Failure."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE4 - Internal Server Failure."));
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] EditorTaskViewModel model,
            [FromServices] TodoContext context)
        {
            try
            {
                if (!ModelState.IsValid)
                    return NotFound(new ResultViewModel<Todos>(ModelState.GetErros()));

                var subTask = await context
                                .SubTodos
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == id);

                if (subTask == null)
                    return NotFound(new ResultViewModel<Todos>("02XE8 - Unable to update this sub task. Inform one task valid."));

                subTask.Title = model.Title;
                subTask.Description = model.Description;
                subTask.Date = model.Date;

                context.SubTodos.Update(subTask);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<SubTodo>(subTask));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE5 - Internal Server Failure."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE6 - Internal Server Failure."));
            }
        }

        [HttpPut("status/{id:int}")]
        public async Task<IActionResult> AlterStatusAsync(
            [FromRoute] int id,
            [FromServices] TodoContext context)
        {
            try
            {
                var task = await context
                                .Todos
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == id);

                if (task == null)
                    return NotFound(new ResultViewModel<Todos>("02XE8 - Unable to update this task. Inform one task valid."));

                task.Status = !task.Status;

                context.Todos.Update(task);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Todo>(task));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<SubTodo>("03XE5 - Internal Server Failure."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<SubTodo>("03XE6 - Internal Server Failure."));
            }
        }

        [HttpPut("favorite/{id:int}")]
        public async Task<IActionResult> AlterFavoriteAsync(
            [FromRoute] int id,
            [FromServices] TodoContext context)
        {
            try
            {
                var task = await context
                                .Todos
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == id);

                if (task == null)
                    return NotFound(new ResultViewModel<Todos>("02XE8 - Unable to update this task. Inform one task valid."));

                task.Favorite = !task.Favorite;

                context.Todos.Update(task);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Todo>(task));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE5 - Internal Server Failure."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE6 - Internal Server Failure."));
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] TodoContext context)
        {
            try
            {
                var task = await context
                                    .Todos
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if (task == null)
                    return BadRequest("02XE14 - Unable to delete this task. Inform one taks valid.");

                context.Todos.Remove(task);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<dynamic>(new { message = "Task list deleted successfully." }));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE7 - Internal Server Failure."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE8 - Internal Server Failure."));
            }
        }

        [HttpDelete("delcompletedtasks")]
        public async Task<IActionResult> DeleteAllCompletedTasksAsync(
            [FromServices] TodoContext context)
        {
            try
            {
                var tasksCompleted = await context
                                            .Todos
                                            .AsNoTracking()
                                            .Where(x => x.Status == true)
                                            .ToListAsync();

                if (tasksCompleted == null)
                    return NotFound(new ResultViewModel<Todos>("02XE11 - Unable to find completed task in database"));

                context.Todos.RemoveRange(tasksCompleted);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<dynamic>(new { message = "Tasks completed deleted successfully." }));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Todos>("02XE12 - Internal Server Failure."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Todos>("02XE13 - Internal Server Failure."));
            }
        }

    }
}