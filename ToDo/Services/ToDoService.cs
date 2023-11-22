using Microsoft.EntityFrameworkCore;
using ToDo.DataContext;
using ToDo.Entities;

namespace ToDo.Services
{
    public class ToDoService
    {
        public static async ValueTask AddAsync(string description,long UserId)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            ToDos toDo = new ToDos();
            toDo.Description = description;
            toDo.Date = DateTime.Now;
            toDo.UserId = UserId;

            await context.ToDo.AddAsync(toDo);
            await context.SaveChangesAsync();
        }

        public static async ValueTask<IEnumerable<ToDos>> GetAllAsync()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            IEnumerable<ToDos> todos = await context.ToDo.Where(x => x.Date.DayOfYear - DateTime.Now.DayOfYear > -3).ToListAsync();

            return todos;
        }
        public static async ValueTask<IEnumerable<ToDos>> Archive()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            IEnumerable<ToDos> todos = await context.ToDo.Where(x => x.Date.DayOfYear - DateTime.Now.DayOfYear < -3).ToListAsync();

            return todos;
        }
    }
}