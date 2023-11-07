using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Extensions;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("v1/subtask")]
    public sealed class SubTaskController : ControllerBase
    {
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] TodoContext context)
        {
            try
            {
                var subTask = await context
                                    .SubTodos
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if (subTask == null)
                    return NotFound(new ResultViewModel<Todos>("02XE3 - Unable to find task in database"));

                return Ok(new ResultViewModel<SubTodo>(subTask));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<SubTodo>("03XE1 - Internal Server Failure."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<SubTodo>("03XE2 - Internal Server Failure."));
            }
        }

        [HttpPost("setmaintask/{id:int}")]
        public async Task<IActionResult> SetMainTaskAsync(
            [FromRoute] int id,
            [FromServices] TodoContext context)
        {
            try
            {
                var subTask = await context
                    .SubTodos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (subTask == null)
                    return NotFound(new ResultViewModel<Todos>("02XE3 - Unable to find task in database"));

                Todo todo = new Todo
                {
                    Title = subTask.Title,
                    Description = subTask.Description,
                    Status = subTask.Status,
                    Favorite = subTask.Favorite,
                    Date = subTask.Date
                };

                context.SubTodos.Remove(subTask);
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();

                return Created($"v1/task/{todo.Id}", new ResultViewModel<Todo>(todo));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE3 - Internal Server Failure."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE4 - Internal Server Failure."));
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
                    return BadRequest(new ResultViewModel<SubTodo>(ModelState.GetErros()));

                var subTodo = new SubTodo
                {
                    TodoId = model.ListTasksId,
                    Title = model.Title,
                    Description = model.Description,
                    Date = model.Date
                };

                await context.SubTodos.AddAsync(subTodo);
                await context.SaveChangesAsync();

                return Created($"v1/subtask/{subTodo.Id}", new ResultViewModel<SubTodo>(subTodo));
            }
            catch (DbUpdateException ex)
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
                    return NotFound(new ResultViewModel<SubTodo>("02XE8 - Unable to update this task. Inform one task valid."));

                subTask.Title = model.Title;
                subTask.Description = model.Description;
                subTask.Date = model.Date;

                context.SubTodos.Update(subTask);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<SubTodo>(subTask));
            }
            catch (DbUpdateException ex)
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
                var subTask = await context
                                .SubTodos
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == id);

                if (subTask == null)
                    return NotFound(new ResultViewModel<Todos>("02XE8 - Unable to update this task. Inform one task valid."));

                subTask.Status = true;

                context.SubTodos.Update(subTask);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<SubTodo>(subTask));
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
                var subTask = await context
                                .SubTodos
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == id);

                if (subTask == null)
                    return NotFound(new ResultViewModel<Todos>("02XE8 - Unable to update this task. Inform one task valid."));

                subTask.Favorite = !subTask.Favorite;

                context.SubTodos.Update(subTask);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<SubTodo>(subTask));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<SubTodo>("03XE5 - Internal Server Failure."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<SubTodo>("03XE6 - Internal Server Failure."));
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] TodoContext context)
        {
            try
            {
                var subTask = await context
                                    .SubTodos
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if (subTask == null)
                    return BadRequest("02XE14 - Unable to delete this task. Inform one taks valid.");

                context.SubTodos.Remove(subTask);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<dynamic>(new { message = "Task list deleted successfully." }));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<SubTodo>("03XE7 - Internal Server Failure."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<SubTodo>("03XE8 - Internal Server Failure."));
            }
        }

    }
}