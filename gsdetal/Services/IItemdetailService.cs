using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using gsdetal.Models;

namespace gsdetal.Services
{
    internal interface IItemdetailService
    {

        // search
        public List<Itemdetail> GetItemDetailByUrl();  // 获取商品详情

        // add
        public void AddItemDetailByUrl( List<Itemdetail> itemdetails);  // 添加商品详情

        // update
        public void UpdateItemDetailByUrl(List<Itemdetail> itemdetails);  // 更新商品详情
        public void UpdateTipByUrl(String tip);  // 更新商品备注
        // delete
        public void DeleteItemDetailByUrl(List<String> urls);  // 删除商品详情
    }
}
