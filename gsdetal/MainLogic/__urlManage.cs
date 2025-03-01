using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using IOFile = System.IO.File;

namespace gsdetal.MainLogic
{
    internal class __urlManage
    {
    }


    public class TextFileProcessor
    {
        private string filePath;

        public TextFileProcessor(string filePath)
        {
            // 初始化文件路径
            this.filePath = filePath;
            // 确保文件存在，如果不存在则创建一个空文件
            if (!IOFile.Exists(filePath))
            {
                IOFile.Create(filePath).Close();
            }
        }

        /// <summary>
        /// 从文件中按行读取内容并返回为 List<string>
        /// </summary>
        /// <returns>文件内容的列表</returns>
        public List<string> ReadLines()
        {
            // 按行读取文件内容
            return IOFile.ReadAllLines(filePath).ToList();
        }

        /// <summary>
        /// 将给定的字符串添加到文件中
        /// </summary>
        /// <param name="line">要添加的字符串</param>
        public void AddLine(string line)
        {
            // 读取当前文件内容
            var lines = ReadLines();
            // 添加新内容
            lines.Add(line);
            // 保存回文件
            SaveLines(lines);
        }

        /// <summary>
        /// 从文件中删除指定的字符串
        /// </summary>
        /// <param name="line">要删除的字符串</param>
        public void RemoveLine(string line)
        {
            // 读取当前文件内容
            var lines = ReadLines();
            // 删除指定内容
            lines.RemoveAll(l => l == line);
            // 保存回文件
            SaveLines(lines);
        }

        /// <summary>
        /// 将列表内容保存到文件
        /// </summary>
        /// <param name="lines">要保存的列表</param>
        private void SaveLines(List<string> lines)
        {
            // 将列表内容写入文件
            IOFile.WriteAllLines(filePath, lines);
        }
    }
}
