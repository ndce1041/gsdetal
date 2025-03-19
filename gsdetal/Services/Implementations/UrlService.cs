using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gsdetal.DBViewModel;
using gsdetal.Models;
using Microsoft.EntityFrameworkCore;

namespace gsdetal.Services.Implementations
{
    public class UrlService : IUrlService
    {
        private readonly MyDBContext _context;

        public UrlService(MyDBContext context)
        {
            _context = context;
        }

        // 按模板分组获取所有url
        public Dictionary<string, List<Url>> GetAllUrllwithGroup()
        {
            return _context.Urls
                .GroupBy(u => u.templatetype)
                .ToDictionary(g => g.Key, g => g.ToList());
        }


        public List<Url> GetAllUrlOrdered()
        {
            // 按order字段排序  如果为空则排在最后,注意这里的order字段是string类型
            return _context.Urls
                .OrderBy(u => u.order == null ? "9999" : u.order)
                .ToList();

        }


        // 获取某一模板下的所有url
        public List<Url> GetUrlByGroup(string group)
        {
            return _context.Urls
                .Where(u => u.templatetype == group)
                .ToList();
        }

        // 添加url
        public void AddUrl(Url url)
        {
            UpdateUrl(url);
        }

        // 更新url
        public void UpdateUrl(Url url)
        {
            var existingUrl = _context.Urls.Find(url.url);
            if (existingUrl != null)
            {
                // 只有非空字段才更新
                if (url.order != null){existingUrl.order = url.order;}
                if (url.templatetype != null) { existingUrl.templatetype = url.templatetype; }
                if (url.title != null) { existingUrl.title = url.title; }
                if (url.type != null) { existingUrl.type = url.type; }
                if (url.price != null) { existingUrl.price = url.price; }
                if (url.status != null) { existingUrl.status = url.status; }
                existingUrl.updatetime = DateTime.Now;


                _context.Urls.Update(existingUrl);
                _context.Entry(existingUrl).State = EntityState.Modified;

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

            }
            else
            {
                // 添加新项
                url.updatetime = DateTime.Now;
                _context.Urls.Add(url);
                _context.SaveChanges();
            }
        }

        // 删除url
        public void RemoveUrl(string url)
        {
            var existingUrl = _context.Urls.Find(url);
            if (existingUrl != null)
            {
                _context.Urls.Remove(existingUrl);
                _context.SaveChanges();
            }
        }


        public bool IsUrlExist(string url)
        {
            return _context.Urls.Any(u => u.url == url);
        }


        public void UpdateOrderByUrl(string _url, string _order)
        {
            _context.Urls.FirstOrDefault(i => i.url == _url).order = (string)_order;
            _context.SaveChanges();
        }


    }
}
