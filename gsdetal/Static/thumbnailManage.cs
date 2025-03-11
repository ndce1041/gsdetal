using gsdetal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using gsdetal.Models;
using gsdetal.Services.Implementations;
using gsdetal.DBViewModel;

namespace gsdetal.Static
{
    class ThumbnailManage
    {
        /// 根据数据库获取url管理缩略图
        /// 

        private static IItemdetailService itemdetailService = new ItemdetailService(new MyDBContext());


        public ThumbnailManage()
        {
            /// 

        }

        public List<Itemdetail> GetUrlThatEmpty()
        {
            return itemdetailService.GetItemThatEmpty();
        }


    }
}
