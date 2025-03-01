using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using gsdetal.MainLogic;

using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.Input;

namespace gsdetal.MainViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public ObservableCollection<Peritem> Items { get; set; }

        [ObservableProperty]
        private string? newurl;

        // private List<string> urls = new();

        private TextFileProcessor umr = new("./save.txt");  // 链接管理器

        private ReptileWorker reptileWorker = new(30, null, null);  // 爬虫工作器

        public MainViewModel()
        {
            Items = reptileWorker.Controller(umr.ReadLines());
        }

        // 在新链接发生变化时触发 规范化
        //private void OnNewurlChanged( string value)
        //{
        //    bool hasWhitespace = Regex.IsMatch(value, @"\s");
        //    if (hasWhitespace)
        //    {
        //        Newurl = Regex.Replace(value, @"\s+", "");
        //    }
        //}
        [RelayCommand]
        private void AddUrl(object sender)
        {
            if (Newurl != null)
            {
                umr.AddLine(Newurl);
                Items = reptileWorker.Controller(new List<string> { Newurl });
                
            }

            Newurl = "";
        }

        [RelayCommand]
        private void RemoveUrl(object sender)
        {
            // 获取触发按钮对象
            System.Windows.Controls.Button button = (System.Windows.Controls.Button)sender;
            // 获取按钮的Tag属性
            string tag = button.Tag.ToString();
            // 删除对应的链接
            if (tag != null) { umr.RemoveLine(tag);}
            // 删除对应的数据
            Items.Remove(Items.Where(x => x.Url == tag).FirstOrDefault());
        }


    }
}
