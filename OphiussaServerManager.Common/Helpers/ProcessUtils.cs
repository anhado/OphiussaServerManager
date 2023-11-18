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
using System.Threading;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Helpers
{
    public static class ProcessUtils
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public static string FIELD_COMMANDLINE = "CommandLine";
        public static string FIELD_EXECUTABLEPATH = "ExecutablePath";
        public static string FIELD_PROCESSID = "ProcessId";
        private const int SW_RESTORE = 9;
        private static Mutex _mutex;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GenerateConsoleCtrlEvent(
          ProcessUtils.CtrlTypes dwCtrlEvent,
          uint dwProcessGroupId);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(
          ProcessUtils.ConsoleCtrlDelegate HandlerRoutine,
          bool Add);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int IsIconic(IntPtr hWnd);

        public static string GetCommandLineForProcess(int processId)
        {
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(string.Format("SELECT {0} FROM Win32_Process WHERE {1} = {2}", (object)ProcessUtils.FIELD_COMMANDLINE, (object)ProcessUtils.FIELD_PROCESSID, (object)processId)))
            {
                using (ManagementObjectCollection source = managementObjectSearcher.Get())
                {
                    ManagementObject managementObject = source.Cast<ManagementObject>().FirstOrDefault<ManagementObject>();
                    if (managementObject != null)
                        return (string)managementObject[ProcessUtils.FIELD_COMMANDLINE];
                }
            }
            return (string)null;
        }

        public static string GetMainModuleFilepath(int processId)
        {
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(string.Format("SELECT {0} FROM Win32_Process WHERE {1} = {2}", (object)ProcessUtils.FIELD_EXECUTABLEPATH, (object)ProcessUtils.FIELD_PROCESSID, (object)processId)))
            {
                using (ManagementObjectCollection source = managementObjectSearcher.Get())
                {
                    ManagementObject managementObject = source.Cast<ManagementObject>().FirstOrDefault<ManagementObject>();
                    if (managementObject != null)
                        return (string)managementObject[ProcessUtils.FIELD_EXECUTABLEPATH];
                }
            }
            return (string)null;
        }

        public static async Task SendStopAsync(Process process)
        {
            if (process == null)
                return;
            TaskCompletionSource<bool> ts = new TaskCompletionSource<bool>();
            EventHandler eventHandler = (EventHandler)((s, e) => ts.TrySetResult(true));
            try
            {
                process.Exited += eventHandler;
                bool flag = ProcessUtils.AttachConsole((uint)process.Id);
                if (flag)
                {
                    flag = ProcessUtils.SetConsoleCtrlHandler((ProcessUtils.ConsoleCtrlDelegate)null, true);
                    if (flag)
                    {
                        flag = ProcessUtils.GenerateConsoleCtrlEvent(ProcessUtils.CtrlTypes.CTRL_C_EVENT, 0U);
                        if (flag)
                        {
                            ProcessUtils.FreeConsole();
                            try
                            {
                                ts.Task.Wait(10000);
                                flag = true;
                            }
                            catch (Exception ex)
                            {
                                flag = false;
                            }
                            ProcessUtils.SetConsoleCtrlHandler((ProcessUtils.ConsoleCtrlDelegate)null, false);
                        }
                        else
                        {
                            ProcessUtils.SetConsoleCtrlHandler((ProcessUtils.ConsoleCtrlDelegate)null, false);
                            ProcessUtils.FreeConsole();
                        }
                    }
                    else
                        ProcessUtils.FreeConsole();
                }
                if (flag || process.HasExited)
                    return;
                process.Kill();
            }
            finally
            {
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
          List<int> SteamCmdIgnoreExitStatusCodes,
          DataReceivedEventHandler outputHandler,
          CancellationToken cancellationToken,
          ProcessWindowStyle windowStyle = ProcessWindowStyle.Normal)
        {
            try
            {
                string fileName = !string.IsNullOrWhiteSpace(file) && File.Exists(file) ? Path.GetFileName(file) : throw new FileNotFoundException("The specified file does not exist or could not be found.", file);
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = file,
                    Arguments = arguments,
                    Verb = verb,
                    UseShellExecute = outputHandler == null && windowStyle == ProcessWindowStyle.Minimized && string.IsNullOrWhiteSpace(username),
                    RedirectStandardOutput = outputHandler != null,
                    CreateNoWindow = outputHandler != null || windowStyle == ProcessWindowStyle.Hidden,
                    WindowStyle = windowStyle,
                    UserName = string.IsNullOrWhiteSpace(username) ? (string)null : username,
                    Password = string.IsNullOrWhiteSpace(username) ? (SecureString)null : password,
                    WorkingDirectory = string.IsNullOrWhiteSpace(workingDirectory) || !Directory.Exists(workingDirectory) ? Path.GetDirectoryName(file) : workingDirectory
                };
                Process process = Process.Start(startInfo);
                process.EnableRaisingEvents = true;
                if (startInfo.RedirectStandardOutput && outputHandler != null)
                {
                    process.OutputDataReceived += outputHandler;
                    process.BeginOutputReadLine();
                }
                TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
                using (cancellationToken.Register((Action)(() =>
                {
                    try
                    {
                        process.Kill();
                    }
                    finally
                    {
                        tcs.TrySetCanceled();
                    }
                })))
                {
                    process.Exited += (EventHandler)((s, e) =>
                    {
                        int num = process.ExitCode;
                        ProcessUtils._logger.Debug(string.Format("{0}: filename {1}; exitcode = {2}", (object)nameof(RunProcessAsync), (object)fileName, (object)num));
                        if (num != 0)
                        {
                            ProcessUtils._logger.Error(string.Format("{0}: filename {1}; exitcode = {2}", (object)nameof(RunProcessAsync), (object)fileName, (object)num));
                            if (SteamCmdIgnoreExitStatusCodes.Contains(num))
                                num = 0;
                        }
                        tcs.TrySetResult(num == 0);
                        process.Close();
                    });
                    return tcs.Task;
                }
            }
            catch (Exception ex)
            {
                ProcessUtils._logger.Error("RunProcessAsync. " + ex.Message + "\r\n" + ex.StackTrace);
                throw;
            }
        }

        private static IntPtr GetCurrentInstanceWindowHandle()
        {
            IntPtr instanceWindowHandle = IntPtr.Zero;
            Process currentProcess = Process.GetCurrentProcess();
            foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
            {
                if (process.Id != currentProcess.Id && process.MainModule.FileName == currentProcess.MainModule.FileName && process.MainWindowHandle != IntPtr.Zero)
                {
                    instanceWindowHandle = process.MainWindowHandle;
                    break;
                }
            }
            return instanceWindowHandle;
        }

        public static bool IsAlreadyRunning()
        {
            bool createdNew;
            ProcessUtils._mutex = new Mutex(true, "Global::" + Path.GetFileName(Assembly.GetEntryAssembly().Location), out createdNew);
            if (createdNew)
                ProcessUtils._mutex.ReleaseMutex();
            return !createdNew;
        }

        public static bool SwitchToCurrentInstance()
        {
            IntPtr instanceWindowHandle = ProcessUtils.GetCurrentInstanceWindowHandle();
            if (instanceWindowHandle == IntPtr.Zero)
                return false;
            if (ProcessUtils.IsIconic(instanceWindowHandle) != 0)
                ProcessUtils.ShowWindow(instanceWindowHandle, 9);
            ProcessUtils.SetForegroundWindow(instanceWindowHandle);
            return true;
        }

        public static int ProcessorCount => Environment.ProcessorCount;

        public static IEnumerable<BigInteger> GetProcessorAffinityList()
        {
            int processorCount = ProcessUtils.ProcessorCount;
            List<BigInteger> processorAffinityList = new List<BigInteger>(processorCount + 1);
            processorAffinityList.Add(BigInteger.Zero);
            for (int y = 0; y < processorCount; ++y)
                processorAffinityList.Add((BigInteger)Math.Pow(2.0, (double)y));
            return (IEnumerable<BigInteger>)processorAffinityList;
        }

        public static string[] GetProcessPriorityList() => new string[5]
        {
      "low",
      "belownormal",
      "normal",
      "abovenormal",
      "high"
        };

        public static bool IsProcessorAffinityValid(BigInteger affinityValue)
        {
            if (affinityValue == BigInteger.Zero)
                return true;
            BigInteger bigInteger = (BigInteger)Math.Pow(2.0, (double)ProcessUtils.ProcessorCount);
            return !(affinityValue < BigInteger.Zero) && !(affinityValue > bigInteger);
        }

        public static bool IsProcessPriorityValid(string priorityValue) => !string.IsNullOrWhiteSpace(priorityValue) && ((IEnumerable<string>)ProcessUtils.GetProcessPriorityList()).Contains<string>(priorityValue);

        private delegate bool ConsoleCtrlDelegate(ProcessUtils.CtrlTypes CtrlType);

        private enum CtrlTypes : uint
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6,
        }
    }
}
