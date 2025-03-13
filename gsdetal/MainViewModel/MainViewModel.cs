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
using gsdetal.DBViewModel;
using gsdetal.Services.Implementations;
using gsdetal.Services;

using gsdetal.Models;
using gsdetal.Models.ObservableModels;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;

namespace gsdetal.MainViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public ObservableCollection<ObUrl> Items { get; set; }   // 左侧列表数据

        [ObservableProperty]
        private string? newurl;  // 新链接输入框绑定

        [ObservableProperty]
        public ObUrl? selectedItem;  // 选中的行

        MyDBContext context = new();
        IItemdetailService itemService;
        IUrlService urlService;

        runTime runtime;
        urlManage umr;

        public MainViewModel()
        {
            
            itemService = new ItemdetailService(context);
            urlService = new UrlService(context);

            Items = new ObservableCollection<ObUrl>();
            

            runtime = new();
            umr = runtime.umr;


            // 

            //TODO 更新数据  runtime
            runtime.run();


            // 从数据库中读取数据

            List<Url> urls = urlService.GetAllUrlOrdered();


            ObUrl temp;
            foreach (var url in urls)
            {
                {
                    temp = new ObUrl(url);

                    temp.PropertyChanged += Item_PropertyChanged;

                    Items.Add(temp);
                }
            }
        }

        [RelayCommand]
        private void AddUrl(object sender)  // 添加新链接按钮回调
        {
            if (Newurl != null)
            {
                umr.AddUrl(Newurl);  // 添加新链接
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
            if (tag != null) { umr.RemoveUrl(tag);}
            // 删除对应的数据
            Items.Remove(Items.Where(x => x.Url == tag).FirstOrDefault());
        }


        [RelayCommand]
        private void GetSelectedItems_Click(object sender)   // 获取所有选中并删除的按钮回调
        {
            // 获取所有选中项的url
            var selectedIds = Items.Where(item => item.IsSelected).Select(item => item.Url).ToList();

            // TODO 删除选中
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)     // Order值变更回调
        {
            if (e.PropertyName == "Order" && sender is ObUrl item)
            {
                //TODO 在数据库中更新 Order值变更回调
                
            }
            else if (e.PropertyName == "IsSelected" && sender is ObUrl selectedItem)
            {
                // IsSelected变更回调（可选）
            }
        }






    }
}
