using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyanoBot
{
	public static class MiscUtils
	{
		public static void Grammar(ref string p1, ref string p3)
		{
			// Changes I to you and vice versa in p1
			if (p1.ToLower() == "you") p1 = "I";
			else if (p1.ToLower() == "i") p1 = "You";

			// Changes me to you and vice versa in p3
			if (p3.Contains("me")) p3 = p3.Replace("me", "you");
			else if (p3.Contains("you")) p3 = p3.Replace("you", "me");

			// Changes I to you and vice versa in p3
			if (p3.Contains("I")) p3 = p3.Replace("I", "you");

			// Change your to my and vice versa in p3
			if (p3.Contains("your ")) p3 = p3.Replace("your", "my");
			else if (p3.Contains("my ")) p3 = p3.Replace("my", "your");

			// Change yourself to myself and vice versa in p3
			if (p3.Contains("yourself")) p3 = p3.Replace("yourself", "myself");
			else if (p3.Contains("myself")) p3 = p3.Replace("myself", "yourself");
		}

		public static void Grammar(ref string p3)
		{
			// Changes me to you and vice versa in p3
			if (p3.Contains("me ")) p3 = p3.Replace("me", "you");
			else if (p3.Contains("you ")) p3 = p3.Replace("you", "me");

			// Changes I to you and vice versa in p3
			if (p3.Contains("I ")) p3 = p3.Replace("I", "you");

			// Change your to my and vice versa in p3
			if (p3.Contains("your ")) p3 = p3.Replace("your ", "my ");
			else if (p3.Contains("my ")) p3 = p3.Replace("my ", "your ");

			// Change yourself to myself and vice versa in p3
			if (p3.Contains("yourself")) p3 = p3.Replace("yourself", "myself");
			else if (p3.Contains("myself")) p3 = p3.Replace("myself", "yourself");
		}
	}
}
