//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;
//using AngleSharp.Dom;
//using AngleSharp.Html.Parser;
//using gsdetal.MainLogic;

//using gsdetal.Models;
//using NLog;
//using System.Net;
//using System.Net.Http;

//namespace gsdetal.SpiderTemplate
//{
//    internal class NO1
//    {
//        // https://runway-webstore.com/ap/item/i/m/0225203054  runway-webstore商品详情页

//        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
//        public NO1() 
//        {
            
//        }

//        public async void  GetBody(string url)
//        {
//            string body = "";

//            try
//            {
//                var response = await client.GetAsync(url);


//                if (response.IsSuccessStatusCode)
//                {
//                    body = await response.Content.ReadAsStringAsync();
//                    //ansJson += body;
//                }
//            }
//            catch (Exception e)
//            {
//                ansJson += "\nException Caught!" + e.Message;


//            }

//            return body;
//        }

//        public void Run(string body) {


//            var parser = new HtmlParser();
//            var document = parser.ParseDocument(body);

//            Logger.Info("start parse");

//            var item_detail = document.QuerySelector(".item_detail_box");
//            if (item_detail == null)
//            {
//                ansJson += body;
//                ansJson += "no detail box";
//                throw new Exception("no detail box");
//            }
//            var prise = item_detail.QuerySelector(".item_detail_pricebox");
//            var name = item_detail.QuerySelector("h1.item_detail_productname");
//            var state = item_detail.QuerySelector(".icon.item_detail_icon.item_status");
//            var info_percolor = item_detail.QuerySelector("div.shopping_area.cart_type_popup")?.QuerySelectorAll("li");

//            if (info_percolor == null) { throw new Exception("no percolor info"); }




//            //if (info_percolor == null || prise == null || name == null || state == null)
//            //{

//            //    ansJson += "no percolor or prise info\n";
//            //    ansJson += info_percolor == null;
//            //    ansJson += prise == null;
//            //    ansJson += name == null;
//            //    ansJson += state == null;

//            //    ansJson += item_detail.TextContent;
//            //    throw new Exception("no percolor or prise info");
//            //}




//            item.Price = prise == null ? null : TraverseAndConcatenateText(prise);
//            item.Name = name == null ? null : name.TextContent;
//            item.State = state == null ? null : TraverseAndConcatenateText(state);
//            item.Url = url;


//            // percolor -- persize
//            Pertype typ;
//            ansJson += "for begin\n";
//            foreach (var element in info_percolor)
//            {
//                // get color
//                var color_info = element.QuerySelector("dd")?.TextContent;
//                if (color_info == null)
//                {
//                    continue;
//                }
//                typ = new();
//                typ.Style = color_info;


//                // get size list
//                var sizes = element.QuerySelector(".choose_item")?.QuerySelectorAll("li");
//                if (sizes == null) { continue; };

//                foreach (var size in sizes)
//                {
//                    Persize sz = new();
//                    sz.Size = size.QuerySelector(".size")?.TextContent;
//                    sz.State = size.QuerySelector("dd.zaiko")?.QuerySelector("span")?.TextContent;
//                    sz.Soldtime = size.QuerySelector("span.shippingdate")?.TextContent;

//                    typ.ps.Add(sz);
//                }


//                item.Pt.Add(typ);
//            }
//            ansJson += "end\n"; //+ JsonSerializer.Serialize(items);
//            items.Add(item);
//        }



//    }
//}
