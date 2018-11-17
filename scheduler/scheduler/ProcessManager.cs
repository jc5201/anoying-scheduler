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
		public List<string> getProcList()
		{
			IEnumerable<Process> processList = Process.GetProcesses().Where(pr => pr.MainWindowHandle != IntPtr.Zero);

			List<String> pList = new List<String>();

			foreach (Process p in processList)
			{
				pList.Add(p.ProcessName.ToString().ToLower());
			}

			return pList;
		}
		public bool checkList(List<string> blackList)
		{
			List<String> pList = getProcList();

			foreach (String p in blackList)
			{
				if (pList.Contains(p))
					return true;
			}

			return false;
		}

	}
}
