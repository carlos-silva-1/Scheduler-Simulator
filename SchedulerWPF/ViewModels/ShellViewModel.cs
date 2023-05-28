using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SchedulerSimulator;
using System.Collections;
using System.ComponentModel;

namespace SchedulerWPF.ViewModels
{
    /// <summary>
    /// Main app window. Displays all the processes in the system, the cpu and the process its currnetly executing and the 
    /// scheduler's queues and the processes stores in them. 
    /// </summary>
    public class ShellViewModel : Screen
    {
        private string _systemClock = "System Clock: 1";
        public string SystemClock
        {
            get { return _systemClock; }
            set
            {
                _systemClock = value;
                NotifyOfPropertyChange(() => SystemClock);
            }
        }

        private string _currentProcessID = "Idle";
        public string CurrentProcessID
        {
            get { return _currentProcessID; }
            set
            {
                _currentProcessID = value;
                NotifyOfPropertyChange(() => CurrentProcessID);
            }
        }

        private string _currentProcessTime = "Idle";
        public string CurrentProcessTime
        {
            get { return _currentProcessTime; }
            set
            {
                _currentProcessTime = value;
                NotifyOfPropertyChange(() => CurrentProcessTime);
            }
        }

        private string _logMessages = "";
        public string LogMessages
        {
            get { return _logMessages; }
            set
            {
                _logMessages = value;
                NotifyOfPropertyChange(() => LogMessages);
            }
        }

        // Only allows the app to begin proper execution after the data about the processes has been inputted
        private bool dataInputted = false;

        public static CPU Cpu { get; set; } = new CPU();
        public static BindingList<Process> Memory { get; set; } = new BindingList<Process>();
        public static BindingList<SchedulerQueue> Queues { get; set; } = new BindingList<SchedulerQueue>();
        public static ILogableSchedulingAlgorithm Algorithm { get; set; } = new FIFO(Queues);
        public static Scheduler Scheduler { get; set; } = new Scheduler(Algorithm, Queues);
        public static SystemSimulator SystemSimulator { get; set; } = new SystemSimulator(Algorithm, Cpu, Memory, Scheduler);

        public static void SetSchedulingAlgorithm(string algorithm)
        {
            if (algorithm.Equals("Round Robin"))
                Algorithm = new RoundRobin(Cpu, Queues);
            else if (algorithm.Equals("FIFO"))
                Algorithm = new FIFO(Queues);

            Scheduler.Algorithm = Algorithm;
            SystemSimulator.Algorithm = Algorithm;
        }

        public void NextClockSignal()
        {
            if(dataInputted)
            {
                SystemSimulator.SimulateExecution();
                UpdateProcessesProperty(Queues);
                UpdateDisplayInfo();
            }
        }

        /// <summary>
        /// Allows correct display (in the UI) of the scheduler's queues and the processes in them
        /// </summary>
        /// <param name="queues"></param>
        private void UpdateProcessesProperty(BindingList<SchedulerQueue> queues)
        {
            foreach(SchedulerQueue queue in queues) 
            {
                Process[] processes = queue.ToArray();
                queue.Processes = processes;
            }
        }

        public void UpdateDisplayInfo()
        {
            SystemClock = "System Clock: " + SystemSimulator.SystemClock.ToString();
            LogMessages = SystemSimulator.GetLog();

            if (!Cpu.IsIdle())
            {
                CurrentProcessID = "Process ID: " + Cpu.Peek().ID.ToString();
                CurrentProcessTime = "Serv. Time: " + Cpu.Peek().RemainingServiceTime.ToString();
            }
            else
            {
                CurrentProcessID = "Idle";
                CurrentProcessTime = "Idle";
            }
        }

        public void InputData()
        {
            dataInputted = true; 
            Bootstrapper bs = new Bootstrapper();
            bs.DisplayInputWindow();
        }
    }
}
