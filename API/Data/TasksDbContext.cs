using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class TasksDbContext : DbContext
    {
        public DbSet<ToDoTask> ToDoTasks { get; set; }

        public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options)
        {

        }
    }
}
