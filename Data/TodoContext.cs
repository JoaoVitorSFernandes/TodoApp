using Microsoft.EntityFrameworkCore;

namespace TodoApp.Data
{
    public class TodoContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=TodoApp.db;Cache=Shared");
        }
    }
}