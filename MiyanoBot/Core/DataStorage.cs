using MiyanoBot.Core.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace MiyanoBot.Core
{
	public static class DataStorage
	{
		// Save all UserAccounts
		public static void SaveUserAccounts(IEnumerable<UserAccount> accounts, string filePath)
		{
			string json = JsonConvert.SerializeObject(accounts, Formatting.Indented);
			File.WriteAllText(filePath, json);
		}

		// Get all UserAccounts
		public static IEnumerable<UserAccount> LoadUserAccounts(string filePath)
		{
			if (!File.Exists(filePath)) return null;
			string json = File.ReadAllText(filePath);
			return JsonConvert.DeserializeObject<List<UserAccount>>(json);
		}

		public static bool SaveExists(string filePath)
		{
			return File.Exists(filePath);
		}
	}
}
