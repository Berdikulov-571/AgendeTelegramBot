using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Args;
using ToDo.Entities;
using ToDo.Services;

namespace ToDo
{
    internal class Program
    {
        public static ITelegramBotClient? botClient;
        static async Task Main(string[] args)
        {

            botClient = new TelegramBotClient("");

            botClient.OnMessage += Bot_OnMessage;

            botClient.StartReceiving();

            await new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                // Register your background service
                services.AddHostedService<BackGroundServiceTest>();
            }).RunConsoleAsync();

            Console.ReadLine();

        }

        private static async void Bot_OnMessage(object? sender, MessageEventArgs e)
        {
            var text = e.Message.Text;

            if (text == "/start")
            {
                await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Welcome");

                await UserService.AddAsync(new DTOs.UserDto()
                {
                    firstName = e.Message.Chat.FirstName,
                    UserId = e.Message.Chat.Id
                });
            }
            else if (text.Contains("/new="))
            {
                string description = text.Split('=')[1];

                await ToDoService.AddAsync(description, e.Message.Chat.Id);

                await botClient.SendTextMessageAsync(e.Message.Chat.Id, "ToDo qo'shildi",replyToMessageId:e.Message.MessageId);

            }
            else if (text == "/active")
            {
                IEnumerable<ToDos> todos = await ToDoService.GetAllAsync();

                if(todos.Count() != 0)
                {
                    foreach (var i in todos)
                    {
                        try
                        {
                            await botClient.SendTextMessageAsync(i.UserId, i.Description);
                        }
                        catch 
                        {

                        }
                    }
                }
                else
                {
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Sizda hali ToDo mavjud emas", replyToMessageId: e.Message.MessageId);
                }

            }
            else if (text == "/archive")
            {
                IEnumerable<ToDos> todos = await ToDoService.Archive();

                if(todos.Count() != 0)
                {
                    foreach (var i in todos)
                    {
                        try
                        {
                            await botClient.SendTextMessageAsync(i.UserId, i.Description);
                        }
                        catch
                        {

                        }
                    }
                }
                else
                {
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Hali archive mavjud emas", replyToMessageId: e.Message.MessageId);
                }

            }
            else
            {
                await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Yangi ToDo qo'shish uchun /new=");
            }

        }
    }
}