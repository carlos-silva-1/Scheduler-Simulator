using System.ComponentModel;

namespace SchedulerSimulator
{
    /// <summary>
    /// Schedules processes into appropriate queues. Behaviour depends on scheduling algorithm used. 
    /// </summary>
    public class Scheduler
    {
        public ILogableSchedulingAlgorithm Algorithm { get; set; }
        private BindingList<SchedulerQueue> queues;

        public Scheduler(ILogableSchedulingAlgorithm algorithm, BindingList<SchedulerQueue> queues)
        {
            Algorithm = algorithm;
            this.queues = queues;
        }

        public void Schedule(List<Process> processes) { Algorithm.Schedule(processes, queues); }

        public void Schedule(Process p) { Algorithm.Schedule(p, queues); }

        public Process? GetReadyProcess() { return Algorithm.GetReadyProcess(queues); }

        public void RunSchedulingAlgorithm() { Algorithm.Run();  }
    }
}
