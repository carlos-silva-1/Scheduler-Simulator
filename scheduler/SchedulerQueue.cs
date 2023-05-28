using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;

namespace SchedulerSimulator
{
    /// <summary>
    /// Queue of a short-term scheduler with an unique 'Queuetype' property, which is used to decide which processes 
    /// should be enqueued into it. The types of process which are appropriate for a given QueueType are given by the 
    /// scheduling algorithm.
    /// </summary>
    public class SchedulerQueue : Queue<Process>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _queueType;
        private Process[]? _processes;

        public string QueueType
        {
            get { return _queueType; }
            private set { _queueType = value; }
        }

        public Process[]? Processes
        {
            get { return _processes; }
            set
            {
                _processes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Processes)));
            }
        }

        public SchedulerQueue(string queueType) 
        { 
            _queueType = queueType; 
        }

        public new void Enqueue(Process p)
        {
            base.Enqueue(p);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, p));
        }

        public new Process Dequeue()
        {
            Process p = base.Dequeue();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, p, 0));
            return p;
        }

        public new void Clear()
        {
            base.Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public static SchedulerQueue GetQueue(BindingList<SchedulerQueue> queues, string queueType)
        {
            SchedulerQueue queue;

            if (!queues.Any(q => q.QueueType.Equals(queueType)))
                queues.Add(new SchedulerQueue(queueType));

            queue = (from q in queues
                     where q.QueueType.Equals(queueType)
                     select q).First();

            return queue;
        }

        public static BindingList<SchedulerQueue> GetIOQueues(BindingList<SchedulerQueue> queues)
        {
            BindingList<SchedulerQueue> IOQueues = new BindingList<SchedulerQueue>((from q in queues
                                                                                    where IO.IOTypes.Contains(q.QueueType)
                                                                                    select q).ToList());
            return IOQueues;
        }
    }
}
