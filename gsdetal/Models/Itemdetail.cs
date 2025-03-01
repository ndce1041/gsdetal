using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gsdetal.Models
{
    public class Itemdetail
    {
        [Key]
        public string url { get; set; }
        public string color { get; set; }  // 颜色
        public string size { get; set; }  // 尺码
        public string state { get; set; }  // 状态  是否售空
        public string thumbnailurl { get; set; }  // 缩略图
        public string thumbnailpath { get; set; }  // 缩略图路径
        public string tip { get; set; }  // 备注
        public string temp { get; set; }  // 临时变量

    }
}
