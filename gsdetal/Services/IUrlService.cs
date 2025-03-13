using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using gsdetal.Models;

namespace gsdetal.Services
{
    internal interface IUrlService
    {
        public Dictionary<string, List<Url>> GetAllUrllwithGroup();  // 按模板分组获取所有url

        public List<Url> GetUrlByGroup(string group);  // 获取某一模板下的所有url

        public List<Url> GetAllUrlOrdered();
        public void AddUrl(Url url);  // 添加url

        public void UpdateUrl(Url url);  // 更新url

        public void RemoveUrl(String url);

        public bool IsUrlExist(String url);
    }
}
