using System.Diagnostics;

namespace SchedulerSimulator
{
    public struct IOOperation
    {
        /// <summary>
        /// Relates the time when a process makes a request to the type of IO request. The time when a process makes a 
        /// request (dict key) indicates the time the process has been executed for. For example, the pair {2, "DISK"} 
        /// indicates that when the process has been executed for two clock signals, it makes a request to access the DISK peripheral
        /// </summary>
        public Dictionary<int, string> IORequestAtGivenTime;
        public string currentIORequest;
        public int currentOperationRemainingTime;
    }

    public class Process : ILogable
    {
        public int ID { get; private set; }
        public string IDDisplay
        {
            get { return $"ID: {ID}"; }
        }

        private readonly string[] possibleStatuses = { "NEW", "PREEMPTED", "IO_REQUEST", "FINISHED_IO", "DONE" };
        private string _status = "NEW";
        public string Status
        {
            get { return _status; }
            set
            {
                if (possibleStatuses.Contains(value))
                    _status = value;
                else
                    throw new InvalidOperationException($"Invalid status. The valid ones are in the array 'possibleStatuses' in Process.cs");
            }
        }

        public int ArrivalTime { get; set; } = 1;

        private int _totalServiceTime;
        public int TotalServiceTime
        {
            get { return _totalServiceTime; }
            set
            {
                _totalServiceTime = value;
                RemainingServiceTime = value;
            }
        }
        public string TotalServiceTimeDisplay
        {
            get { return $"Time:{TotalServiceTime}"; }
        }

        public int RemainingServiceTime { get; private set; }
        public string RemainingServiceTimeDisplay
        {
            get { return $"Time: {RemainingServiceTime}"; }
        }

        private int timeExecuted = 0;
        public IOOperation IOOperations;

        // Stores the IO requests the process will make. Binded to the UI.
        public string? IORequestTypes { get; set; }
        // Stores the times when the process will make the IO requests in 'IORequestTypes'. Binded to the UI.
        public string? IORequestTimes { get; set; }

        private string log = "";

        public Process() { }

        public Process(int id) { ID = id; }

        public bool HasIORequest()
        {
            if (Status == "IO_REQUEST")
                return true;
            return false;
        }

        public string GetIORequest() { return IOOperations.currentIORequest; }

        public bool HasFinishedExecution()
        {
            if (Status == "DONE")
                return true;
            return false;
        }

        public bool HasFinishedIOOperation()
        {
            if(Status == "FINISHED_IO")
                return true;
            return false;
        }

        public void Preempt() { Status = "PREEMPTED"; }

        public void SimulateExecution()
        {
            RemainingServiceTime--;
            timeExecuted++;
            if (IOOperations.IORequestAtGivenTime != null && IOOperations.IORequestAtGivenTime.ContainsKey(timeExecuted))
            {
                Status = "IO_REQUEST";
                IOOperations.currentIORequest = IOOperations.IORequestAtGivenTime[timeExecuted];
                IOOperations.currentOperationRemainingTime = IO.OperationTime[IOOperations.currentIORequest];
                Log($"Process #{ID} requested access to the {IOOperations.currentIORequest} peripheral\n");
            }
            else if (RemainingServiceTime <= 0)
                Status = "DONE";    
        }

        public void SimulateIOExecution()
        {
            IOOperations.currentOperationRemainingTime--;
            if (IOOperations.currentOperationRemainingTime <= 0)
                Status = "FINISHED_IO";
        }

        // Uses the data entered through the UI to set the IO data for a process
        public void BuildIODataFromInput()
        {
            IOOperations.IORequestAtGivenTime = new Dictionary<int, string>();
            string[] IORequestTypesSplit = Array.Empty<string>();
            string[] IORequestTimesSplit = Array.Empty<string>();

            if (IORequestTypes != null && IORequestTimes != null)
            {
                IORequestTypesSplit = IORequestTypes.Split(",");
                IORequestTimesSplit = IORequestTimes.Split(",");
            }

            if (IORequestTypesSplit.Length != IORequestTimesSplit.Length)
                throw new Exception("Each IO operation must have a time at which it is requested");
            else
            {
                for (int i = 0; i < IORequestTypesSplit.Length; i++)
                {
                    int key = int.Parse(IORequestTimesSplit[i]);
                    IOOperations.IORequestAtGivenTime.Add(key, IORequestTypesSplit[i].Trim().ToUpper());
                }
            }
        }

        public void Log(string message) { log += message; }

        public void ClearLog() { log = ""; }

        public string GetLog() { return log; }
    }
}
