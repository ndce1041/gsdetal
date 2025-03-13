using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Collections;

using AngleSharp;
using AngleSharp.Html.Parser;
using System.Diagnostics;
using System.Threading;
using AngleSharp.Io;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using AngleSharp.Css;
using AngleSharp.Dom;
using static System.Net.WebRequestMethods;
using System.Security.Policy;

using gsdetal.MainLogic;
using gsdetal.MainViewModel;

using gsdetal.SpiderTemplate;
using gsdetal.SpiderTemplate.Imple;
using gsdetal.Models;
using gsdetal.Services;
using gsdetal.Services.Implementations;

using gsdetal.DBViewModel;

namespace gsdetal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //public ObservableCollection<Peritem> Items { get; set; }

        //public TextFileProcessor textFileProcessor = new("./save.txt");

        //public ReptileWorker rwoker;

        public string log = "log:\n";
        public MainWindow()
        {
            //InitializeComponent();
            //MainViewModel.MainViewModel mainViewModel = new();
            //this.DataContext = mainViewModel;

            MyDBContext context = new();
            IItemdetailService itemService = new ItemdetailService(context);
            IUrlService urlService = new UrlService(context);
            IThumbnailService thumbnailService = new ThumbnailService(context);


            // 测试
            var picurlstr = "https://itemimg-rcw.runway-webstore.net/itemimg/MK010/A0MK0000BIUN/02_M09-72.jpg";


            var mainurl = "https://runway-webstore.com/ap/item/i/m/0124503006";


            IOriginTemplate template = new ThumbnailTemplate("debug");

            IOriginTemplate No1Template = new NO1Template("debug");



            //Func<Task> task = No1Template.GetTask(mainurl);

            //No1Template.Run();

            //Itemdetail itl = new Itemdetail();
            //itl.thumbnailurl = picurlstr;

            //template.GetTask(itl)();


            runTime runtime = new();

            runtime.StartThumbnailTask();


            //var umr = runtime.umr;
            ////umr.AddUrl(mainurl);
            ///

            //thumbnailService.AddUrl(picurlstr);
            //var ans = thumbnailService.GetPathByUrl(picurlstr);
            //var ans2 = thumbnailService.GetUrlThatNoFile();
            //thumbnailService.UpdateUrl(picurlstr, "");


            Console.WriteLine("end");
























            //Items = new ObservableCollection<Peritem>();

            ////List<string> urls = new List<string> { 
            ////    "https://runway-webstore.com/ap/item/i/m/0225203054", 
            ////    "https://runway-webstore.com/ap/item/i/m/0825103017",
            ////    "https://runway-webstore.com/ap/item/i/m/0825103001"
            ////      };
            //List<string> urls = textFileProcessor.ReadLines();
            ////Thread thread = null;
            //rwoker = new ReptileWorker( 10, null, null, Items);

            //try
            //{
            //    //thread = new Thread(new ThreadStart(rwoker.Controller));
            //    //thread.Start();

            //    rwoker.Controller(urls);
            //    log += "ok\n";
            //}
            //catch (Exception e)
            //{
            //    log += "er:" + e.Message;
            //}

            //测试   新建线程
            // TestClass testClass = new TestClass();
            //Thread thread = null;
            //try
            //{
            //    thread = new Thread(new ThreadStart(testClass.runtest));
            //    thread.Start();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //}

            // 等待线程结束
            //thread.Join();

            //mainTextBox.Text = testClass.ans;


            // 设置DataGrid的数据源
            //mainTextBox.Text = log + rwoker.ansJson + JsonSerializer.Serialize(rwoker.items);
            //mainDataGrid.ItemsSource = Items;
            //mainDataGrid.ItemsSource = null;

            //for (int i = 0; i < 100; i++)
            //{
            //    // 更新页面
            //    Thread.Sleep(1000);
            //    mainDataGrid.ItemsSource = rwoker.items;

            //}
        }



        //private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        //{
        //        Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
        //        e.Handled = true;
        //}

        //private void Delet_url(object sender, RoutedEventArgs e)
        //{
        //    var button = sender as Button;
        //    // 获取name属性
        //    var url = button.Tag as string;

        //    // 从文件中删除指定的字符串
        //    textFileProcessor.RemoveLine(url);

        //    // 更新DataGrid
        //    var toRemove = Items.Where(x => x.Url == url).ToList();
        //    foreach (var item in toRemove)
        //    {
        //        Items.Remove(item);
        //    }

        //}

        //private void Add_Url(object sender, RoutedEventArgs e)
        //{
        //    // 获取输入框的内容
        //    var url = NewUrl.Text;
        //    // 添加到文件中
        //    // 查重
        //    foreach (var item in Items)
        //    {
        //        if (item.Url == url)
        //        {
        //            return;
        //        }
        //    }

        //    textFileProcessor.AddLine(url);

        //    NewUrl.Text = null;

        //    // 更新DataGrid
        //    try
        //    {
        //       rwoker.Controller(new List<string> { url });
        //    }
        //    catch(Exception ex)
        //    {
        //        log += ex.Message;
        //    }
        //}
    }



    //public class TestClass
    //{
    //    public string path = "C:\\Users\\x1041\\Desktop\\0225203054.html";
    //    public Peritem item = new();
    //    string content;
    //    HttpClient client = new();
    //    string ansJson = "start";
    //    string url = "https://runway-webstore.com/ap/item/i/m/0225203054";
    //    public TestClass()
    //    {
    //        //content = File.ReadAllText(path);
    //        this.client.Timeout = TimeSpan.FromSeconds(30);
    //        this.client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/132.0.0.0 Safari/537.36");
    //        this.client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
    //        this.client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br, zstd");
    //        this.client.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8");
    //        this.client.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");

    //        this.client.DefaultRequestHeaders.Remove("Connection");
    //    }


    //    public async void runtest()
    //    {
    //        string body = "start\n";

    //        try
    //        {
    //            var response = await client.GetAsync(url);


    //            if (response.IsSuccessStatusCode)
    //            {
    //                body = await response.Content.ReadAsStringAsync();
    //                ansJson += body;
    //            }
    //        }
    //        catch (HttpRequestException e)
    //        {
    //            ansJson += "\nException Caught!" + e.Message;
            

    //        }

    //        ansJson += "\nend";
    //    }

    //    public string ans
    //    {
    //        get { return this.ansJson; }
    //    }

    //}

}