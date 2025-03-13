using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gsdetal.Services;
using AngleSharp.Html.Parser;
using gsdetal.Models;
using System.Diagnostics;

namespace gsdetal.SpiderTemplate.Imple
{
    internal class NO1Template : AbstractOriginTemplate
    {
        public override string Match { get; set; } = @"^https:\/\/runway-webstore\.com\/ap\/item\/i\/m\/\d{10}$";   // 重要标识符  自身对应url的正则表达式  需要排除冗余信息
        public override string TemplateName { get; set; } = "NO1";   // 重要标识符  模板名称 可以任意取 但是要保证唯一性

        SpiderTools tools = new SpiderTools();    // 工具类   其中只有一个方法  TraverseAndConcatenateText  用于遍历节点并拼接文本  当信息分散在层次结构中时使用


        public NO1Template() : base() { }  // 构造函数

        /// <summary>
        /// 解析页面内容
        /// </summary>
        /// <param name="_url">目标url,作为主键与其他信息存储在数据库</param>
        /// <param name="body">一般为string 需要显示转换</param>
        /// <param name="urlService">对于url模型的数据库存储服务</param>
        /// <param name="itemService">对于item模型的数据库存储服务</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override async Task AnalyseBody(string _url ,object body,IUrlService urlService, IItemdetailService itemService)
        {
            


            // 解析页面内容
            var parser = new HtmlParser();
            var document = await parser.ParseDocumentAsync((string)body);

            //Logger.Info("start parse");   // 需要加入日志 用于调试




            // 商品基础信息  记录在url中
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


            urlService.UpdateUrl(urlinf);  /// ！！更新url信息



            //////////////////////////






            //商品详细信息  每个颜色-尺码组合为一个数据行
            foreach (var element in info_percolor)
            {
                // get color
                var color_info = element.QuerySelector("dd")?.TextContent;
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
                    item.thumbnailurl = "https:" + element.QuerySelector("img")?.GetAttribute("src");   // 缩略图url 可能有省略 需要补全为正常url格式   为每个颜色对应的缩略图
                    item.thumbnailpath = null;  // 缩略图路径 一定为空
                    item.tip = null;  // 备注  一定为空
                    item.temp = null;  // 临时变量 一定为空


                    itemService.UpdateItemDetail(item);  /// ！！更新item信息
                   
                }
            }
        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
    }
    
}
