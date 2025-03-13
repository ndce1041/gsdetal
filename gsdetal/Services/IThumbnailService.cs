using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gsdetal.Services
{
    internal interface IThumbnailService
    {
        public List<string> GetUrlThatNoFile();  // 获取空缩略图的url
        public void AddUrl(string url);  // 添加缩略图
        public void UpdateUrl(string url, string path);  // 更新缩略图
        public void RemoveUrl(string url);  // 删除缩略图
        public string? GetPathByUrl(string url);  // 通过url获取缩略图路径
    }
}
