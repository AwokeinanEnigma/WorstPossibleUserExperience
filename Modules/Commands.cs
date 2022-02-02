using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace WorstPossibleUserExperience.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("send")]
        public async Task SND([Remainder] string reason = null)
        {
            //CHANGE THESE
            var ch = Context.Client.GetGuild(000).GetTextChannel(999);
            string filtered = FilterRetardation(reason);
            var bytes = new byte[3];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            string hash1 = BitConverter.ToString(bytes);

            var EmbedBuilderLog = new EmbedBuilder()
       .WithDescription($"Anonymous Message: {filtered} -- HASH: {hash1}")
       .WithImageUrl("https://i.imgur.com/hi57MqN.gif")
       .WithFooter(footer =>
       {
           footer
           .WithIconUrl("https://i.imgur.com/hi57MqN.gif");
       });
            var built = EmbedBuilderLog.Build();
            await ch.SendMessageAsync(embed: built);
        }
            
        public string FilterRetardation(string msg) {
            //...ugh

            msg = Regex.Replace(msg, "nigger", "------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "n1gg3r", "------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "nigg3r", "------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "n1gger", "------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "tranny", "------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "tr4nny", "------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "faggot", "------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "f4gg0t", "------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "fag", "---", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "f4g", "---", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "kike", "----", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "k1ke", "----", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "trannie", "-------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "tr4nni3", "-------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "you will never be a real woman", "( i have no bitches or social life )", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "you will never be a real man", "( i have no bitches or social life )", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "AMBER YOU WILL NEVER BE A REAL WOMAN NO ONE WILL EVER LOVE YOU", "( i have no bitches or social life )", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "AMBER YOU WILL NEVER BE A REAL WOMAN NO ONE WILL EVER LOVE YOU", "( i should kill myself now )", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "real man", "==(POSTER IS A BITCH)==", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "real woman", "==(POSTER IS A BITCH)==", RegexOptions.IgnoreCase);

            msg = Regex.Replace(msg, "n.igger", "-------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "n.i.gger", "-------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "n.i.g.ger", "-------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "n.i.g.g.er", "-------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "n.i.g.g.e.r", "------", RegexOptions.IgnoreCase);
           
            msg = Regex.Replace(msg, "ni.gger", "-------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "nig.ger", "-------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "nigg.er", "-------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "nigge.r", "-------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "nigger", "-------", RegexOptions.IgnoreCase);
            msg = Regex.Replace(msg, "n.igger", "-------", RegexOptions.IgnoreCase);
            return msg;
        }
    }
}
