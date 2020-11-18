using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using color_bot.Controllers;


namespace color_bot
{
    class Program
    {
        private readonly string botToken = "color_bot_token";

        private readonly DiscordSocketClient _client;
        private CommandParser commandParser = new CommandParser();
        private LinkCreator linkCreator = new LinkCreator();

        public Program()
        {
            // It is recommended to Dispose of a client when you are finished
            // using it, at the end of your app's lifetime.
            _client = new DiscordSocketClient();

            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;
            _client.MessageReceived += MessageReceivedAsync;
        }

        // There is no need to implement IDisposable like before as we are
        // using dependency injection, which handles calling Dispose for us.
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            // Tokens should be considered secret data, and never hard-coded.
            // Visual Studio may need to be restarted after setting the env var, took me a bit to figure that one out :)
            await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable(botToken));
            await _client.StartAsync();

            // Block the program until it is closed.
            await Task.Delay(Timeout.Infinite);
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());

            return Task.CompletedTask;
        }

        // The Ready event indicates that the client has opened a
        // connection and it is now safe to access the cache.
        private Task ReadyAsync()
        {
            Console.WriteLine($"{_client.CurrentUser} is connected!");

            return Task.CompletedTask;
        }

        // This is not the recommended way to write a bot - consider
        // reading over the Commands Framework sample.
        private async Task MessageReceivedAsync(SocketMessage message)
        {
            // The bot should never respond to itself.
            if (message.Author.Id == _client.CurrentUser.Id)
            {
                return;
            }

            if (commandParser.IsValidColor(message.Content))
            {
                await message.Channel.SendMessageAsync("<@!189598222187102209>\n" + linkCreator.CreateLink(message.Content));
            }
        }
    }
}
