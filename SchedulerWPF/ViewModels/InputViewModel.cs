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
using System.Windows.Navigation;
using System.Windows.Shell;
using System.Windows.Controls;

namespace SchedulerWPF.ViewModels
{
    /// <summary>
    /// UI window that allows data about the processes and scheduling algorithm to be entered.
    /// </summary>
    public class InputViewModel : Screen
    {
        public string[] SchedulingAlgorithms { get; } = { "Round Robin", "FIFO" };
        private string _selectedSchedulingAlgorithm = "FIFO";
        public string SelectedSchedulingAlgorithm
        {
            get { return _selectedSchedulingAlgorithm; }
            set
            {
                _selectedSchedulingAlgorithm = value;
                ShellViewModel.SetSchedulingAlgorithm(value);
            }
        }

        private string _inputInfoText = "Enter the number of processes:";
        public string InputInfoText
        {
            get { return _inputInfoText; }
            set
            {
                _inputInfoText = value;
                NotifyOfPropertyChange(() => InputInfoText);
            }
        }

        private string _numberOfProcessesInput = "";
        public string NumberOfProcessesInput
        {
            get { return _numberOfProcessesInput; }
            set
            {
                _numberOfProcessesInput = value;
                NotifyOfPropertyChange(() => NumberOfProcessesInput);
            }
        }

        private Visibility _numberOfProcessesInputVisibility;
        public Visibility NumberOfProcessesInputVisibility
        {
            get { return _numberOfProcessesInputVisibility; }
            set
            {
                _numberOfProcessesInputVisibility = value;
                NotifyOfPropertyChange(() => NumberOfProcessesInputVisibility);
            }
        }

        private Visibility _startInputBtnVisibility;
        public Visibility StartInputBtnVisibility
        {
            get { return _startInputBtnVisibility; }
            set
            {
                _startInputBtnVisibility = value;
                NotifyOfPropertyChange(() => StartInputBtnVisibility);
            }
        }

        private BindingList<Process> _memory = new BindingList<Process>();
        public BindingList<Process> Memory
        {
            get { return _memory; }
            set
            {
                _memory = value;
                NotifyOfPropertyChange(() => Memory);
            }
        }

        private int NUMBER_PROCESSES;

        public void StartInput()
        {
            NUMBER_PROCESSES = int.Parse(NumberOfProcessesInput);

            StartInputBtnVisibility = Visibility.Hidden;
            NumberOfProcessesInputVisibility = Visibility.Hidden;
            InputInfoText = $"Number of processes entered: {NUMBER_PROCESSES}";

            for (int i = 1; i < NUMBER_PROCESSES+1; i++)
                Memory.Add(new Process(i));
        }

        public void FinishInput() 
        {
            foreach (Process process in Memory)
                process.BuildIODataFromInput();

            for (int i = 0; i < Memory.Count; i++)
                ShellViewModel.Memory.Add(Memory[i]);

            this.TryCloseAsync();
        }
    }
}
