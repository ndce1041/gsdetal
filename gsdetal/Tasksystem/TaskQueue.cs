//using System;
//using System.Collections.Concurrent;

//using System.Threading;

//public class TaskQueue
//{
//    private readonly TaskFactory _taskFactory;
//    private readonly SemaphoreSlim _semaphore;
//    private readonly ConcurrentQueue<Func<Task>> _tasks;

//    public TaskQueue(TaskScheduler scheduler)
//    {
//        _taskFactory = new TaskFactory(scheduler);
//        _semaphore = new SemaphoreSlim(1, 1); // 允许一个任务同时执行
//        _tasks = new ConcurrentQueue<Func<Task>>();
//    }

//    public void EnqueueTask(Func<Task> task)
//    {
//        _tasks.Enqueue(task);
//        ProcessQueue();
//    }

//    private async void ProcessQueue()
//    {
//        await _semaphore.WaitAsync();
//        try
//        {
//            while (_tasks.TryDequeue(out var task))
//            {
//                await _taskFactory.StartNew(task);
//            }
//        }
//        finally
//        {
//            _semaphore.Release();
//        }
//    }
//}

