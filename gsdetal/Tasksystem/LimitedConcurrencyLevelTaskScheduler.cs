using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class LimitedConcurrencyLevelTaskScheduler : TaskScheduler
{
    private readonly int _maxDegreeOfParallelism;
    private readonly BlockingCollection<Task> _tasks = new BlockingCollection<Task>();
    private int _delegatesQueuedOrRunning = 0;

    public LimitedConcurrencyLevelTaskScheduler(int maxDegreeOfParallelism)
    {
        if (maxDegreeOfParallelism < 1) throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism));
        _maxDegreeOfParallelism = maxDegreeOfParallelism;

        // Start processing tasks
        for (int i = 0; i < _maxDegreeOfParallelism; i++)
        {
            Thread thread = new Thread(new ThreadStart(ExecuteTasks));
            thread.IsBackground = true;
            thread.Start();
        }
    }

    protected override IEnumerable<Task> GetScheduledTasks()
    {
        return _tasks.ToArray();
    }

    protected override void QueueTask(Task task)
    {
        _tasks.Add(task);
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        if (Thread.CurrentThread.IsThreadPoolThread)
        {
            return TryExecuteTask(task);
        }
        return false;
    }

    private void ExecuteTasks()
    {
        foreach (var task in _tasks.GetConsumingEnumerable())
        {
            TryExecuteTask(task);
        }
    }

    public override int MaximumConcurrencyLevel => _maxDegreeOfParallelism;
}

