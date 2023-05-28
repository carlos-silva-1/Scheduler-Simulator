using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    /// <summary>
    /// Extension of the 'SchedulingAlgorithm' interface that allows storing information about its operations.
    /// </summary>
    public interface ILogableSchedulingAlgorithm : ISchedulingAlgorithm, ILogable
    {
    }
}
