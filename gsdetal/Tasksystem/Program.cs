//using System;



//public class Program
//{
//    public static async Task Main(string[] args)
//    {
//        var taskManager = new TaskManager(3); // 限制最大并发线程数为3

//        // 创建三个任务队列
//        var queue1 = taskManager.CreateQueue();
//        var queue2 = taskManager.CreateQueue();
//        var queue3 = taskManager.CreateQueue();

//        // 向队列1添加任务
//        queue1.EnqueueTask(async () =>
//        {
//            Console.WriteLine("Queue1 Task1 Start");
//            await Task.Delay(1000); // 模拟异步工作
//            Console.WriteLine("Queue1 Task1 End");
//        });

//        queue1.EnqueueTask(async () =>
//        {
//            Console.WriteLine("Queue1 Task2 Start");
//            await Task.Delay(1000); // 模拟异步工作
//            Console.WriteLine("Queue1 Task2 End");
//        });

//        // 向队列2添加任务
//        queue2.EnqueueTask(async () =>
//        {
//            Console.WriteLine("Queue2 Task1 Start");
//            await Task.Delay(1000); // 模拟异步工作
//            Console.WriteLine("Queue2 Task1 End");
//        });

//        // 向队列3添加任务
//        queue3.EnqueueTask(async () =>
//        {
//            Console.WriteLine("Queue3 Task1 Start");
//            await Task.Delay(1000); // 模拟异步工作
//            Console.WriteLine("Queue3 Task1 End");
//        });

//        // 等待所有任务完成
//        await Task.Delay(5000);
//    }
//}

