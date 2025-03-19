using System;
using System.Collections.Generic;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using gsdetal.Models;
using gsdetal.Services;
using gsdetal.SpiderTemplate;
using gsdetal.Models;
using gsdetal.Services;

namespace gsdetal.SpiderTemplate.Imple
{
    internal class SNIDELTemplate : AbstractOriginTemplate
    {
        public override string Match { get; set; } = @"^https:\/\/snidel\.com\/Form\/Product\/ProductDetail\.aspx\?shop=0&pid=([^&]+)&vid=&bid=SND01&cid=&_type=&cat=&swrd=";
        public override string TemplateName { get; set; } = "SNIDEL";

        private const string MainContainerSelector = ".block-right";
        private const string VariantListSelector = ".list-valiation > li";

        public override async Task AnalyseBody(string url, object body, IUrlService urlService, IItemdetailService itemService)
        {
            var parser = new HtmlParser();
            var document = await parser.ParseDocumentAsync((string)body);

            // 提取基础信息
            var mainContainer = document.QuerySelector(MainContainerSelector);
            if (mainContainer == null) throw new Exception("主容器未找到");

            var urlInfo = new gsdetal.Models.Url
            {
                url = url,
                title = mainContainer.QuerySelector("h2.ttl")?.TextContent.Trim(),
                price = mainContainer.QuerySelector(".price")?.TextContent.Replace("税込", "").Trim(),
                status = ExtractStockStatus(mainContainer)
            };
            urlService.UpdateUrl(urlInfo);

            // 提取多规格信息（平级结构）
            foreach (var variant in document.QuerySelectorAll(VariantListSelector))
            {
                var item = new Itemdetail
                {
                    url = url,
                    color = variant.QuerySelector(".color")?.TextContent.Trim(),
                    size = variant.QuerySelector(".size")?.TextContent.Replace("サイズ相当", "").Trim(),
                    state = variant.QuerySelector(".stockcheck_count__nostock") != null ? "SOLDOUT" : "在庫あり",
                    thumbnailurl = variant.QuerySelector(".variationImages")?.GetAttribute("src"),
                    thumbnailpath = null
                };

                // 特殊处理嵌套库存状态
                var stockSpan = variant.QuerySelector(".stock > span > span");
                if (stockSpan != null && item.state == null)
                    item.state = stockSpan.TextContent.Trim();

                itemService.UpdateItemDetail(item);
            }
        }

        // 处理全局库存状态（如页面顶部提示）
        private string ExtractStockStatus(IElement container)
        {
            var globalStock = container.QuerySelector(".stockcheck_count");
            return globalStock?.TextContent switch
            {
                "SOLDOUT" => "全品完売",
                _ => "在庫あり"
            };
        }
    }
}