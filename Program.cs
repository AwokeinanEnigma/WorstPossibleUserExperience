using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace WorstPossibleUserExperience
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string token = "MYBOTTOKENHERE";

            _client.Log += _client_Log;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, token);

            await _client.StartAsync();

            await Task.Delay(-1);

        }

        private Task _client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }
            
        public async Task RegisterCommandsAsync()
        {

            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);    
        }

        public List<string> duh = new List<string>();
        public bool firstTimeFill = false;
        private async Task SendAMessage(SocketMessage arg, SocketUserMessage message, SocketCommandContext context) {
            if (firstTimeFill == false)
            {
                foreach (string line in System.IO.File.ReadLines(KnownFolders.GetPath(KnownFolders.KnownFolder.Documents) + "\\DiscordMessages.txt"))
                {
                    duh.Add(line);
                    firstTimeFill = true;
                }
            }


            int argPos = 0;
            if (message.HasStringPrefix("__", ref argPos))
            {
                duh.Add($"Content: {message.Content} -- Author: { message.Author} -- Time: {message.Timestamp.DateTime.ToLocalTime() }");
                Console.WriteLine($"Content: {message.Content} -- Author: { message.Author}");
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
                if (result.Error.Equals(CommandError.UnmetPrecondition)) await message.Channel.SendMessageAsync(result.ErrorReason);
            }

            TextWriter tw = new StreamWriter(KnownFolders.GetPath(KnownFolders.KnownFolder.Documents) + "\\DiscordMessages.txt");

            foreach (string s in duh)
                tw.WriteLine(s);

            tw.Close();
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;

            if (message.Channel is IPrivateChannel) {
                await SendAMessage(arg, message, context);
            }

        }
    }
}
