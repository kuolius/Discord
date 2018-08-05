using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBOT.modules
{
    public class Hi : ModuleBase<SocketCommandContext>
    {
        [Command("Hi")]
        public async Task HiAsync()
        {
            await ReplyAsync("Hello, there!");
        }
    }
}
