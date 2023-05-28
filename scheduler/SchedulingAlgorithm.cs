using System.ComponentModel;

namespace SchedulerSimulator
{
    /// <summary>
    /// Denotes an instance of a short-term scheduling algorithm.
    /// </summary>
    /// 
    /// It might be expected from a scheduling algorithm class to only have one public method which would perform the scheduling. 
    /// However, the system needs the short-term scheduler to perform different actions at different points in time. For example, 
    /// the action of scheduling a new process and the action of getting a process which is ready to be executed by the CPU are 
    /// distinct actions that must be called at distinct times. For this reason, the scheduler needs distinct public methods.
    /// The manner in which the scheduler performs these tasks depends on the algorithm being used, and consequently, 
    /// the algorithm class also needs distinct public methods.
    public interface ISchedulingAlgorithm
    {
        public abstract void Schedule(List<Process> processes, BindingList<SchedulerQueue> queues);

        public abstract void Schedule(Process process, BindingList<SchedulerQueue> queues);

        /// <summary>
        /// Gets a process which can be executed by the cpu.
        /// </summary>
        /// <param name="queues"></param>
        /// <returns></returns>
        public abstract Process? GetReadyProcess(BindingList<SchedulerQueue> queues);

        /// <summary>
        /// Algorithm logic.
        /// Actions unique to the specific scheduling algorithm should be performed by this method.
        /// </summary>
        /// <example> With the Round Robin algorithm, checking if a process' time slice is over would be done in this method. </example>
        public abstract void Run();
    }
}
