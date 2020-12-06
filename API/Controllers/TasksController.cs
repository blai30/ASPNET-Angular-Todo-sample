using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private TasksDbContext _context;

        public TasksController(TasksDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ToDoTask>>> GetTasks()
        {
            var data = await _context.ToDoTasks.ToListAsync();
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTask(ToDoTask toDoTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model not matching requirements");
            }

            await _context.ToDoTasks.AddAsync(toDoTask);
            await _context.SaveChangesAsync();
            return Ok("Successfully created a task.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            var taskToDelete = await _context.ToDoTasks.SingleOrDefaultAsync(x => x.Id == id);
            if (taskToDelete == null)
            {
                return BadRequest("Task not found.");
            }

            _context.Remove(taskToDelete);
            await _context.SaveChangesAsync();
            return Ok("Successfully deleted task.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(int id, ToDoTask newToDoTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model not matching requirements");
            }

            newToDoTask.Id = id;
            _context.Entry(newToDoTask).State = EntityState.Modified;
            var taskToUpdate = await _context.ToDoTasks.FindAsync(id);

            if (taskToUpdate == null)
            {
                return NotFound("Task not found.");
            }

            _context.Update(taskToUpdate);
            await _context.SaveChangesAsync();
            return Ok("Task updated.");
        }
    }
}
