using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Chorebotnet {
    public class Chorecommand {

        [Command("commands")]
        public async Task Commands(CommandContext context) {
            await context.RespondAsync($"I currently do nothing");
        }
    }
}
