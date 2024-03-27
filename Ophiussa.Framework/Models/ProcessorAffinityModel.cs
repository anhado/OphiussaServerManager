using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaFramework.Models {
    public class ProcessorAffinityModel {
        public string Code { get; set; }
        public string Name { get; set; }
    }


    public enum ProcessPriority {
        /// <summary>Specifies that the process has no special scheduling needs.</summary>
        Normal = 32, // 0x00000020

        /// <summary>
        ///     Specifies that the threads of this process run only when the system is idle, such as a screen saver. The
        ///     threads of the process are preempted by the threads of any process running in a higher priority class.
        /// </summary>
        Idle = 64, // 0x00000040

        /// <summary>
        ///     Specifies that the process performs time-critical tasks that must be executed immediately, such as the
        ///     <see langword="Task List" /> dialog, which must respond quickly when called by the user, regardless of the load on
        ///     the operating system. The threads of the process preempt the threads of normal or idle priority class processes.
        /// </summary>
        High = 128, // 0x00000080

        /// <summary>Specifies that the process has the highest possible priority.</summary>
        RealTime = 256, // 0x00000100

        /// <summary>Specifies that the process has priority above <see langword="Idle" /> but below <see langword="Normal" />.</summary>
        BelowNormal = 16384, // 0x00004000

        /// <summary>
        ///     Specifies that the process has priority above <see langword="Normal" /> but below
        ///     <see cref="F:System.Diagnostics.ProcessPriority.High" />.
        /// </summary>
        AboveNormal = 32768 // 0x00008000
    }
}
