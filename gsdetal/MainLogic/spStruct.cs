using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace gsdetal.MainLogic
{
    internal class spStruct
    {
    }


    //public class User
    //{
    //    public string? Name { get; set; }
    //    public int? Age { get; set; }
    //    public string? City { get; set; }
    //}

    public partial class Peritem : ObservableObject
    {
        [ObservableProperty]
        private string? url;
        [ObservableProperty]
        private string? name;
        [ObservableProperty]
        private string? price;
        [ObservableProperty]
        private string? state;
        [ObservableProperty]
        private List<Pertype> pt;

        public Peritem()
        {
            pt = new();
            url = "";
        }


    }

    public struct Pertype
    {
        public string? Style { get; set; }
        public List<Persize> ps { get; set; }

        public Pertype()
        {
            ps = new();
        }
    }

    public struct Persize
    {
        public string? Size { get; set; }
        public string? State { get; set; }
        public string? Soldtime { get; set; }
    }


}
