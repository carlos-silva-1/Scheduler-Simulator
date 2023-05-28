namespace SchedulerSimulator
{
    /// <summary>
    /// Basic CPU cimulator. Simulates the execution of at most one process at a time.
    /// </summary>
    public class CPU : ILogable
    {
        private Process currentProcess = new();
        private bool idle = true;
        private string log = "";

        public bool IsIdle() { return idle; }

        public void Push(Process p)
        {
            if (idle)
            {
                currentProcess = p;
                Log($"Process #{p.ID} gets access to the CPU\n");
                idle = false;
            }
            else
                throw new Exception("CPU cannot get new process as it is already executing one.");
        }

        public Process Pop()
        {
            if (!idle)
            {
                idle = true;
                return currentProcess;
            }
            else
                throw new Exception("CPU cannot return process as it is not executing any process.");
        }

        public Process Peek()
        {
            if (!idle)
                return currentProcess;
            else
                throw new Exception("CPU cannot return process as it is not executing any process.");
        }

        public void SimulateProcessExecution()
        {
            if (!idle)
            {
                currentProcess.SimulateExecution();
                Log($"CPU executed process #{currentProcess.ID}\n");
            }
            else
                throw new Exception("CPU cannot execute process as it doesn't have a process.");
        }

        public bool FinishedExecutingProcess()
        {
            if (!idle)
            {
                if(currentProcess.HasFinishedExecution())
                    return true;
            }
            return false;
        }

        public void Log(string message) { log += message; }

        public void ClearLog() { log = ""; }

        public string GetLog() { return log; }
    }
}
