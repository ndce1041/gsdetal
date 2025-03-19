using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace gsdetal.Models.ObservableModels
{
    public partial class ObItemdetail : ObservableObject
    {
        [ObservableProperty]
        public int? id;
        [ObservableProperty]
        public string? color;
        [ObservableProperty]
        public string? size;
        [ObservableProperty]
        public string? state;
        [ObservableProperty]
        public string? thumbnailpath;
        [ObservableProperty]
        public string? tip;

        public ObItemdetail(Itemdetail Oritemdetail)
        {
            id = Oritemdetail.Id;
            color = Oritemdetail?.color;
            size = Oritemdetail?.size;
            state = Oritemdetail?.state;
            tip = Oritemdetail?.tip;
        }
    }
}
