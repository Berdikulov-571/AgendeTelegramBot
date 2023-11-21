using Microsoft.EntityFrameworkCore;
using ToDo.DataContext;
using ToDo.DTOs;
using ToDo.Entities;

namespace ToDo.Services
{
    public class UserService
    {
        public static async ValueTask AddAsync(UserDto model)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var check = await context.Users.FirstOrDefaultAsync(x => x.UserId == model.UserId);

            if(check == null)
            {
                User user = new User();
                user.UserId = model.UserId;
                user.firstName = model.firstName;

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }

        }
    }
}