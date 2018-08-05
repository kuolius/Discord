using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBOT.modules
{
    public class RegisterMatch : ModuleBase<SocketCommandContext>
    {

        public string text="";

        [Command("regmatch")]
        public async Task RegisterMatchAsync(string matchId)
        {
            var isNumeric = int.TryParse(matchId, out int n);
            if (!isNumeric) return;

          
            

          /*  if (!text.Contains("Match:"))
            {
                await ReplyAsync("Match doesn't exist");
                return;
            }*/
            

            await ReplyAsync($"#{matchId} match registered! ");
        }





       
    }
}
