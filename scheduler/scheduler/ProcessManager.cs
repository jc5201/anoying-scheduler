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
		public static List<String> GetProcList()
		{
			IEnumerable<Process> processList = Process.GetProcesses().Where(pr => pr.MainWindowHandle != IntPtr.Zero);

			List<String> pList = new List<String>();

			foreach (Process p in processList)
			{
				pList.Add(p.ProcessName.ToString().ToLower());
			}

			return pList;
		}
		

	}
}
