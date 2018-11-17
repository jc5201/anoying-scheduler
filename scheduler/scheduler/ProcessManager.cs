using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace scheduler
{
	class ProcessManager
	{
		private Process[] processList;

		public bool checkList(List<string> blackList)
		{
			processList = Process.GetProcesses();

			List<String> pList = new List<String>();

			foreach (Process p in processList)
			{
				pList.Add(p.ProcessName.ToString());
			}

			foreach (String p in blackList)
			{
				if (pList.Contains(p))
					return true;
			}

			return false;
		}

	}
}
