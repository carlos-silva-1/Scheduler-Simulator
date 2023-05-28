using System.ComponentModel;

namespace SchedulerSimulator
{
    /// <summary>
    /// Round Robin scheduling algorithm.
    /// </summary>
    public class RoundRobin : ILogableSchedulingAlgorithm
    {
        private List<string> ReadyQueueTypes { get; } = new List<string>{ "HIGH PRIORITY", "LOW PRIORITY" };

        private CPU cpu;
        BindingList<SchedulerQueue> queues;

        public const int TIME_SLICE = 4;
        private int timeSliceClock = 1;
        private bool finishedIOOperation = false;
        private string log = "";

        // Used to be able to say if the cpu has started executing a new process, so that 'timeSliceClock' can be updated correctly
        private bool cpuIdleDuringLastClockSignal = true;

        /// <summary>
        /// Relates the type of IO request (key) to the appropriate type of queue (value) that a process making that 
        /// request should be scheduled into.
        /// </summary>
        /// <example> The pair {"DISK", "DISK"} indicates a process requesting access to the disk should be sent to 
        /// the queue of type "DISK" </example>
        private readonly Dictionary<string, string> appropriateQueueBasedOnRequest = new Dictionary<string, string>
        {
            { "DISK", "DISK" },
            { "PRINTER", "PRINTER" },
            { "TAPE", "TAPE" }
        };

        /// <summary>
        /// Relates the type of IO operation that was done (key) to the appropriate type of queue (value) that a 
        /// process that executed that operation should be scheduled into.
        /// </summary>
        private readonly Dictionary<string, string> appropriateQueueBasedOnFinishedIO = new Dictionary<string, string>
        {
            { "DISK", "LOW PRIORITY" },
            { "PRINTER", "HIGH PRIORITY" },
            { "TAPE", "HIGH PRIORITY" }
        };

        /// <summary>
        /// Relates the process status (key) to the appropriate type of queue (value) that a process with a specific 
        /// status should be scheduled into.
        /// </summary>
        private readonly Dictionary<string, string> appropriateQueueBasedOnProcessStatus = new Dictionary<string, string>
        {
            { "NEW", "HIGH PRIORITY" },
            { "PREEMPTED", "LOW PRIORITY" }
        };

        public RoundRobin(CPU cpu, BindingList<SchedulerQueue> queues)
        {
            this.cpu = cpu;
            this.queues = queues;
        }

        public void Schedule(List<Process> processes, BindingList<SchedulerQueue> queues)
        {
            foreach (Process process in processes)
                Schedule(process, queues);
        }

        public void Schedule(Process process, BindingList<SchedulerQueue> queues)
        {
            string appropriateQueueType = GetAppropriateQueueType(process);
            SchedulerQueue queue = SchedulerQueue.GetQueue(queues, appropriateQueueType);
            Log($"Process #{process.ID} scheduled to {queue.QueueType} queue\n");

            queue.Enqueue(process);
        }

        private string GetAppropriateQueueType(Process p)
        {
            string appropriateQueueType;

            if (p.HasIORequest())
                appropriateQueueType = appropriateQueueBasedOnRequest[p.GetIORequest()];
            else if (p.HasFinishedIOOperation())
                appropriateQueueType = appropriateQueueBasedOnFinishedIO[p.GetIORequest()];
            else
                appropriateQueueType = appropriateQueueBasedOnProcessStatus[p.Status];

            return appropriateQueueType;
        }

        public Process? GetReadyProcess(BindingList<SchedulerQueue> queues)
        {
            SchedulerQueue queue;
            Process? readyProcess = null;

            foreach (string type in ReadyQueueTypes)
            {
                if (queues.Any(q => q.QueueType.Equals(type)))
                {
                    queue = (from q in queues
                             where q.QueueType.Equals(type)
                             select q).First();

                    if (queue.Count > 0)
                    {
                        readyProcess = queue.Dequeue();
                        break;
                    }
                }
            }

            return readyProcess;
        }

        public void Run()
        {
            SimulateIOOperations();
            if (finishedIOOperation)
                UpdateIOQueues();

            // When a new process is pushed into the cpu, the clock of the process' time slice should be reset
            if (NewProcessPushed(cpu))
                ResetTimeSliceClock(1);

            cpuIdleDuringLastClockSignal = cpu.IsIdle();

            if (ClockInterrupt() && !cpu.IsIdle())
            {
                Process p = cpu.Pop();
                p.Preempt();
                Log($"Process #{p.ID} preempted due to a clock interrupt\n");
                Schedule(p, queues);
                ResetTimeSliceClock(1);
            }

            if (!cpu.IsIdle())
                timeSliceClock++;
        }

        private void SimulateIOOperations()
        {
            BindingList<SchedulerQueue> IOQueues = SchedulerQueue.GetIOQueues(queues);

            foreach (SchedulerQueue queue in IOQueues)
            {
                if (queue.Count > 0)
                {
                    Process p = queue.Peek();
                    p.SimulateIOExecution();
                    Log($"Process #{p.ID} has {p.IOOperations.currentOperationRemainingTime} remaining\n");

                    if (p.HasFinishedIOOperation())
                    {
                        finishedIOOperation = true;
                        Log($"Process #{p.ID} has finished its IO operation\n");
                    }
                }
            }
        }

        private void UpdateIOQueues()
        {
            BindingList<SchedulerQueue> IOQueues = SchedulerQueue.GetIOQueues(queues);
            foreach (SchedulerQueue queue in IOQueues)
            {
                if (queue.Count > 0)
                {
                    Process p = queue.Peek();
                    if (p.HasFinishedIOOperation())
                    {
                        p = queue.Dequeue();
                        Schedule(p, queues);
                    }
                }
            }
        }

        private bool NewProcessPushed(CPU cpu)
        {
            if (cpuIdleDuringLastClockSignal)
                if (!cpu.IsIdle())
                    return true;
            return false;
        }

        public void ResetTimeSliceClock(int value = 0) { timeSliceClock = value; }

        public bool ClockInterrupt()
        {
            if (timeSliceClock % TIME_SLICE == 0)
                return true;
            return false;
        }

        public void Log(string message) { log += message; }

        public void ClearLog() { log = ""; }

        public string GetLog() { return log; }
    }
}
