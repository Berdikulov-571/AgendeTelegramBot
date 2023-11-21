using Microsoft.EntityFrameworkCore;
using ToDo.Entities;

namespace ToDo.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;DataBase=ToDo;Trusted_Connection=true;");
        }

        public virtual DbSet<ToDos> ToDo { get; set; }
        public virtual DbSet<User > Users { get; set; }
    }
}
