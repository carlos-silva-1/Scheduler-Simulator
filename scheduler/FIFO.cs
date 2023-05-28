using System.ComponentModel;

namespace SchedulerSimulator
{
    /// <summary>
    /// First-In First-Out scheduling algorithm.  
    /// </summary>
    public class FIFO : ILogableSchedulingAlgorithm
    {
        private List<string> ReadyQueueTypes { get; } = new List<string> { "READY" };

        BindingList<SchedulerQueue> queues;
        private bool finishedIOOperation = false;
        private string log = "";

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
            { "DISK", "READY" },
            { "PRINTER", "READY" },
            { "TAPE", "READY" }
        };

        /// <summary>
        /// Relates the process status (key) to the appropriate type of queue (value) that a process with a specific 
        /// status should be scheduled into.
        /// </summary>
        private readonly Dictionary<string, string> appropriateQueueBasedOnProcessStatus = new Dictionary<string, string>
        {
            { "NEW", "READY" },
            { "PREEMPTED", "READY" }
        };

        public FIFO(BindingList<SchedulerQueue> queues)
        {
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

        public void Log(string message) { log += message; }

        public void ClearLog() { log = ""; }

        public string GetLog() { return log; }
    }
}
