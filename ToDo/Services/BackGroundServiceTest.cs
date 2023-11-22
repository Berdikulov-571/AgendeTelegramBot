using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using ToDo.Entities;

namespace ToDo.Services
{
    public class BackGroundServiceTest : BackgroundService
    {
        private readonly ITelegramBotClient? _botClient;

        public BackGroundServiceTest()
        {
            _botClient = new TelegramBotClient("");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _botClient.StartReceiving();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (DateTime.Now.Hour == 8 && DateTime.Now.Minute == 00 && DateTime.Now.Second == 0 || DateTime.Now.Hour == 12 && DateTime.Now.Minute == 00 && DateTime.Now.Second == 0 || DateTime.Now.Hour == 18 && DateTime.Now.Minute == 00 && DateTime.Now.Second == 0)
                {
                    IEnumerable<ToDos> todos = await ToDoService.GetAllAsync();

                    if (todos.Count() != 0)
                    {
                        foreach (var i in todos)
                        {
                            try
                            {
                                await _botClient.SendTextMessageAsync(i.UserId, i.Description);
                            }
                            catch (Exception ex)
                            {
                                await Console.Out.WriteLineAsync(ex.Message);
                            }
                        }
                    }
                }
            }
        }
    }
}

