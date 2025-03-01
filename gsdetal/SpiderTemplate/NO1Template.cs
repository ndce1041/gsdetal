using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gsdetal.Services;
using AngleSharp.Html.Parser;
using gsdetal.Models;

namespace gsdetal.SpiderTemplate
{
    internal class NO1Template : AbstractOriginTemplate
    {

        SpiderTools tools = new SpiderTools();
        public NO1Template() : base()
        {
            
        }

        public override async Task AnalyseBody(string body,IUrlService urlService, IItemdetailService itemService)
        {
            // 解析页面内容

            var parser = new HtmlParser();
            var document = parser.ParseDocument(body);

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

            
            Url urlinf = new();
            urlinf.url = _url;
            urlinf.price = prise == null ? null : tools.TraverseAndConcatenateText(prise);
            urlinf.title = name == null ? null : name.TextContent;
            urlinf.status = state == null ? null : tools.TraverseAndConcatenateText(state);

            urlService.UpdateUrl(urlinf);  /// ！！更新url信息

            //////////////////////////


            // percolor -- persize
            Pertype typ;
            ansJson += "for begin\n";
            foreach (var element in info_percolor)
            {
                // get color
                var color_info = element.QuerySelector("dd")?.TextContent;
                if (color_info == null)
                {
                    continue;
                }
                typ = new();
                typ.Style = color_info;


                // get size list
                var sizes = element.QuerySelector(".choose_item")?.QuerySelectorAll("li");
                if (sizes == null) { continue; };

                foreach (var size in sizes)
                {
                    Persize sz = new();
                    sz.Size = size.QuerySelector(".size")?.TextContent;
                    sz.State = size.QuerySelector("dd.zaiko")?.QuerySelector("span")?.TextContent;
                    sz.Soldtime = size.QuerySelector("span.shippingdate")?.TextContent;

                    typ.ps.Add(sz);
                }


                item.Pt.Add(typ);
            }
            ansJson += "end\n"; //+ JsonSerializer.Serialize(items);
            items.Add(item);
        }





        // 保存到数据库
    }
    }
}
