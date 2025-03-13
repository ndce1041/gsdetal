using gsdetal.DBViewModel;
using gsdetal.Services;
using gsdetal.SpiderTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using gsdetal.Services.Implementations;

namespace gsdetal.MainLogic
{
    class runTime
    {
        /// 定义启动 爬取 资源准备 展示
        /// 

        MyDBContext context = new();
        IItemdetailService itemService;
        IUrlService urlService;
        IThumbnailService thumbnailService;

        public Dictionary<string, string> NameToMatch = new Dictionary<string, string>();

        public Dictionary<string, IOriginTemplate> NameToTemplate = new Dictionary<string, IOriginTemplate>();

        public urlManage umr;

        public spLogic sp;

        public runTime()
        {
            // 初始化模板
            InitializeTemplates();

            // 初始化链接管理
            umr = new urlManage(NameToMatch);

            // 初始化服务
            itemService = new ItemdetailService(context);
            urlService = new UrlService(context);
            thumbnailService = new ThumbnailService(context);

            // 初始化爬虫工作器
            sp = new spLogic();
        }

        public void run()
        {
            // 
            StartHttpTask();

            // 等待任务完成  实时更新显示
            while (true)
            {
                if (sp.GetCompletedTasks() != sp.GetTotalTasks())
                {
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    break;
                }
            }


            // 开始缩略图任务
            StartThumbnailTask();

            // 等待任务完成  实时更新显示
            // TODO



        }


        private void InitializeTemplates()
        {
            // 获取当前程序集
            Assembly assembly = Assembly.GetExecutingAssembly();

            // 获取命名空间中的所有类
            var types = assembly.GetTypes()
                .Where(t => t.Namespace == "gsdetal.SpiderTemplate.Imple" && typeof(IOriginTemplate).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            // 遍历并初始化类
            foreach (var type in types)
            {
                IOriginTemplate instance = (IOriginTemplate)Activator.CreateInstance(type);
                NameToMatch.Add(instance.TemplateName, instance.Match);
                NameToTemplate.Add(instance.TemplateName, instance);
            }
        }



        public void StartHttpTask()
        {
            /// 多线程任务配置


            sp.ResetCounter();

            // 获取所有URL
            var urls = urlService.GetAllUrllwithGroup();

            foreach (var type in urls)
            {
                var template = type.Key;
                if (NameToTemplate.ContainsKey(template))
                {
                    var templateInstance = NameToTemplate[template];
                    var urlList = type.Value;
                    var taskList = new List<Func<Task>>();
                    foreach (var url in urlList)
                    {
                        taskList.Add(templateInstance.GetTask(url.url));
                    }
                    sp.AddTask(taskList);
                }
            }

            sp.StartProcessing();

        }


        public void StartThumbnailTask()
        {
            /// 多线程任务配置
            /// 

            sp.ResetCounter();

            // 获取所有URL
            var urls = thumbnailService.GetUrlThatNoFile();
            var template = NameToTemplate["Thumbnail"];
            foreach (var url in urls)
            {
                sp.AddTask(template.GetTask(url));
            }

            sp.StartProcessing();
        }



    }
}
