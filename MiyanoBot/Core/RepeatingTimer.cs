using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace MiyanoBot.Core
{
	internal static class RepeatingTimer
	{
		private static Timer loopingTimer;
		private static SocketTextChannel channel;

		internal static Task StartTimer()
		{
			channel = Global.Client.GetGuild(397315696976986113).GetTextChannel(397315697572839427);

			loopingTimer = new Timer()
			{
				Interval = 60000,
				AutoReset = true,
				Enabled = true
			};
			loopingTimer.Elapsed += OnTimerTicked;

			Console.WriteLine("StartClock");
			return Task.CompletedTask;
		}

		private static async void OnTimerTicked(object sender, ElapsedEventArgs e)
		{

		}
	}
}
