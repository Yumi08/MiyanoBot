using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MiyanoBot.Core.UserAccounts;
using NReco.ImageGenerator;

namespace MiyanoBot.Modules
{
	public class Misc : ModuleBase<SocketCommandContext>
	{
		[Command("do")]
		public async Task Do_question(string p1, string p2, [Remainder]string p3)
		{
			string LV = "do";

			// Determines if what she's going to say is positive or negative
			Random r = new Random();
			bool positive = (r.Next(1, 101) >= 50 ? true : false);

			// Changes I to you and vice versa
			if (p1.ToLower() == "you") p1 = "I";
			else if (p1.ToLower() == "i") p1 = "You";

			// Removes question mark from end
			p3 = p3.Trim('?');

			// Changes to negative if necessary
			if (!positive)
			{
				LV = "don't";
			}

			await Context.Channel.SendMessageAsync($"{p1} {LV} {p2} {p3}.");
		}

		[Command("hey")]
		public async Task Hey(string color = "red")
		{
			string css = "<style>\n    h1{\n        color: " + color + ";\n    }\n</style>\n";
			string html = String.Format("<h1>Hello {0}!</h1>", Context.User.Username);
			var converter = new HtmlToImageConverter
			{
				Width = 250,
				Height = 70
			};
			var jpgBytes = converter.GenerateImage(css + html, NReco.ImageGenerator.ImageFormat.Jpeg);
			await Context.Channel.SendFileAsync(new MemoryStream(jpgBytes), "hello.jpg");
		}

		[Command("stats")]
		public async Task Stats([Remainder]string arg = "")
		{
			SocketUser target = null;
			var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
			target = mentionedUser ?? Context.User;

			var account = UserAccounts.GetAccount(target);
			await Context.Channel.SendMessageAsync($"{target.Username} has {account.XP} XP, and {account.Points} points.");
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
