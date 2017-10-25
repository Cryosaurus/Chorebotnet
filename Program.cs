/*
 * A port of my existing Chorebot discord bot to C#
 * Chorebotnet is based on the DSharpPlus library
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using System.Threading;
using DSharpPlus.Entities;

namespace Chorebotnet{
    class Chorecore{

        private static System.Threading.Timer timer;
        static DiscordClient discord;
        static CommandsNextModule commands;
        static DiscordChannel choreChannel;

        static void Main(string[] args){
            //Start daily reminders
            StartReminder(new TimeSpan(19, 00, 00), new TimeSpan(24, 00, 00));

            //Actually start the Chorebotnet process
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args) {
            ulong choreChannelID = 0;
            ulong.TryParse(args[1], out choreChannelID);

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
            choreChannel = await discord.GetChannelAsync(choreChannelID);

            //Wait forever, all work should be done before this point
            await Task.Delay(-1);
        }

        private static void StartReminder(TimeSpan alertTime, TimeSpan interval) {

            //Avoid time zone shenanigans and convert the server local time to the MST zone we live in
            DateTime serverUTC = DateTime.Now.ToUniversalTime();
            TimeZoneInfo localZone = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(serverUTC, localZone);

            //Determine how long until the next alert should go off
            TimeSpan timeToGo = alertTime - localTime.TimeOfDay;
            if (timeToGo < TimeSpan.Zero) {
                timeToGo = timeToGo + interval;
            }

            timer = new System.Threading.Timer(x => 
            {
                DailyReminder();
            }, null, timeToGo, interval);
        }

        static async void DailyReminder() {
       
            //Avoid time zone shenanigans and convert the server local time to the MST zone we live in
            DateTime serverUTC = DateTime.Now.ToUniversalTime();
            TimeZoneInfo localZone = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(serverUTC, localZone);

            if (localTime.DayOfWeek == DayOfWeek.Thursday) {
                await discord.SendMessageAsync(choreChannel, "It's Time to take out the garbage");
            }
        }
    }
}
