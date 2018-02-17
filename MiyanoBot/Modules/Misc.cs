using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

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
			embed.WithColor(new Color(223, 193, 159));

			await Context.Channel.SendMessageAsync("", false, embed);
		}

		[Command("hello")]
		public async Task Hello()
		{
			await Context.Channel.SendMessageAsync(Utilities.GetFormattedAlert("HELLO_&NAME", Context.User.Username));
		}

		[Command("pick")]
		public async Task PickOne([Remainder]string message)
		{
			string[] options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

			Random r = new Random();
			string selection = options[r.Next(0, options.Length)];
			
			var embed = new EmbedBuilder();
			embed.WithTitle("Choice for " + Context.User.Username);
			embed.WithDescription(selection);
			embed.WithColor(new Color(223, 193, 159));
			embed.WithThumbnailUrl(Context.User.GetAvatarUrl());

			await Context.Channel.SendMessageAsync("", false, embed);
		}

		//[Command("secret")]
		//public async Task RevealSecret([Remainder]string arg = "")
		//{
		//	if (!UserIsAAA((SocketGuildUser)Context.User)) return;
		//	var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
		//	await dmChannel.SendMessageAsync(Utilities.GetAlert("SECRET"));
		//}

		//private bool UserIsAAA(SocketGuildUser user)
		//{
		//	string targetRoleName = "aaa";
		//	var result = from r in user.Guild.Roles
		//				 where r.Name == targetRoleName
		//				 select r.Id;
		//	ulong roleID = result.FirstOrDefault();
		//	if (roleID == 0) return false;
		//	var targetRole = user.Guild.GetRole(roleID);
		//	return user.Roles.Contains(targetRole);
		//}
	}
}
