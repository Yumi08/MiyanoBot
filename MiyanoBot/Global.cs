﻿using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyanoBot
{
	internal static class Global
	{
		internal static DiscordSocketClient Client { get; set; }
		internal static ulong MessageIdToTrack { get; set; }

		public static Random r = new Random();
	}
}