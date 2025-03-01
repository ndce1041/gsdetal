using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gsdetal.Services;

namespace gsdetal.SpiderTemplate
{
    interface IOriginTemplate
    {
        /// <summary>
        /// 所有的爬虫模板都需要实现这个接口
        ///
        /// </summary>
        /// <param name="url"></param>
        public Task<string> GetBody(string url);  // 根据url获取页面内容

        public Task AnalyseBody(string body, IUrlService urlService, IItemdetailService itemService);  // 根据html解析页面内容 同时保存

        public Task Run();  // 运行爬虫

        public Func<Task> GetTask(string url);  //输出为Task
    }
}
