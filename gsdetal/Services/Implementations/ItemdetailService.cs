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
        public void UpdateItemDetailByUrl(List<Itemdetail> itemdetails)
        {
            foreach (var itemdetail in itemdetails)
            {
                var existingItemdetail = _context.Itemdetails.Find(itemdetail.url);
                if (existingItemdetail != null)
                {
                    existingItemdetail.color = itemdetail.color;
                    existingItemdetail.size = itemdetail.size;
                    existingItemdetail.state = itemdetail.state;
                    existingItemdetail.thumbnailurl = itemdetail.thumbnailurl;
                    existingItemdetail.thumbnailpath = itemdetail.thumbnailpath;
                    existingItemdetail.tip = itemdetail.tip;
                    existingItemdetail.temp = itemdetail.temp;

                    _context.Itemdetails.Update(existingItemdetail);
                }
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


