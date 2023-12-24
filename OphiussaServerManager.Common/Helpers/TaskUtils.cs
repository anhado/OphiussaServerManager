using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace OphiussaServerManager.Common.Helpers {
    public static class TaskUtils {
        public static readonly Task FinishedTask = Task.FromResult(true);

        public static void DoNotWait(this Task task) {
        }

        public static void DoNotWait(this ValueTask task) {
        }

        public static async Task RunOnUIThreadAsync(Action action) {
            var current = Application.Current;
            if (current == null)
                return;
            await current.Dispatcher.InvokeAsync(action);
        }

        public static async Task TimeoutAfterAsync(this Task task, int millisecondsDelay) {
            using (var timeoutCancellationTokenSource = new CancellationTokenSource()) {
                if (await Task.WhenAny(task, Task.Delay(millisecondsDelay, timeoutCancellationTokenSource.Token)) != task)
                    throw new TimeoutException("The operation has timed out.");
                timeoutCancellationTokenSource.Cancel();
                await task;
            }
        }

        public static async Task<TResult> TimeoutAfterAsync<TResult>(
            this Task<TResult> task,
            int                millisecondsDelay) {
            TResult result;
            using (var timeoutCancellationTokenSource = new CancellationTokenSource()) {
                if (await Task.WhenAny(task, Task.Delay(millisecondsDelay, timeoutCancellationTokenSource.Token)) != task)
                    throw new TimeoutException("The operation has timed out.");
                timeoutCancellationTokenSource.Cancel();
                result = await task;
            }

            return result;
        }
    }
}