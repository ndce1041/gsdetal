//using System;



//public class Program
//{
//    public static async Task Main(string[] args)
//    {
//        var taskManager = new TaskManager(3); // ������󲢷��߳���Ϊ3

//        // ���������������
//        var queue1 = taskManager.CreateQueue();
//        var queue2 = taskManager.CreateQueue();
//        var queue3 = taskManager.CreateQueue();

//        // �����1�������
//        queue1.EnqueueTask(async () =>
//        {
//            Console.WriteLine("Queue1 Task1 Start");
//            await Task.Delay(1000); // ģ���첽����
//            Console.WriteLine("Queue1 Task1 End");
//        });

//        queue1.EnqueueTask(async () =>
//        {
//            Console.WriteLine("Queue1 Task2 Start");
//            await Task.Delay(1000); // ģ���첽����
//            Console.WriteLine("Queue1 Task2 End");
//        });

//        // �����2�������
//        queue2.EnqueueTask(async () =>
//        {
//            Console.WriteLine("Queue2 Task1 Start");
//            await Task.Delay(1000); // ģ���첽����
//            Console.WriteLine("Queue2 Task1 End");
//        });

//        // �����3�������
//        queue3.EnqueueTask(async () =>
//        {
//            Console.WriteLine("Queue3 Task1 Start");
//            await Task.Delay(1000); // ģ���첽����
//            Console.WriteLine("Queue3 Task1 End");
//        });

//        // �ȴ������������
//        await Task.Delay(5000);
//    }
//}

