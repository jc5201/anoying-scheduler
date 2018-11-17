using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
	class BanListManager
	{
		private static String banListDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AnnoyingScheduler");
		private static String banListFile = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AnnoyingScheduler"), "banList");

		public static void checkDir()
		{
			if (!Directory.Exists(banListDir)) { Directory.CreateDirectory(banListDir); }
		}

		public static List<String> GetBanList()
		{
			if (!System.IO.File.Exists(banListFile))
				return new List<String>();

			else
				return new List<String>(System.IO.File.ReadAllLines(banListFile));
		}

		public void Add(String proc)
		{
			checkDir();
			List<String> old = GetBanList();
			if (!old.Contains<String>(proc))
			{
				old.Add(proc);
				System.IO.File.WriteAllLines(banListFile, old);
			}
		}

		public void Rm(String proc)
		{
			checkDir();
			List<String> old = GetBanList();
			if (old.Contains<String>(proc))
			{
				old.Remove(proc);
				System.IO.File.WriteAllLines(banListFile, old);
			}
		}

		public static bool IsBanProcessRunning()
		{
			List<String> pList = ProcessManager.GetProcList();

			foreach (String p in GetBanList())
			{
				if (pList.Contains(p))
					return true;
			}

			return false;
		}
	}
}
