using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MiyanoBot.Core.UserAccounts;

namespace MiyanoBot.Modules
{
	public class Misc : ModuleBase<SocketCommandContext>
	{
		[Command("myStats")]
		public async Task MyXP()
		{
			var account = UserAccounts.GetAccount(Context.User);
			await Context.Channel.SendMessageAsync($"You have {account.XP} XP, and {account.Points} points.");
		}

		[Command("addXP")]
		[RequireUserPermission(GuildPermission.Administrator)]
		public async Task AddXP(uint xp)
		{
			var account = UserAccounts.GetAccount(Context.User);
			account.XP += xp;
			UserAccounts.SaveAccounts();
			await Context.Channel.SendMessageAsync($"You gained {xp} xp.");
		}

		[Command("echo")]
		public async Task Echo([Remainder]string message)
		{
			var embed = new EmbedBuilder();
			embed.WithTitle("Echoed message by " + Context.User.Username);
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
			string[] options = message.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			Random r = new Random();
			string selection = options[r.Next(0, options.Length)];
			
			var embed = new EmbedBuilder();
			embed.WithTitle("Choice for " + Context.User.Username);
			embed.WithDescription(selection);
			embed.WithColor(new Color(223, 193, 159));
			embed.WithThumbnailUrl(Context.User.GetAvatarUrl());

			await Context.Channel.SendMessageAsync("", false, embed);
		}

		[Command("cookie")]
		public async Task Cookie()
		{
			await Context.Channel.SendMessageAsync(Utilities.GetAlert("COOKIE"));
		}

		[Command("rate")]
		public async Task Rate([Remainder]string message)
		{
			Random r = new Random();
			string rating = String.Format("{0}/10", r.Next(1, 11));

			await Context.Channel.SendMessageAsync(Utilities.GetFormattedAlert("RATE_&NAME_&RATING", message, rating));
		}

		[Command("percent")]
		public async Task Percent(string thing, string amount)
		{
			Random r = new Random();
			string percent = String.Format("{0}%", r.Next(1, 101));

			await Context.Channel.SendMessageAsync(Utilities.GetFormattedAlert("PERCENT_&THING_&PERCENTAGE_&AMOUNT", thing, percent, amount));
		}

		[Command("data")]
		public async Task GetData()
		{
			await Context.Channel.SendMessageAsync("Data has " + DataStorage.GetPairsCount() + " pairs");
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
