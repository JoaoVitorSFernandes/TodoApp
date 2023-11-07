using Aspose.Pdf;
using Aspose.Pdf.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TodoApp.Data;
using TodoApp.Extensions;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("v1/tasklist")]
    public sealed class TaskListController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync(
            [FromServices] TodoContext context)
        {
            try
            {
                var listTasks = await context
                                        .ListTodos
                                        .AsNoTracking()
                                        .ToListAsync();

                return Ok(new ResultViewModel<List<Todos>>(listTasks));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Todos>("02XE1 - Internal Server Failure"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Todos>("02XE2 - Internal Server Failure"));
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] TodoContext context)
        {
            try
            {
                var listTask = await context
                                    .ListTodos
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if (listTask == null)
                    return NotFound(new ResultViewModel<Todos>("02XE3 - Unable to find task in database"));

                return Ok(new ResultViewModel<Todos>(listTask));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Todos>("02XE4 - Internal Server Failure"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Todos>("02XE5 - Internal Server Failure"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorTasksViewModels model,
            [FromServices] TodoContext context)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<Todos>(ModelState.GetErros()));

                var listTask = new Todos
                {
                    Id = 0,
                    Title = model.Title,
                    Tasks = { }
                };

                await context.ListTodos.AddAsync(listTask);
                await context.SaveChangesAsync();

                return Created($"v1/tasks/{listTask.Id}", new ResultViewModel<Todos>(listTask));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Todos>("02XE6 - Internal Server Failure"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Todos>("02XE7 - Internal Server Failure"));
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
                if (!ModelState.IsValid)
                    return NotFound(new ResultViewModel<Todos>(ModelState.GetErros()));

                var listTask = await context
                                .ListTodos
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == id);

                if (listTask == null)
                    return NotFound(new ResultViewModel<Todos>("02XE8 - Unable to update this task list. Inform one task list valid."));

                listTask.Title = model.Title;

                context.ListTodos.Update(listTask);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Todos>(listTask));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Todos>("02XE9 - Internal Server Failure."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Todos>("02XE10 - Internal Server Failure."));
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] TodoContext context)
        {
            try
            {
                var listTask = await context
                                    .ListTodos
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == id);

                if (listTask == null)
                    return BadRequest("02XE14 - Unable to delete this task list. Inform one taks list valid.");

                context.ListTodos.Remove(listTask);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<dynamic>(new { message = "Task list deleted successfully." }));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "02XE15 - Internal Server Failure.");
            }
            catch
            {
                return StatusCode(500, "02XE16 - Internal Server Failure.");
            }
        }

        [HttpGet("pdf")]
        public async Task<IActionResult> GetPdfAsync(
            [FromServices] TodoContext context)
        {
            try
            {
                var listTasks = await context
                                        .ListTodos
                                        .AsNoTracking()
                                        .ToListAsync();

                var document = new Document();
                var page = document.Pages.Add();

                TextFragment text = new TextFragment();
                StringBuilder taskList = new StringBuilder();

                foreach (var list in listTasks)
                {
                    taskList.Append($"- {list.Title}\n");
                    foreach (var item in list.Tasks)
                        taskList.Append($"\n  -> {item.Title}");
                }

                text.Text = taskList.ToString();
                page.Paragraphs.Add(text);

                MemoryStream streamPDF = new MemoryStream();
                document.Save(streamPDF);

                return Ok(new ResultViewModel<dynamic>(streamPDF));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Todos>("02XE17 - Internal Server Failure."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Todos>("02XE18 - Internal Server Failure."));
            }
        }

    }
}