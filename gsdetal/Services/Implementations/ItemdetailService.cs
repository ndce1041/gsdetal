using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gsdetal.DBViewModel;
using gsdetal.Models;

namespace gsdetal.Services.Implementations
{
    internal class ItemdetailService : IItemdetailService
    {
        private readonly MyDBContext _context;

        public ItemdetailService(MyDBContext context)
        {
            _context = context;
        }

        // 获取商品详情
        public List<Itemdetail> GetItemDetailByUrl()
        {
            return _context.Itemdetails.ToList();
        }

        // 添加商品详情
        public void AddItemDetailByUrl(List<Itemdetail> itemdetails)
        {
            _context.Itemdetails.AddRange(itemdetails);
            _context.SaveChanges();
        }

        // 更新商品详情
        public void UpdateItemDetail(Itemdetail itemdetail)
        {
            /// 以url,color, size 属性查询是否存在此组合 如果存在则更新，不存在则添加，更新只更新不为空的字段


            var existingItem = _context.Itemdetails
                .FirstOrDefault(i => i.url == itemdetail.url && i.color == itemdetail.color && i.size == itemdetail.size);

            if (existingItem != null)
            {
                // 更新不为空的字段
                if (!string.IsNullOrEmpty(itemdetail.state))
                {
                    existingItem.state = itemdetail.state;
                }
                if (!string.IsNullOrEmpty(itemdetail.thumbnailurl))
                {
                    existingItem.thumbnailurl = itemdetail.thumbnailurl;
                }
                if (!string.IsNullOrEmpty(itemdetail.thumbnailpath))
                {
                    existingItem.thumbnailpath = itemdetail.thumbnailpath;
                }
                if (!string.IsNullOrEmpty(itemdetail.tip))
                {
                    existingItem.tip = itemdetail.tip;
                }
                if (!string.IsNullOrEmpty(itemdetail.temp))
                {
                    existingItem.temp = itemdetail.temp;
                }
            }
            else
            {
                // 添加新项
                _context.Itemdetails.Add(itemdetail);
            }
            
            _context.SaveChanges();

        }

        // 更新商品备注
        public void UpdateTipByUrl(string tip)
        {
            var itemdetails = _context.Itemdetails.Where(i => i.tip == tip).ToList();
            foreach (var itemdetail in itemdetails)
            {
                itemdetail.tip = tip;
            }
            _context.SaveChanges();
        }

        // 删除商品详情
        public void DeleteItemDetailByUrl(List<string> urls)
        {
            var itemdetails = _context.Itemdetails.Where(i => urls.Contains(i.url)).ToList();
            _context.Itemdetails.RemoveRange(itemdetails);
            _context.SaveChanges();
        }
    }
}


