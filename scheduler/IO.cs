using System.ComponentModel;

namespace SchedulerSimulator
{
    /// <summary>
    /// Stores information on what IO peripherals the system has access to and the time taken to perform one IO operation
    /// with each of these peripherals.
    /// </summary>
    public class IO
    {
        public static readonly List<string> IOTypes = new List<string>{ "DISK", "PRINTER", "TAPE" };

        public static readonly Dictionary<string, int> OperationTime = new Dictionary<string, int>
        {
            { "DISK", 2 },
            { "PRINTER", 4 },
            { "TAPE", 3 }
        };
    }
}
