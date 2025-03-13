using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System.Windows.Controls;
using AngleSharp;
using AngleSharp.Css;

using gsdetal.Models;
using System.Collections.Concurrent;


namespace gsdetal.MainLogic
{
    internal class spLogic
    {
        // 固定2个线程
        BlockingCollection<Func<Task>> taskList1 = new();  // 任务队列1
        BlockingCollection<Func<Task>> taskList2 = new();  // 任务队列2

        Thread thread1;
        Thread thread2;


        private int totalTasks;
        private int completedTasks;

        public spLogic()
        {
            this.totalTasks = 0;
            this.completedTasks = 0;

            thread1 = new Thread(async () => ProcessTasks(taskList1));
            thread2 = new Thread(async () => ProcessTasks(taskList2));

        }

        public void AddTask(List<Func<Task>> task)
        {

            totalTasks += task.Count;
            // 不同组任务需要隔离 防止频率过高
            if (taskList1.Count <= taskList2.Count)
            {
                
                for (int i = 0; i < task.Count; i++)
                {
                    taskList1.Add(task[i]);
                }
            }
            else
            {
                for (int i = 0; i < task.Count; i++)
                {
                    taskList2.Add(task[i]);
                }
            }
        }

        public void AddTask(Func<Task> task)
        {
            totalTasks++;
            // 不用隔离时
            if (taskList1.Count <= taskList2.Count)
            {
                taskList1.Add(task);
            }
            else
            {
                taskList2.Add(task);
            }
        }

        public void StartProcessing()
        {
            // 检测线程是否已经启动
            if (thread1.ThreadState == ThreadState.Unstarted)
            {
                thread1.Start();
            }
            if (thread2.ThreadState == ThreadState.Unstarted)
            {
                thread2.Start();
            }
        }

        public void StopProcessing()
        {
            taskList1.CompleteAdding();
            taskList2.CompleteAdding();
        }


        private async Task ProcessTasks(BlockingCollection<Func<Task>> taskList)
        {
            foreach (var task in taskList.GetConsumingEnumerable())
            {
                await task();
                Interlocked.Increment(ref completedTasks);
                //Console.WriteLine($"Completed {completedTasks} of {totalTasks} tasks.");
            }
        }


        public void ResetCounter()
        {
            totalTasks = 0;
            completedTasks = 0;
        }


        public int GetCompletedTasks()
        {
            return completedTasks;
        }

        public int GetTotalTasks()
        {
            return totalTasks;
        }



        public void CompleteAdding()
        {
            taskList1.CompleteAdding();
            taskList2.CompleteAdding();
        }

    }
    
}
