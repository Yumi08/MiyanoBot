using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiyanoBot.Core.UserAccounts;
using Discord;

namespace MiyanoBot.Core.LevelingSystem
{
	internal static class Leveling
	{
		internal static async void UserSentMessage(SocketGuildUser user, SocketTextChannel channel)
		{
			// TODO: If the user has a timeout, ignore them

			var userAccount = UserAccounts.UserAccounts.GetAccount(user);
			uint oldLevel = userAccount.LevelNumber;
			userAccount.XP += 50;
			UserAccounts.UserAccounts.SaveAccounts();
			uint newLevel = userAccount.LevelNumber;

			if (oldLevel != newLevel)
			{
				var embed = new EmbedBuilder();
				embed.WithColor(220, 20, 60);
				embed.WithTitle("LEVEL UP!");
				embed.WithDescription(user.Username + " has leveled up!");
				embed.AddInlineField("Level: ", newLevel);
				embed.AddInlineField("XP: ", userAccount.XP);

				//await channel.SendMessageAsync("", embed: embed);
			}
		}
	}
}
