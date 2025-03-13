using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using gsdetal.Services;
using gsdetal.Models;

using System.IO;
using gsdetal.Services.Implementations;


namespace gsdetal.SpiderTemplate.Imple
{
    class ThumbnailTemplate : AbstractOriginTemplate
    {
        /// <summary>
        /// 缩略图模板
        /// </summary>
        /// 

        string filetype;
        string PATH = "Static";

        public override string Match { get; set; } = "null";
        public override string TemplateName { get; set; } = "Thumbnail";

        public ThumbnailService _thumbnailService;

        bool isDebug = false;

        public ThumbnailTemplate() : base() {
            _thumbnailService = new ThumbnailService(dbcontext);

        }



        public override async Task<object> GetBody(string url)
        {
            //url = "https:" + url;
            var client = GetHttpClient();
            HttpResponseMessage response = null;

            try
            {
                response = await client.GetAsync(url);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            // 等待

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



        public override async Task AnalyseBody(string _url, object body, IUrlService urlService, IItemdetailService itemService)
        {
            /// 保存缩略图并更新数据库
            /// 
            

            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            // 检测文件夹是否存在
            if (!Directory.Exists(PATH))
            {
                Directory.CreateDirectory(PATH);
            }



            // 以时间戳 + 随机数命名
            string fileName = $"{DateTimeOffset.Now.ToUnixTimeMilliseconds()}-{new Random().Next(1000, 9999)}.{filetype}";
            string filePath = "./" + PATH + "/" + fileName;
            await File.WriteAllBytesAsync(filePath, (byte[])body);




            if (isDebug)
            {
                Console.WriteLine("Thumbnail saved to " + filePath);
            }
            else
            {
                _thumbnailService.UpdateUrl(_url, fileName);
            }



        }

    }
}
