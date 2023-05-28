using System;
using System.ComponentModel;
using System.Net;

namespace SchedulerSimulator
{
    public class SystemSimulator : ILogable
    {
        private bool newProcessHasArrived;
        private CPU cpu;
        private BindingList<Process> memory;
        private Scheduler scheduler;
        private string log = "Log: ";

        public int SystemClock { get; private set; } = 1;
        public ILogableSchedulingAlgorithm Algorithm { get; set; }

        public SystemSimulator(ILogableSchedulingAlgorithm algorithm, CPU cpu, BindingList<Process> memory, Scheduler scheduler)
        {
            Algorithm = algorithm;
            this.cpu = cpu;
            this.memory = memory;
            this.scheduler = scheduler;
        }

        public void SimulateExecution()
        {
            ClearLog();

            CheckForNewlyArrivedProcesses(memory);

            if (newProcessHasArrived)
            {
                List<Process> newlyArrivedProcesses = GetNewlyArrivedProcesses(memory);
                scheduler.Schedule(newlyArrivedProcesses);

                Log(Algorithm.GetLog());
                Algorithm.ClearLog();
            }

            if (cpu.IsIdle())
            {
                Process? readyProcess = scheduler.GetReadyProcess();
                if (readyProcess != null)
                {
                    cpu.Push(readyProcess);

                    Log(cpu.GetLog());
                    cpu.ClearLog();
                }
            }

            if (!cpu.IsIdle())
            {
                cpu.SimulateProcessExecution();

                Log(cpu.GetLog());
                cpu.ClearLog();
                Log(cpu.Peek().GetLog());
                cpu.Peek().ClearLog();
            }

            if (cpu.FinishedExecutingProcess())
            {
                Process p = cpu.Pop();
                TerminateProcess(memory, p);
            }
            else if (ExecutingProcessMadeRequest(cpu))
            {
                PreemptProcess(cpu, scheduler);

                Log(Algorithm.GetLog());
                Algorithm.ClearLog();
            }

            scheduler.RunSchedulingAlgorithm();
            Log(Algorithm.GetLog());
            Algorithm.ClearLog();

            SystemClock++;
        }

        public void CheckForNewlyArrivedProcesses(BindingList<Process> memory)
        {
            if(memory.Any(p => p.ArrivalTime == SystemClock))
                newProcessHasArrived = true;
            else
                newProcessHasArrived = false;
        }

        public List<Process> GetNewlyArrivedProcesses(BindingList<Process> memory)
        {
            List<Process> newlyArrivedProcesses = (from p in memory
                                                  where p.ArrivalTime == SystemClock
                                                   select p).ToList();
            return newlyArrivedProcesses;
        }        

        public void TerminateProcess(BindingList<Process> memory, Process process) 
        { 
            memory.Remove(process);
            Log($"Process #{process.ID} was terminated\n");
        }

        public void PreemptProcess(CPU cpu, Scheduler scheduler)
        {
            Process process = cpu.Pop();

            if(!process.HasIORequest())
                process.Preempt();

            scheduler.Schedule(process);
        }

        public bool ExecutingProcessMadeRequest(CPU cpu)
        {
            if (!cpu.IsIdle())
            {
                Process p = cpu.Peek();
                if (p.Status.Equals("FINISHED_IO"))
                    return false;

                string request = p.GetIORequest();

                if (IO.IOTypes.Contains(request))
                    return true;
            }

            return false;
        }

        public void Log(string message) { log += message; }

        public void ClearLog() { log = ""; }

        public string GetLog() { return log; }
    }
}
