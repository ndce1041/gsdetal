using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gsdetal.Services;
using AngleSharp.Html.Parser;
using gsdetal.Models;
using System.Diagnostics;

namespace gsdetal.SpiderTemplate
{
    internal class NO1Template : AbstractOriginTemplate
    {

        SpiderTools tools = new SpiderTools();
        bool Debug = false;
        public NO1Template(string? Runtype) : base()
        {
            if (Runtype == "debug")
            {
                Debug = true;
            }
        }

        public NO1Template() : base()
        {
        }

        public override async Task AnalyseBody(Object body,Itemdetail? tochage,IUrlService urlService, IItemdetailService itemService)
        {
            // 解析页面内容

            var parser = new HtmlParser();
            var document = await parser.ParseDocumentAsync((string)body);

            //Logger.Info("start parse");

            var item_detail = document.QuerySelector(".item_detail_box");
            if (item_detail == null)
            {
                // log
                throw new Exception("no detail box");
            }
            var prise = item_detail.QuerySelector(".item_detail_pricebox");
            var name = item_detail.QuerySelector("h1.item_detail_productname");
            var state = item_detail.QuerySelector(".icon.item_detail_icon.item_status");
            var info_percolor = item_detail.QuerySelector("div.shopping_area.cart_type_popup")?.QuerySelectorAll("li");

            if (info_percolor == null) { throw new Exception("no percolor info"); }

            

            ///构造数据行
            Url urlinf = new();
            urlinf.url = _url;
            urlinf.price = prise == null ? null : tools.TraverseAndConcatenateText(prise);
            urlinf.title = name == null ? null : name.TextContent;
            urlinf.status = state == null ? null : tools.TraverseAndConcatenateText(state);


            if(Debug)
            {
                Console.WriteLine("url: " + urlinf.url);
                Console.WriteLine("price: " + urlinf.price);
                Console.WriteLine("title: " + urlinf.title);
                Console.WriteLine("status: " + urlinf.status);
            }
            else
            {
                urlService.UpdateUrl(urlinf);  /// ！！更新url信息
            }
            

            //////////////////////////






            // percolor -- persize
            foreach (var element in info_percolor)
            {
                // get color
                var color_info = element.QuerySelector("dd")?.TextContent;
                if (color_info == null)
                {
                    continue;
                }


                // get size list
                var sizes = element.QuerySelector(".choose_item")?.QuerySelectorAll("li");
                if (sizes == null) { continue; };

                foreach (var size in sizes)
                {

                    Itemdetail item = new();

                    // 填入数据
                    item.url = _url;
                    item.color = color_info;
                    item.size = size.QuerySelector(".size")?.TextContent;
                    item.state = size.QuerySelector("dd.zaiko")?.QuerySelector("span")?.TextContent;
                    item.thumbnailurl = "https:" + element.QuerySelector("img")?.GetAttribute("src");  
                    item.thumbnailpath = null;  // 缩略图路径 没有表示尚未缓存
                    item.tip = null;  // 备注
                    item.temp = null;  // 临时变量

                    if (Debug)
                    {

                        item.url = "";
                    }
                    else
                    {
                        itemService.UpdateItemDetail(item);  /// ！！更新item信息
                    }
                }
            }
        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      




        // 保存到数据库
    }
    
}
