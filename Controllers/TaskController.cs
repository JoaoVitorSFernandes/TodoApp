using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;
using TodoApp.ViewModels;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("v1/task")]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE1 - Internal Server Failure."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE2 - Internal Server Failure."));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync()
        {
            try
            {

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

        [HttpPut]
        public async Task<IActionResult> PutAsync()
        {
            try
            {

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

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            try
            {

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE7 - Internal Server Failure."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Todo>("03XE8 - Internal Server Failure."));
            }
        }
    
    }
}