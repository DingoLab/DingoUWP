using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace DingoUWP_Csharp.HTTP
{
    /// <summary>
    /// 数据转换方法
    /// </summary>
    class DataConvert
    {
        /// <summary>
        /// Stream转String
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string StreamToString(Stream stream)
        {
            stream.Position = 0;
            using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
            {
                return streamReader.ReadToEnd();
            }
        }
        
        /// <summary>
        /// String 转 Stream
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Stream StringToStream(string str)
        {
            byte[] strBytes = Encoding.UTF32.GetBytes(str);
            return new MemoryStream(strBytes);
        }
        
        /// <summary>
        /// Bytes转BitmapImage
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public async static Task<BitmapImage> BytesToImage(byte[] bytes)
        {
            try
            {
                BitmapImage bitmapimage = new BitmapImage();
                await bitmapimage.SetSourceAsync(BytesToStream(bytes).AsRandomAccessStream());
                return bitmapimage;
            }
            catch
            {
                return null;
            }
        }
        
        /// <summary>
        /// 图片转bytes
        /// </summary>
        /// <param name="imagesource"></param>
        /// <returns></returns>
        public async static Task<byte[]> ImageToBytes(ImageSource imagesource)
        {
            if(imagesource==null)
            {
                return null;
            }
            BitmapImage tempimg = (BitmapImage)imagesource;
            try
            {
                byte[] bytes;
                using (FileStream fsRead = File.Open(tempimg.UriSource.LocalPath, FileMode.Open))
                {
                    bytes = new byte[fsRead.Length];
                    await fsRead.ReadAsync(bytes, 0, Convert.ToInt32(fsRead.Length));
                }
                return bytes;
            }
            catch
            {
                return null;
            }
            
        }
        
        /// <summary>
        /// bytes转Stream
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Stream BytesToStream(byte[] bytes)
        {
            try
            {
                Stream stream = new MemoryStream(bytes);
                return stream;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Stream转Bytes
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async Task<byte[]> StreamToBytes(Stream stream)
        {
            try
            {
                byte[] bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, bytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                return bytes;
            }
            catch
            {
                return null;
            }
        }
    }
}
