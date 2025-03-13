using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gsdetal.Models
{
    public class Url
    {
        [Key]
        public string url { get; set; }
        public string? order { get; set; }  // 优先级
        public string templatetype { get; set; }  // 爬虫模板类型
        public string? title { get; set; }
        public string? type { get; set; }  // 商品种类
        public string? price { get; set; }
        public string? status { get; set; }
        // 记录更新时间
        public DateTime? updatetime { get; set; }
    }
}
