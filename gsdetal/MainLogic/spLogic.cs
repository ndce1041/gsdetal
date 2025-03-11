using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System.Windows.Controls;
using AngleSharp;
using AngleSharp.Css;

using gsdetal.Models;


namespace gsdetal.MainLogic
{
    internal class spLogic
    {
    }


    public class ReptileWorker
    {

        HttpClient client = new();
        public string ansJson = "11";
        //public Peritem item = new();
        public ObservableCollection<Peritem> items = new();
        public List<string>? Url;
        private static readonly string DefaultUA = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/132.0.0.0 Safari/537.36";
        public ReptileWorker(int? timeout, string? UA, CookieCollection? cookies) 
        {

            this.client.Timeout = timeout == null ? TimeSpan.FromSeconds(30) : TimeSpan.FromSeconds((int)timeout);
            this.client.DefaultRequestHeaders.Add("User-Agent", UA == null ? DefaultUA : UA);
            this.client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            this.client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br, zstd");
            this.client.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8");
            this.client.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");

            this.client.DefaultRequestHeaders.Remove("Connection");

        }



        public ObservableCollection<Peritem> Controller(List<string> url_)
        {

            if (url_.Count == 0)
            {
                return items;
            }

            this.Url = url_;


            foreach (var url in this.Url)
            {
                try
                {
                    Run(url);
                }
                catch (Exception e)
                {
                    ansJson += e + "\n";
                }
            }

            //dataGrid.Dispatcher.Invoke(() =>
            //{
            //    dataGrid.ItemsSource = items;
            //});

            return items;
        }



        public async void Run(string url)
        {

            Peritem item = new();
            string body = "";

            try
            {
                var response = await client.GetAsync(url);


                if (response.IsSuccessStatusCode)
                {
                    body = await response.Content.ReadAsStringAsync();
                    //ansJson += body;
                }
            }
            catch (Exception e)
            {
                ansJson += "\nException Caught!" + e.Message;


            }

            // 开始解析


            var parser = new HtmlParser();
            var document = parser.ParseDocument(body);

            //Logger.Info("start parse");

            var item_detail = document.QuerySelector(".item_detail_box");
            if (item_detail == null)
            {
                ansJson += body;
                ansJson += "no detail box";
                throw new Exception("no detail box");
            }
            var prise = item_detail.QuerySelector(".item_detail_pricebox");
            var name = item_detail.QuerySelector("h1.item_detail_productname");
            var state = item_detail.QuerySelector(".icon.item_detail_icon.item_status");
            var info_percolor = item_detail.QuerySelector("div.shopping_area.cart_type_popup")?.QuerySelectorAll("li");

            if (info_percolor == null) { throw new Exception("no percolor info"); }




            //if (info_percolor == null || prise == null || name == null || state == null)
            //{

            //    ansJson += "no percolor or prise info\n";
            //    ansJson += info_percolor == null;
            //    ansJson += prise == null;
            //    ansJson += name == null;
            //    ansJson += state == null;

            //    ansJson += item_detail.TextContent;
            //    throw new Exception("no percolor or prise info");
            //}




            item.Price = prise == null ? null : TraverseAndConcatenateText(prise);
            item.Name = name == null ? null : name.TextContent;
            item.State = state == null ? null : TraverseAndConcatenateText(state);
            item.Url = url;


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
            
        

        private static string TraverseAndConcatenateText(IElement element)
        {
            var textBuilder = new List<string>();

            // 遍历当前元素的所有子节点
            foreach (var child in element.ChildNodes)
            {
                if (child is IElement childElement)
                {
                    // 如果是元素节点，递归处理
                    textBuilder.Add(TraverseAndConcatenateText(childElement));
                }
                else if (child is IText textNode)
                {
                    // 如果是文本节点，添加文本内容
                    textBuilder.Add(textNode.TextContent.Trim());
                }
            }

            // 返回拼接后的文本内容
            return string.Join(" ", textBuilder);
        }



    }
}
