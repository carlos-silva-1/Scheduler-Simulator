using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    /// <summary>
    /// Allows logging information about its operations
    /// </summary>
    public interface ILogable
    {
        public abstract void Log(string message);
        public abstract string GetLog();
        public abstract void ClearLog();
    }
}
