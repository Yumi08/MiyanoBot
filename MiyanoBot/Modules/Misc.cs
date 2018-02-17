using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace MiyanoBot.Modules
{
	public class Misc : ModuleBase<SocketCommandContext>
	{
		[Command("echo")]
		public async Task Echo([Remainder]string message)
		{
			var embed = new EmbedBuilder();
			embed.WithTitle("Echoed message");
			embed.WithDescription(message);
			embed.WithColor(new Color(220, 20, 60));

			await Context.Channel.SendMessageAsync("", false, embed);
		}
		[Command("hello")]
		public async Task Hello()
		{
			await Context.Channel.SendMessageAsync($"Hello {Context.User.Username}!");
		}
	}
}
