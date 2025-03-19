using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using gsdetal.Commands;
using gsdetal.DBViewModel;
using gsdetal.Models;
using gsdetal.Services;
using gsdetal.Services.Implementations;

namespace gsdetal.SpiderTemplate
{
    abstract class AbstractOriginTemplate : IOriginTemplate
    {
        public abstract string Match { get; set; }
        public abstract string TemplateName { get; set; }

        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);  // 实例锁
        protected IItemdetailService _itemService;
        protected IUrlService _urlService;
        static HttpClient? client;

        public Itemdetail? tochange;   // 保存当前正在处理的itemdetail   可传入方便修改半成品

        public MyDBContext dbcontext;   

        public String _encodingStr { get; set; } = "utf-8";

        Encoding encoding;

        protected string _url;

        protected AbstractOriginTemplate()
        {
            dbcontext = new MyDBContext();
            _itemService = new ItemdetailService(dbcontext); 
            _urlService = new UrlService(dbcontext);



        }

        protected HttpClient GetHttpClient()
        {
            // 单例获取HttpClient
            if (client != null)
            {
                return client;
            }

            int timeout = 30;
            string? UA = null;   // TODO 从配置文件中读取

            string DefaultUA = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/134.0.0.0 Safari/537.36 Edg/134.0.0.0";

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

            client = new HttpClient(new HttpClientHandler
            {
                // 启用HTTP/2.0
                MaxConnectionsPerServer = 10, // 可以根据需要调整
                SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13,
                AutomaticDecompression = DecompressionMethods.All
            });

            client.DefaultRequestVersion = HttpVersion.Version20; // 设置默认版本为HTTP/2.0

            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.DefaultRequestHeaders.UserAgent.ParseAdd(UA ?? DefaultUA);
            client.DefaultRequestHeaders.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br, zstd");
            client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("zh-CN,zh;q=0.9,en;q=0.8");
            client.DefaultRequestHeaders.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue
            {
                NoCache = true,
            };
            client.DefaultRequestHeaders.ConnectionClose = false; // 保持连接

            // 添加更多浏览器常见的请求头
            client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
            client.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");

            client.DefaultRequestHeaders.Add("Sec-Ch-Ua", "\"Chromium\";v=\"134\", \"Not:A-Brand\";v=\"24\", \"Microsoft Edge\";v=\"134\"");
            client.DefaultRequestHeaders.Add("Sec-Ch-Ua-Mobile", "?0");
            client.DefaultRequestHeaders.Add("Sec-Ch-Ua-Platform", "\"Windows\"");

            client.DefaultRequestHeaders.Add("Pragma", "no-cache");
            client.DefaultRequestHeaders.Add("Priority", "u=0, i");

            return client;
        }



        public virtual async Task<Object> GetBody(string url)
        {
            /// 必要时可以重写这个方法

            // 用MATH正则表达式匹配url  并获取匹配的字符段
            MatchCollection matches = Regex.Matches(url, Match);
            if (matches.Count == 0)
            {
                throw new Exception("URL不匹配");
            }
            string match = matches[0].Value;
            url = match;


            HttpClient client = GetHttpClient();



            var response = await client.GetAsync(url);

            encoding = Encoding.GetEncoding(_encodingStr);

            if (response.IsSuccessStatusCode)
            {
                return  encoding.GetString( await response.Content.ReadAsByteArrayAsync());
                //ansJson += body;
            }
            else
            {
                // 返回错误信息
                return response.StatusCode;
            }
        }
        public abstract Task AnalyseBody(string _url ,Object body, IUrlService urlService, IItemdetailService itemService);
        /// <summary>
        /// 解析页面内容并保存
        /// tochange 为传入的可能附带信息的itemdetail 当只传入url时为null
        /// </summary>
        /// <returns></returns>




        public Func<Task> GetTask(string url)
        {
            return async () =>
            {
                await semaphore.WaitAsync(); // 异步等待锁
                try
                {
                    await AnalyseBody(url,await GetBody(url), _urlService, _itemService);
                }
                finally
                {
                    semaphore.Release();
                }
            };
        }

    }
}
