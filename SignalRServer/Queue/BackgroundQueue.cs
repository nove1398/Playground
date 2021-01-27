using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRServer.Queue
{
    public interface IBackgroundQueue
    {
        void QueueTask(Func<CancellationToken, Task> task);

        Task<Func<CancellationToken, Task>> PopQueue(CancellationToken cancellationToken);
    }

    public class BackgroundQueue : IBackgroundQueue
    {
        private ConcurrentQueue<Func<CancellationToken, Task>> Tasks;
        private SemaphoreSlim signal;

        public BackgroundQueue()
        {
            Tasks = new ConcurrentQueue<Func<CancellationToken, Task>>();
            signal = new SemaphoreSlim(0);
        }

        public async Task<Func<CancellationToken, Task>> PopQueue(CancellationToken cancellationToken)
        {
            await signal.WaitAsync(cancellationToken);
            Tasks.TryDequeue(out var task);

            return task;
        }

        public void QueueTask(Func<CancellationToken, Task> task)
        {
            Tasks.Enqueue(task);
            signal.Release();
        }
    }
}