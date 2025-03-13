using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace gsdetal.Models.ObservableModels
{
    public partial class ObUrl : ObservableObject
    {
        [ObservableProperty]
        private string? url;
        [ObservableProperty]
        public string? order;  // 优先级
        [ObservableProperty]
        public string? title;
        [ObservableProperty]
        public string? type; // 商品种类
        [ObservableProperty]
        public string? price;
        [ObservableProperty]
        public string? status;
        [ObservableProperty]
        public bool isSelected = false;
        // 记录更新时间
        public DateTime? updatetime;

        public ObUrl(Url Orurl)
        {
            url = Orurl.url;
            order = Orurl.order;
            title = Orurl.title;
            type = Orurl.type;
            price = Orurl.price;
            status = Orurl.status;
            updatetime = Orurl.updatetime;

        }

    }
}
