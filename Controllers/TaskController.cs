using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("v1/tasks")]
    public sealed class TaskController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync(
            [FromServices] TodoContext context)
        {
            var ListTasks = context
                        .ListTodos
                        .AsNoTracking()
                        .Select(x => x.Title)
                        .ToList();

            return Ok(ListTasks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] TodoContext context)
        {
            var tasks = context
                            .ListTodos
                            .AsNoTracking()
                            .FirstOrDefault(x => x.Id == id);

            if (tasks == null)
                return BadRequest("02XE2 - Tarefa não encontrada em nosso banco");

            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorTasksViewModels model,
            [FromServices] TodoContext context)
        {
            try
            {
                var todos = new Todos
                {
                    Id = 0,
                    Title = model.Title,
                    Tasks = { }
                };

                await context.ListTodos.AddAsync(todos);
                await context.SaveChangesAsync();

                return Created($"v1/tasks/{todos.Id}", todos);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "02XE3 - Não foi possivel criar essa lista de tarefas");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "02XE4 - Não foi possivel criar essa lista de tarefas");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] EditorTasksViewModels model,
            [FromServices] TodoContext context)
        {
            try
            {
                var todos = await context
                                .ListTodos
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == id);

                if (todos == null)
                    return StatusCode(500, "02XE4 - Não foi possivel atualizar essa lista de tarefas");

                todos.Title = model.Title;

                context.ListTodos.Update(todos);
                await context.SaveChangesAsync();

                return Ok(todos);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "02XE5 - Não foi possivel atualizar essa lista de tarefas");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "02XE6 - Não foi possivel atualizar essa lista de tarefas");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] TodoContext context)
        {
            try
            {
                var todos = await context
                                    .ListTodos
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if(todos == null)
                    return BadRequest("02XE7 - Não foi possivel deletar essa lista de tarefas");
            
                context.ListTodos.Remove(todos);
                await context.SaveChangesAsync();

                return Ok(new {message = "Registro deletado com sucesso"});
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "02XE8 - Não foi possivel deletar essa lista de tarefas");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "02XE9 - Não foi possivel deletar essa lista de tarefas");
            }
        }
    }
}