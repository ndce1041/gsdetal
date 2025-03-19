using System;
using System.Collections.Generic;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using gsdetal.Models;
using gsdetal.Services;

namespace gsdetal.SpiderTemplate.Imple
{
    internal class ZozoProductTemplate : AbstractOriginTemplate
    {
        // 配置项
        public override string Match { get; set; } = @"^https:\/\/zozo\.jp\/shop\/\w+\/goods-sale\/\d+\/";
        public override string TemplateName { get; set; } = "ZOZO_V1";

        // 日语-中文映射字典
        private readonly Dictionary<string, string> _colorMap = new()
        {
            {"ホワイト", "白色"}, {"ブラック", "黑色"}, {"イエロー", "黄色"},
            {"ブルー", "蓝色"}, {"ピンク", "粉色"}, {"グレー", "灰色"}
        };

        private readonly Dictionary<string, string> _statusMap = new()
        {
            {"在庫あり", "有库存"}, {"予約可能", "可预订"},
            {"完売しました", "已售罄"}, {"在庫なし", "无库存"}
        };


        public ZozoProductTemplate(): base()
        {
            _encodingStr = "shift_jis";
        }


        public override async Task AnalyseBody(string url, object body, IUrlService urlService, IItemdetailService itemService)
        {
            var parser = new HtmlParser();
            var document = await parser.ParseDocumentAsync((string)body);

            // 提取基础信息
            var mainSection = document.QuerySelector("#goodsRight");
            if (mainSection == null) throw new Exception("主容器未找到");

            // 处理命名空间冲突
            var urlInfo = new gsdetal.Models.Url
            {
                url = url,
                title = mainSection.QuerySelector("h1.p-goods-information__heading")?.TextContent.Trim(),
                price = ExtractPrice(mainSection),
                status = mainSection.QuerySelector(".p-goods-information-status__heading")?.TextContent.Trim()
            };
            urlService.UpdateUrl(urlInfo);

            // 提取多规格信息
            var variantGroups = mainSection.QuerySelectorAll("dl[data-goods-skus='group']");
            foreach (var group in variantGroups)
            {
                var colorElement = group.QuerySelector(".p-goods-add-cart__color");
                var color = colorElement?.TextContent.Trim();

                // 颜色中日转换
                if (_colorMap.TryGetValue(color ?? "", out var cnColor))
                    color = cnColor;

                var sizeItems = group.QuerySelectorAll("li.p-goods-add-cart-list__item");
                foreach (var item in sizeItems)
                {
                    var detail = new Itemdetail
                    {
                        url = url,
                        color = color,
                        size = item.GetAttribute("data-size")?.ToUpper(),
                        state = ExtractStockStatus(item),
                        thumbnailurl = ExtractImageUrl(group)
                    };

                    // 状态中日转换
                    if (_statusMap.TryGetValue(detail.state ?? "", out var cnStatus))
                        detail.state = cnStatus;

                    itemService.UpdateItemDetail(detail);
                }
            }
        }

        // 价格提取逻辑
        private string ExtractPrice(IElement container)
        {
            var priceNode = container.QuerySelector(".p-goods-information__price--discount");
            var originalPrice = container.QuerySelector(".u-text-style-strike")?.TextContent;
            var salePrice = priceNode?.TextContent.Replace("税込", "").Trim();

            return $"{originalPrice}→{salePrice}";
        }

        // 库存状态提取
        private string ExtractStockStatus(IElement item)
        {
            return item.QuerySelector(".p-goods-add-cart-stock span:last-child")?.TextContent.Trim()
                   ?? item.QuerySelector(".p-goods-information-action__stock-status")?.TextContent.Trim();
        }

        // 图片URL处理
        private string ExtractImageUrl(IElement group)
        {
            var imgSrc = group.QuerySelector(".o-responsive-thumbnail__image")?.GetAttribute("src");
            return imgSrc?.StartsWith("//") == true ? $"https:{imgSrc}" : imgSrc;
        }
    }
}