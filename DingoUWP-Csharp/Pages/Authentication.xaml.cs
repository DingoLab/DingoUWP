using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace DingoUWP_Csharp.Pages
{
    /// <summary>
    /// 用于填写详细用户信息的页面
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Authentication : Page
    {
        //三级地址对象
        JsonArray address0;
        JsonArray address1;
        JsonArray address2;
        public Authentication()
        {
            this.InitializeComponent();
            //三级地址对象初始化
            address0 = new JsonArray();
            address1 = new JsonArray();
            address2 = new JsonArray();
            //向三级地址对象中写入数据，并加载省份列表
            ReadAddressJSON();
        }

        /// <summary>
        /// 向三级地址对象中写入数据，并加载省份列表
        /// </summary>
        async private void ReadAddressJSON()
        {
            StorageFile file;
            //初始化地址JSON文件位置
            Uri address0URI = new Uri("ms-appx:///Data/Address0.json");
            Uri address1URI = new Uri("ms-appx:///Data/Address1.json");
            Uri address2URI = new Uri("ms-appx:///Data/Address2.json");
            //获取并向一级地址对象写入数据
            file = await StorageFile.GetFileFromApplicationUriAsync(address0URI);
            string address0text = await FileIO.ReadTextAsync(file);
            address0 = JsonArray.Parse(address0text);
            //获取并向二级地址对象写入数据
            file = await StorageFile.GetFileFromApplicationUriAsync(address1URI);
            string address1text = await FileIO.ReadTextAsync(file);
            address1 = JsonArray.Parse(address1text);
            //获取并向三级地址对象写入数据
            file = await StorageFile.GetFileFromApplicationUriAsync(address2URI);
            string address2text = await FileIO.ReadTextAsync(file);
            address2 = JsonArray.Parse(address2text);

            //加载省份列表
            loadProvince("-1");
        }

        /// <summary>
        /// 当省份发生变化时，加载省份对应的城市列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void provSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadCity(Convert.ToString(((ComboBoxItem)provSelect.SelectedItem).Tag));
        }

        /// <summary>
        /// 当城市发生变化时，加载城市对应的地区列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void citySelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (citySelect.SelectedItem != null)
            {
                loadArea(Convert.ToString(((ComboBoxItem)citySelect.SelectedItem).Tag));
            }
        }

        /// <summary>
        /// 当地区发生变化时的响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void areaSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        
        /// <summary>
        /// 获取获取三级地址对应的编号和名称JSON对象
        /// </summary>
        /// <param name="regionID">三级地址编号</param>
        /// <param name="type">三级地址类型，0-省份，1-城市，2-地区</param>
        /// <returns></returns>
        private JsonObject getAddress(string regionID, int type)
        {
            JsonObject array = new JsonObject();
            if (type == 0)
            {
                int len = address0.Count;
                for (int temp = 0; temp < len; temp++)
                {
                    string key = address0[temp].GetArray()[0].GetString();
                    var value = address0[temp].GetArray()[1];
                    array.Add(key, value);
                }
            }
            else if (type == 1)
            {
                string str = regionID.Substring(0, 2);
                int len = address1.Count;
                for (int temp = 0; temp < len; temp++)
                {
                    string key = address1[temp].GetArray()[0].GetString();
                    var value = address1[temp].GetArray()[1];
                    if (key.Substring(0, 2) == str)
                    {
                        array.Add(key, value);
                    }
                }
            }
            else if (type == 2)
            {
                string str = regionID.Substring(0, 4);
                int len = address2.Count;
                for (int temp = 0; temp < len; temp++)
                {
                    string key = address2[temp].GetArray()[0].GetString();
                    var value = address2[temp].GetArray()[1];
                    if (key.Substring(0, 4) == str)
                    {
                        array.Add(key, value);
                    }
                }
            }
            return array;
        }
        
        /// <summary>
        /// 加载省份列表
        /// </summary>
        /// <param name="regionID">省份编号</param>
        private void loadProvince(string regionID)
        {
            var jsonStr = getAddress(regionID, 0);
            foreach (var temp in jsonStr)
            {
                provSelect.Items.Add(new ComboBoxItem() { Content = temp.Value.GetString(), Tag = temp.Key });
            }
        }

        /// <summary>
        /// 清理并重新加载城市列表，清理地区列表
        /// </summary>
        /// <param name="regionID">城市编号</param>
        private void loadCity(string regionID)
        {
            citySelect.Items.Clear();
            areaSelect.Items.Clear();
            JsonObject jsonStr = getAddress(regionID, 1);
            foreach (var temp in jsonStr)
            {
                citySelect.Items.Add(new ComboBoxItem() { Content = temp.Value.GetString(), Tag = temp.Key });
            }
        }

        /// <summary>
        /// 清理并重新加载地区列表
        /// </summary>
        /// <param name="regionID">地区编号</param>
        private void loadArea(string regionID)
        {
            areaSelect.Items.Clear();
            JsonObject jsonStr = getAddress(regionID, 2);
            foreach (var temp in jsonStr)
            {
                areaSelect.Items.Add(new ComboBoxItem() { Content = temp.Value.GetString(), Tag = temp.Key });
            }
        }

        /// <summary>
        /// 用户选择头像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void chooseHeadImage_Click(object sender, RoutedEventArgs e)
        {
            var mypicture = Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Pictures);
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                headImage.Source = headImageTemp.Source;
                Windows.Storage.StorageFile head = await file.CopyAsync(storageFolder, "head.jpg", Windows.Storage.NameCollisionOption.ReplaceExisting);
                headImage.Source = new BitmapImage(new Uri(head.Path, UriKind.RelativeOrAbsolute));
            }
            else
            { }
        }

        /// <summary>
        /// 用户选择自行车图片，张数不得大于4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void chooseBicycleImage_Click(object sender, RoutedEventArgs e)
        {
            var mypicture = Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Pictures);
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            IReadOnlyList<StorageFile> bikeList =await picker.PickMultipleFilesAsync();
            List<Image> bikeImageList = new List<Image>();
            bikeImageList.Add(bikeImage1);
            bikeImageList.Add(bikeImage2);
            bikeImageList.Add(bikeImage3);
            bikeImageList.Add(bikeImage4);
            for(int temp=0;temp<bikeList.Count&&temp<=3; temp++)
            {
                bikeImageList[temp].Source= headImageTemp.Source;
                Windows.Storage.StorageFile biketemp = await bikeList[temp].CopyAsync(storageFolder, "bike"+(temp+1).ToString()+".jpg", Windows.Storage.NameCollisionOption.ReplaceExisting);
                bikeImageList[temp].Source = new BitmapImage(new Uri(biketemp.Path, UriKind.RelativeOrAbsolute));
            }
        }
    }
}
