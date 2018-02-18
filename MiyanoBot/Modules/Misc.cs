using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using MiyanoBot.Core.UserAccounts;
using NReco.ImageGenerator;

namespace MiyanoBot.Modules
{
	public class Misc : ModuleBase<SocketCommandContext>
	{
		[Command("Warn")]
		[RequireUserPermission(GuildPermission.Administrator)]
		[RequireBotPermission(GuildPermission.KickMembers)]
		public async Task WarnUser(IGuildUser user)
		{
			var userAccount = UserAccounts.GetAccount((SocketUser)user);
			userAccount.NumberOfWarnings++;
			UserAccounts.SaveAccounts();

			if (userAccount.NumberOfWarnings >= 5)
			{
				await user.KickAsync();
			}
			else if (userAccount.NumberOfWarnings == 4)
			{
				var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
				await dmChannel.SendMessageAsync(Utilities.GetAlert("You will be kicked if you receive one or more warnings."));
			}
			else if (userAccount.NumberOfWarnings == 1)
			{
				var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
				await dmChannel.SendMessageAsync(Utilities.GetAlert("Please note that getting 5 or more warnings results in a kick."));
			}
		}

		[Command("kick")]
		[RequireUserPermission(GuildPermission.KickMembers)]
		[RequireBotPermission(GuildPermission.KickMembers)]
		public async Task KickUser(IGuildUser user, string reason = "No reason given.")
		{
			await user.KickAsync(reason);
		}

		[Command("ban")]
		[RequireUserPermission(GuildPermission.BanMembers)]
		[RequireBotPermission(GuildPermission.BanMembers)]
		public async Task BanUser(IGuildUser user, string reason = "No reason given.")
		{
			await user.Guild.AddBanAsync(user, 2, reason);
		}

		[Command("react")]
		public async Task HandleReactionMessage()
		{
			RestUserMessage msg = await Context.Channel.SendMessageAsync("React to me with :ok_hand:!");
			Global.MessageIdToTrack = msg.Id;
		}

		[Command("do")]
		public async Task Do_question(string p1, string p2, [Remainder]string p3)
		{
			// Removes question mark from end
			p3 = p3.Trim('?');

			// Sets mod to a degree
			string[] Degree = { "really", "definitely", "absolutely", "sorta", "kinda", "barely", "hardly", "don't", "definitely don't", "absolutely don't" };
			string mod = Degree[Global.r.Next(0, Degree.Length)];

			MiscUtils.GrammarDo(ref p1, ref p3);

			await Context.Channel.SendMessageAsync($"{p1} {mod} {p2} {p3}.");
		}

		[Command("do")]
		public async Task Do_question(string p1, string p2)
		{
			// Says yes or no etc
			string[] Degree = { "definitely", "yes", "maybe", "no", "definitely not" };
			string mod = Degree[Global.r.Next(0, Degree.Length)];

			await Context.Channel.SendMessageAsync(mod);
		}

		[Command("can")]
		[Alias("could", "may")]
		public async Task Can_question(string p1, string p2, [Remainder]string p3)
		{
			// Removes question mark from end
			p3 = p3.Trim('?');

			if (p1.ToLower().Contains("you"))
			{
				// Sets mod to a degree
				string[] Degree = { "undoubtedly will", "definitely will", "really want to", "want to", "don't want to", "don't really want to", "will never" };
				string mod = Degree[Global.r.Next(0, Degree.Length)];

				// Adding a space ensures that the GrammarReq can remove it
				p2 += " ";
				MiscUtils.GrammarReq(ref p1, ref p2, ref p3);

				await Context.Channel.SendMessageAsync($"{p1} {mod} {p2}{p3}.");
			}
			else
			{	
				// Says yes or no etc
				string[] Degree = { "definitely", "yes", "maybe", "no", "definitely not" };
				string mod = Degree[Global.r.Next(0, Degree.Length)];

				await Context.Channel.SendMessageAsync(mod);
			}
		}

		[Command("can")]
		[Alias("could", "may")]
		public async Task Can_question(string p1, string p2)
		{
			// Says yes or no etc
			string[] Degree = { "definitely", "please do", "yes", "I guess", "maybe", "no", "please don't", "definitely not" };
			string mod = Degree[Global.r.Next(0, Degree.Length)];

			await Context.Channel.SendMessageAsync(mod);
		}

		//[Command("are")]
		//public async Task Are_question(string p1, string p2, [Remainder]string p3)
		//{
		//	bool positive;
		//	string mod;

		//	// Removes question mark from end
		//	p3 = p3.Trim('?');


		//	Random r = new Random();
		//	if (r.Next(1, 101) >= 50) positive = true;
		//	else positive = false;
		//	// Sets mod to a degree
		//	if (positive)
		//	{
		//		string[] PosDegree = { "are", "are really" };
		//		mod = PosDegree[r.Next(1, PosDegree.Length)];
		//	}
		//	else
		//	{
		//		string[] NegDegree = { "aren't", "aren't really" };
		//		mod = NegDegree[r.Next(1, NegDegree.Length)];
		//	}

		//	MiscUtils.GrammarAre(ref p1, ref mod, ref p3, positive);

		//	await Context.Channel.SendMessageAsync($"{p1} {mod} {p2} {p3}.");
		//}

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
			await Context.Channel.SendMessageAsync($"{target.Username} has {account.XP} XP, is level {account.LevelNumber}, and has {account.Points} points.");
		}

		//[Command("level")]
		//public async Task WhatLevelIs(uint xp)
		//{
		//	uint level = (uint)Math.Sqrt(xp / 50);
		//	await Context.Channel.SendMessageAsync("The level is " + level);
		//}

		//[Command("addXP")]
		//[RequireUserPermission(GuildPermission.Administrator)]
		//public async Task AddXP(uint xp)
		//{
		//	var account = UserAccounts.GetAccount(Context.User);
		//	account.XP += xp;
		//	UserAccounts.SaveAccounts();
		//	await Context.Channel.SendMessageAsync($"You gained {xp} xp.");
		//}

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
		[Alias("choice")]
		public async Task PickOne([Remainder]string message)
		{
			string[] options = message.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			string selection = options[Global.r.Next(0, options.Length)];
			
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
			
			string rating = String.Format("{0}/10", Global.r.Next(0, 11));

			await Context.Channel.SendMessageAsync(Utilities.GetFormattedAlert("RATE_&NAME_&RATING", message, rating));
		}

		[Command("percent")]
		public async Task Percent(string thing, [Remainder]string amount)
		{
			string percent = String.Format("{0}%", Global.r.Next(0, 101));

			await Context.Channel.SendMessageAsync(Utilities.GetFormattedAlert("PERCENT_&THING_&PERCENTAGE_&AMOUNT", thing, percent, amount));
		}

		//[Command("data")]
		//public async Task GetData()
		//{
		//	await Context.Channel.SendMessageAsync("Data has " + DataStorage.GetPairsCount() + " pairs");
		//}

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
