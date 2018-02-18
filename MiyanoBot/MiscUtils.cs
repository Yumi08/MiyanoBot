using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyanoBot
{
	public static class MiscUtils
	{
		public static void GrammarDo(ref string p1, ref string p3)
		{
			// Changes I to you and vice versa in p1
			if (p1.ToLower() == "you") p1 = "I";
			else if (p1.ToLower() == "i") p1 = "You";

			P3fix(ref p3);
		}

		public static void GrammarReq(ref string p1, ref string p2, ref string p3)
		{
			// Changes I to you and vice versa in p1
			if (p1.ToLower() == "you") p1 = "I";
			else if (p1.ToLower() == "i") p1 = "You";

			// Removes please from p2
			if (p2.Contains("please")) p2 = p2.Replace("please ", "");

			P3fix(ref p3);
		}

		//public static void GrammarAre(ref string p1, ref string mod, ref string p3, bool positive)
		//{
		//	// Changes I to you and vice versa in p1
		//	if (p1.ToLower() == "you") p1 = "I";
		//	else if (p1.ToLower() == "i") p1 = "You";

		//	// Fixes I collision with are
		//	if (p1.ToLower() == "i" && positive) mod = "am";
		//	if (p1.ToLower() == "i" && !positive) mod = "am not";

		//	P3fix(ref p3);
		//}

		private static void P3fix(ref string p3)
		{
			// Changes me to you and vice versa in p3
			if (p3.Contains("me")) p3 = p3.Replace("me", "you");
			else if (p3.Contains("you")) p3 = p3.Replace("you", "me");

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
