using Discord;
using Discord.Commands;
using Discord.Net.Providers.WS4Net;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordBOT
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();
       


        private DiscordSocketClient _client;
        private CommandService _command;
        private IServiceProvider _services;
        private string text;
        

       

        public async Task RunBotAsync()
        {

            _client = new DiscordSocketClient(new DiscordSocketConfig() {
                LogLevel = LogSeverity.Verbose,
                WebSocketProvider = WS4NetProvider.Instance
            });

           
            _command = new CommandService();
            _services = new ServiceCollection().AddSingleton(_client).AddSingleton(_command).BuildServiceProvider();

            string botToken = "NDczNzU3ODQ5OTQwMzk0MDA0.DkUANg.ghq7v3CznVoHPlJD3SeAE5ZWTOo";

            

            _client.Log += Log;

          

            await RegisterCommandAsync();

            await _client.LoginAsync(TokenType.Bot, botToken);

            await _client.StartAsync();

            await Task.Delay(-1);

        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.CompletedTask;
        }

        public async Task RegisterCommandAsync()
        {

            _client.MessageReceived += HandleCommandAsync;
            await _command.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot) return;

            int argPos = 0;

            if(message.HasStringPrefix("!",ref argPos) || message.HasMentionPrefix(_client.CurrentUser,ref argPos))
            {

                var context = new SocketCommandContext(_client, message);

                

                var result = await _command.ExecuteAsync(context, argPos, _services);


                if (context.Message.Content.Contains("!regmatch"))
                {
                    await FindMatchAsync(context);
                }

                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);



            }
        }


        private async Task FindMatchAsync(SocketCommandContext context)
        {

            string matchId = context.Message.Content.Substring(10, context.Message.Content.Length - 10);

            var channel = ((ISocketMessageChannel)_client.GetChannel(473739181063929861));

            await GetTextFromUrl(matchId);


            await Task.Delay(1000);

            /* if (!text.Contains("Match:"))
             {
                 await channel.SendMessageAsync("Match doesn't exist");
                 return;
             }

             */

            await channel.SendMessageAsync($"Match {matchId} was successfully added to the database!");


        }

        


        private async Task GetTextFromUrl(string matchid)
        {
            string url = "https://archive.smitegame.com/match-details/?match=" + matchid;

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent content = response.Content;
            text = await content.ReadAsStringAsync();

        }
    }
}
