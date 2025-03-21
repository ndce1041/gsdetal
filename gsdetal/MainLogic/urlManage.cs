﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using gsdetal.DBViewModel;
using gsdetal.Models;
using gsdetal.Services;
using gsdetal.Services.Implementations;

namespace gsdetal.MainLogic
{
    internal class urlManage
    {
        private static IUrlService _urlService = new UrlService(new MyDBContext());

        // 正则表达式 为url匹配模板
        public Dictionary<string, string> TemplateDict;
        //{
        //    { "NO1", @"^https:\/\/runway-webstore\.com\/ap\/item\/i\/m\/\d{10}$"}
        //};



        public urlManage(Dictionary<string, string> keyValuePairs)
        {
            TemplateDict = keyValuePairs;
        }

        /// <summary>
        /// 获取所有URL并按模板分组
        /// </summary>
        /// <returns>按模板分组的URL字典</returns>
        public Dictionary<string, List<Url>> GetAllUrlsWithGroup()
        {
            return _urlService.GetAllUrllwithGroup();
        }

        /// <summary>
        /// 获取某一模板下的所有URL
        /// </summary>
        /// <param name="group">模板类型</param>
        /// <returns>模板下的URL列表</returns>
        public List<Url> GetUrlsByGroup(string group)
        {
            return _urlService.GetUrlByGroup(group);
        }

        /// <summary>
        /// 添加新的URL
        /// </summary>
        /// <param name="url">URL字符串</param>
        public void AddUrl(string url)
        {
            // 处理URL 去除多余的空格 切除参数部分
            url = url.Trim();
            

            // 判断URL是否已经存在
            if (_urlService.IsUrlExist(url))
            {
                // TODO URL已经存在 触发通知
                return;
            }


            string templatetype = string.Empty;
            foreach (var item in TemplateDict)
            {
                // 使用template匹配url
                if (Regex.IsMatch(url, item.Value))
                {
                    templatetype = item.Key;
                    break;
                }
            }

            if (templatetype == string.Empty)
            {
                // TODO 未匹配到模板 触发通知
                return;
            }

            var newUrl = new Url
            {
                url = Regex.Match(url, TemplateDict[templatetype]).Groups[1].Value,
                templatetype = templatetype,
                order = string.Empty,
                title = string.Empty,
                type = string.Empty,
                price = string.Empty,
                status = string.Empty,
            };
            _urlService.AddUrl(newUrl);


            // TODO 通知爬虫工作器添加新的任务


        }



        /// <summary>
        /// 更新现有的URL
        /// </summary>
        /// <param name="url">URL对象</param>
        public void UpdateUrl(Url url)
        {
            _urlService.UpdateUrl(url);
        }

        /// <summary>
        /// 删除指定的URL
        /// </summary>
        /// <param name="url">URL字符串</param>
        public void RemoveUrl(string url)
        {
      
            if (url != null)
            {
                _urlService.RemoveUrl(url);
            }
        }
    }
}

