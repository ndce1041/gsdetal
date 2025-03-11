﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        protected IItemdetailService _itemService;
        protected IUrlService _urlService;
        static HttpClient? client;

        public Itemdetail? tochange;   // 保存当前正在处理的itemdetail   可传入方便修改半成品

        protected string _url;

        protected AbstractOriginTemplate()
        {
            var dbcontext = new MyDBContext();
            _itemService = new ItemdetailService(dbcontext); 
            _urlService = new UrlService(dbcontext);



        }

        protected HttpClient GetHttpClient()
        {
            ///
            /// 单例获取HttpClient
            ///
            if (client != null)
            {
                return client;
            }

            int timeout = 30;
            string? UA = null;   // TODO 从配置文件中读取

            string DefaultUA = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/132.0.0.0 Safari/537.36";

            client = new HttpClient();
            client.Timeout = timeout == null ? TimeSpan.FromSeconds(30) : TimeSpan.FromSeconds((int)timeout);
            client.DefaultRequestHeaders.Add("User-Agent", UA == null ? DefaultUA : UA);
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br, zstd");
            client.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8");
            client.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
            client.DefaultRequestHeaders.Remove("Connection");

            return client;
        }



        public virtual async Task<Object> GetBody(string url)
        {
            /// 必要时可以重写这个方法
        

            HttpClient client = GetHttpClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
                //ansJson += body;
            }
            else
            {
                // TODO: 保存错误信息 log
                return "";
            }
        }
        public abstract Task AnalyseBody(Object body, Itemdetail? tochange, IUrlService urlService, IItemdetailService itemService);
        /// <summary>
        /// 解析页面内容并保存
        /// tochange 为传入的可能附带信息的itemdetail 当只传入url时为null
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {

            if (tochange == null)
            {
                await AnalyseBody(await GetBody(_url), null, _urlService, _itemService);
            }
            else
            {
                await AnalyseBody(await GetBody(tochange.url), tochange, _urlService, _itemService);
            }


        }

        public Func<Task> GetTask(Itemdetail itemdetail)
        {
            tochange = itemdetail;
            return Run;
        }

        public Func<Task> GetTask(string url)
        {
            _url = url;
            return Run;
        }

    }
}
