using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace Chorebotnet{
    class Chorecore{

        static DiscordClient discord;
        static CommandsNextModule commands;

        static void Main(string[] args){
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args) {
            discord = new DiscordClient(new DiscordConfiguration {
                Token = args[0],
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });

            commands = discord.UseCommandsNext(new CommandsNextConfiguration {
                StringPrefix = "!"
            });

            commands.RegisterCommands<Chorecommand>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
