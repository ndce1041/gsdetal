using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using gsdetal.Services;
using gsdetal.Models;

using System.IO;


namespace gsdetal.SpiderTemplate
{
    class ThumbnailTemplate : AbstractOriginTemplate
    {
        /// <summary>
        /// 缩略图模板
        /// </summary>
        /// 

        string filetype;
        string PATH = "images";

        public ThumbnailTemplate() : base() {
            
        }

        public override async Task<object> GetBody(string url)
        {
            if (tochange == null)
            {
                throw new ArgumentNullException(nameof(tochange));
            }
            url = tochange.thumbnailurl;

            var client = GetHttpClient();
            var response = await client.GetAsync(url);

            // 从url结尾获取文件后缀
            filetype = url.Split('.').Last();


            response.EnsureSuccessStatusCode();

            if (response.Content.Headers.ContentType?.MediaType?.StartsWith("image/") == true)
            {
                var imageBytes = await response.Content.ReadAsByteArrayAsync();
                return imageBytes;
            }
            else
            {
                throw new InvalidOperationException("The URL does not point to an image.");
            }
        }



        public override async Task AnalyseBody(Object body, Itemdetail? tochange, IUrlService urlService, IItemdetailService itemService)
        {
            /// 保存缩略图并更新数据库
            /// 

            if (tochange == null)
            {
                throw new ArgumentNullException(nameof(tochange));
            }

            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }
            // 以时间戳 + 随机数命名
            string fileName = $"{DateTimeOffset.Now.ToUnixTimeMilliseconds()}-{new Random().Next(1000, 9999)}.{filetype}";
            string filePath = "./" + PATH + "/" + fileName;
            await File.WriteAllBytesAsync(filePath, (byte[])body);



            tochange.thumbnailpath = filePath;


            itemService.UpdateItemDetail(tochange);


        }

    }
}
