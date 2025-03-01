using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;

namespace gsdetal.SpiderTemplate
{
    public class SpiderTools
    {
        public string TraverseAndConcatenateText(IElement element)
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
