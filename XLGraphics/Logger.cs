using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityModManagerNet;

namespace XLGraphics
{
	static class Logger
	{
		public static void Log(string s) {
			UnityModManager.Logger.Log("[XLGraphics] " + s);
		}
	}
}
