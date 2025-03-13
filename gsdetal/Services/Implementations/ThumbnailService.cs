using System;
using System.Collections.Generic;
using System.Linq;
using gsdetal.Models;
using Microsoft.EntityFrameworkCore;

using gsdetal.DBViewModel;

namespace gsdetal.Services.Implementations
{
    internal class ThumbnailService : IThumbnailService
    {
        private readonly MyDBContext _context;

        public ThumbnailService(MyDBContext context)
        {
            _context = context;
        }

        public List<string> GetUrlThatNoFile()
        {
            return _context.Thumbnails
                .Where(item => string.IsNullOrEmpty(item.thumbnailpath))
                .Select(item => item.thumbnailurl)
                .ToList();
        }

        public void AddUrl(string url)
        {
            // 判断是否为空
            if (string.IsNullOrEmpty(url))
            {
                return;
            }
            // 判断是否已经存在
            if (_context.Thumbnails.Any(item => item.thumbnailurl == url))
            {
                return;
            }
            var item = new Thumbnail { thumbnailurl = url };
            _context.Thumbnails.Add(item);
            _context.SaveChanges();
        }

        public void UpdateUrl(string url, string path)
        {
            var item = _context.Thumbnails.FirstOrDefault(t => t.thumbnailurl == url);
            if (item != null)
            {
                item.thumbnailpath = path;
                _context.SaveChanges();
            }
        }


        public void RemoveUrl(string url)
        {
            var item = _context.Thumbnails.FirstOrDefault(t => t.thumbnailurl == url);
            if (item != null)
            {
                _context.Thumbnails.Remove(item);
                _context.SaveChanges();
            }
        }

        public string? GetPathByUrl(string url)
        {
            return _context.Thumbnails
                .Where(item => item.thumbnailurl == url)
                .Select(item => item.thumbnailpath)
                .FirstOrDefault();
        }
    }
}
