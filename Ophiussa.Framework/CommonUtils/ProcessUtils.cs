using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OphiussaFramework.CommonUtils {
    public static class ProcessUtils {
        private const int SwRestore = 9;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static string FieldCommandline = "CommandLine";
        public static string FieldExecutablepath = "ExecutablePath";
        public static string FieldProcessid = "ProcessId";
        private static Mutex _mutex;

        public static int ProcessorCount => Environment.ProcessorCount;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GenerateConsoleCtrlEvent(
            CtrlTypes dwCtrlEvent,
            uint dwProcessGroupId);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(
            ConsoleCtrlDelegate handlerRoutine,
            bool add);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int IsIconic(IntPtr hWnd);

        public static string GetCommandLineForProcess(int processId) {
            using (var managementObjectSearcher = new ManagementObjectSearcher(string.Format("SELECT {0} FROM Win32_Process WHERE {1} = {2}", FieldCommandline, FieldProcessid, processId))) {
                using (var source = managementObjectSearcher.Get()) {
                    var managementObject = source.Cast<ManagementObject>().FirstOrDefault();
                    if (managementObject != null)
                        return (string)managementObject[FieldCommandline];
                }
            }

            return null;
        }

        public static string GetMainModuleFilepath(int processId) {
            using (var managementObjectSearcher = new ManagementObjectSearcher(string.Format("SELECT {0} FROM Win32_Process WHERE {1} = {2}", FieldExecutablepath, FieldProcessid, processId))) {
                using (var source = managementObjectSearcher.Get()) {
                    var managementObject = source.Cast<ManagementObject>().FirstOrDefault();
                    if (managementObject != null)
                        return (string)managementObject[FieldExecutablepath];
                }
            }

            return null;
        }

        public static async Task SendStopAsync(Process process) {
            if (process == null)
                return;
            var ts = new TaskCompletionSource<bool>();
            EventHandler eventHandler = (s, e) => ts.TrySetResult(true);
            try {
                process.Exited += eventHandler;
                bool flag = AttachConsole((uint)process.Id);
                if (flag) {
                    flag = SetConsoleCtrlHandler(null, true);
                    if (flag) {
                        flag = GenerateConsoleCtrlEvent(CtrlTypes.CtrlCEvent, 0U);
                        if (flag) {
                            FreeConsole();
                            try {
                                ts.Task.Wait(10000);
                                flag = true;
                            }
                            catch (Exception ex) {
                                flag = false;
                            }

                            SetConsoleCtrlHandler(null, false);
                        }
                        else {
                            SetConsoleCtrlHandler(null, false);
                            FreeConsole();
                        }
                    }
                    else {
                        FreeConsole();
                    }
                }

                if (flag || process.HasExited)
                    return;
                process.Kill();
            }
            finally {
                process.Exited -= eventHandler;
            }
        }

        public static Task<bool> RunProcessAsync(
            string file,
            string arguments,
            string verb,
            string workingDirectory,
            string username,
            SecureString password,
            List<int> steamCmdIgnoreExitStatusCodes,
            DataReceivedEventHandler outputHandler,
            CancellationToken cancellationToken,
            ProcessWindowStyle windowStyle = ProcessWindowStyle.Normal) {
            try {
                string fileName = !string.IsNullOrWhiteSpace(file) && File.Exists(file) ? Path.GetFileName(file) : throw new FileNotFoundException("The specified file does not exist or could not be found.", file);
                var startInfo = new ProcessStartInfo {
                    FileName = file,
                    Arguments = arguments,
                    Verb = verb,
                    UseShellExecute = outputHandler == null && windowStyle == ProcessWindowStyle.Minimized && string.IsNullOrWhiteSpace(username),
                    RedirectStandardOutput = outputHandler != null,
                    CreateNoWindow = outputHandler != null || windowStyle == ProcessWindowStyle.Hidden,
                    WindowStyle = windowStyle,
                    UserName = string.IsNullOrWhiteSpace(username) ? null : username,
                    Password = string.IsNullOrWhiteSpace(username) ? null : password,
                    WorkingDirectory = string.IsNullOrWhiteSpace(workingDirectory) || !Directory.Exists(workingDirectory) ? Path.GetDirectoryName(file) : workingDirectory
                };
                var process = Process.Start(startInfo);
                process.EnableRaisingEvents = true;
                if (startInfo.RedirectStandardOutput && outputHandler != null) {
                    process.OutputDataReceived += outputHandler;
                    process.BeginOutputReadLine();
                }

                var tcs = new TaskCompletionSource<bool>();
                using (cancellationToken.Register(() => {
                    try {
                        process.Kill();
                    }
                    finally {
                        tcs.TrySetCanceled();
                    }
                })) {
                    process.Exited += (s, e) => {
                        int num = process.ExitCode;
                        Logger.Debug(string.Format("{0}: filename {1}; exitcode = {2}", nameof(RunProcessAsync), fileName, num));
                        if (num != 0) {
                            Logger.Error(string.Format("{0}: filename {1}; exitcode = {2}", nameof(RunProcessAsync), fileName, num));
                            if (steamCmdIgnoreExitStatusCodes.Contains(num))
                                num = 0;
                        }

                        tcs.TrySetResult(num == 0);
                        process.Close();
                    };
                    return tcs.Task;
                }
            }
            catch (Exception ex) {
                Logger.Error("RunProcessAsync. " + ex.Message + "\r\n" + ex.StackTrace);
                throw;
            }
        }

        private static IntPtr GetCurrentInstanceWindowHandle() {
            var instanceWindowHandle = IntPtr.Zero;
            var currentProcess = Process.GetCurrentProcess();
            foreach (var process in Process.GetProcessesByName(currentProcess.ProcessName))
                if (process.Id != currentProcess.Id && process.MainModule.FileName == currentProcess.MainModule.FileName && process.MainWindowHandle != IntPtr.Zero) {
                    instanceWindowHandle = process.MainWindowHandle;
                    break;
                }

            return instanceWindowHandle;
        }

        public static bool IsAlreadyRunning() {
            bool createdNew;
            _mutex = new Mutex(true, "Global::" + Path.GetFileName(Assembly.GetEntryAssembly().Location), out createdNew);
            if (createdNew)
                _mutex.ReleaseMutex();
            return !createdNew;
        }

        public static bool SwitchToCurrentInstance() {
            var instanceWindowHandle = GetCurrentInstanceWindowHandle();
            if (instanceWindowHandle == IntPtr.Zero)
                return false;
            if (IsIconic(instanceWindowHandle) != 0)
                ShowWindow(instanceWindowHandle, 9);
            SetForegroundWindow(instanceWindowHandle);
            return true;
        }

        public static IEnumerable<BigInteger> GetProcessorAffinityList() {
            int processorCount = ProcessorCount;
            var processorAffinityList = new List<BigInteger>(processorCount + 1);
            processorAffinityList.Add(BigInteger.Zero);
            for (int y = 0; y < processorCount; ++y)
                processorAffinityList.Add((BigInteger)Math.Pow(2.0, y));
            return processorAffinityList;
        }

        public static string[] GetProcessPriorityList() {
            return new string[5] {
                                     "low",
                                     "belownormal",
                                     "normal",
                                     "abovenormal",
                                     "high"
                                 };
        }

        public static bool IsProcessorAffinityValid(BigInteger affinityValue) {
            if (affinityValue == BigInteger.Zero)
                return true;
            var bigInteger = (BigInteger)Math.Pow(2.0, ProcessorCount);
            return !(affinityValue < BigInteger.Zero) && !(affinityValue > bigInteger);
        }

        public static bool IsProcessPriorityValid(string priorityValue) {
            return !string.IsNullOrWhiteSpace(priorityValue) && GetProcessPriorityList().Contains(priorityValue);
        }

        private delegate bool ConsoleCtrlDelegate(CtrlTypes ctrlType);

        private enum CtrlTypes : uint {
            CtrlCEvent = 0,
            CtrlBreakEvent = 1,
            CtrlCloseEvent = 2,
            CtrlLogoffEvent = 5,
            CtrlShutdownEvent = 6
        }

    }
}
