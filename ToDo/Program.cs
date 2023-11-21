using Telegram.Bot;
using Telegram.Bot.Args;
using ToDo.Services;

namespace ToDo
{
    internal class Program
    {
        public static ITelegramBotClient? botClient;
        static void Main(string[] args)
        {
            botClient = new TelegramBotClient("6534651577:AAH84sVjRAYA6lEqyF45NjbbhiAvGLyxu80");

            botClient.OnMessage += Bot_OnMessage;

            botClient.StartReceiving();

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
        }
    }
}