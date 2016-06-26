using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.Security.Cryptography;
using System.Runtime.InteropServices.WindowsRuntime;

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
                /*
                BitmapImage bitmapimage = new BitmapImage();
                await bitmapimage.SetSourceAsync(BytesToStream(bytes).AsRandomAccessStream());
                return bitmapimage;
                */
                string filename = Md5Encrypt(StreamToString(BytesToStream(bytes)))+GetTimeStamp();
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalCacheFolder;
                Windows.Storage.StorageFile file = await storageFolder.CreateFileAsync(filename);
                await Windows.Storage.FileIO.WriteBytesAsync(file, bytes);
                BitmapImage bitmapimage = new BitmapImage(new Uri(file.Path));
                //await bitmapimage.SetSourceAsync(BytesToStream(bytes).AsRandomAccessStream());
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

        /// <summary>
        /// 获取密码散列值
        /// </summary>
        /// <param name="Options"></param>
        /// <param name="plaintext"></param>
        /// <returns></returns>
        public static string Encryption(string Options,string plaintext)
        {
            string temp = SHA256Encrypt(plaintext);
            return SHA512Encrypt(Options + temp + GetTimeStamp());
        }
        /// <summary>
        /// 获取明文的SHA256散列值
        /// </summary>
        /// <param name="plaintext"></param>
        /// <returns></returns>
        public static string SHA256Encrypt(string plaintext)
        {
            HashAlgorithmProvider hashalgorithmprovider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);
            CryptographicHash cryptographichash = hashalgorithmprovider.CreateHash();
            cryptographichash.Append(CryptographicBuffer.ConvertStringToBinary(plaintext, BinaryStringEncoding.Utf8));
            return CryptographicBuffer.EncodeToHexString(cryptographichash.GetValueAndReset());
        }
        
        /// <summary>
        /// 获取明文的SHA512散列值
        /// </summary>
        /// <param name="plaintext"></param>
        /// <returns></returns>
        public static string SHA512Encrypt(string plaintext)
        {
            HashAlgorithmProvider hashalgorithmprovider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha512);
            CryptographicHash cryptographichash = hashalgorithmprovider.CreateHash();
            cryptographichash.Append(CryptographicBuffer.ConvertStringToBinary(plaintext, BinaryStringEncoding.Utf8));
            return CryptographicBuffer.EncodeToHexString(cryptographichash.GetValueAndReset());
        }
        /// <summary>
        /// 获取明文的Md5散列值
        /// </summary>
        /// <param name="plaintext"></param>
        /// <returns></returns>
        public static string Md5Encrypt(string plaintext)
        {
            HashAlgorithmProvider hashalgorithmprovider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            CryptographicHash cryptographichash = hashalgorithmprovider.CreateHash();
            cryptographichash.Append(CryptographicBuffer.ConvertStringToBinary(plaintext, BinaryStringEncoding.Utf8));
            return CryptographicBuffer.EncodeToHexString(cryptographichash.GetValueAndReset());
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}
