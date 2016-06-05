using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Net;
using Windows.Web.Http;
using Windows.Storage.Streams;
using Windows.Data.Json;

namespace DingoUWP_Csharp.HTTP
{
    /// <summary>
    /// 基础HTTP访问服务
    /// </summary>
    class BaseService
    {
        /// <summary>
        /// 向服务器发送get请求，返回服务器回复字符串数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async static Task<string> HTTP_SendGetAsString(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                Uri uri = new Uri(url);
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 向服务器发送post请求,无附加的头信息，返回包含状态指示和服务器回复字符串数据的字典
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async static Task<Dictionary<string,string>> HTTP_SendPostAsDictionary(string url,string body)
        {
            Dictionary<string, string> responseDictionary = new Dictionary<string, string>();
            try
            {
                string Status, Context_Where;
                HttpRequestMessage mSent = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                mSent.Content = new HttpStringContent(body, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json;charset=utf-8");
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.SendRequestAsync(mSent);
                response.EnsureSuccessStatusCode();
                response.Headers.TryGetValue("Status", out Status);
                response.Headers.TryGetValue("Context-Where", out Context_Where);
                if(Status=="Success")
                {
                    if(Context_Where=="Body")
                    {
                        responseDictionary.Add("Context", await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        string Context;
                        response.Headers.TryGetValue(Context_Where, out Context);
                        responseDictionary.Add("Context",Context);
                    }
                    responseDictionary.Add("Status", "Success");
                    return responseDictionary;
                }
                else if(Status=="Failed")
                {
                    if (Context_Where == "Body")
                    {
                        responseDictionary.Add("Context", await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        string Context;
                        response.Headers.TryGetValue(Context_Where, out Context);
                        responseDictionary.Add("Context", Context);
                    }
                    responseDictionary.Add("Status", "Failed");
                    return responseDictionary;
                }
                else
                {
                    throw new DingoDataException();
                }
            }
            catch
            {
                responseDictionary.Add("Status","Failed");
                responseDictionary.Add("Error","Unknow");
                return responseDictionary;
            }
        }

        /// <summary>
        /// 向服务器发送post请求,存在附加的头信息，返回包含状态指示和服务器回复字符串数据的字典
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <param name="headerkey"></param>
        /// <param name="headvalue"></param>
        /// <returns></returns>
        public async static Task<Dictionary<string, string>> HTTP_SendPostAsDictionary(string url, string body,string[] headerkey,string[] headvalue)
        {
            Dictionary<string, string> responseDictionary = new Dictionary<string, string>();
            try
            {
                string Status, Context_Where;
                HttpRequestMessage mSent = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                for(int temp = 0; temp < headerkey.Length ; temp++ )
                {
                    mSent.Headers.Add(headerkey[temp], headvalue[temp]);
                }
                mSent.Content = new HttpStringContent(body, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json;charset=utf-8");
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.SendRequestAsync(mSent);
                response.EnsureSuccessStatusCode();
                response.Headers.TryGetValue("Status", out Status);
                response.Headers.TryGetValue("Context-Where", out Context_Where);
                if (Status == "Success")
                {
                    if (Context_Where == "Body")
                    {
                        responseDictionary.Add("Context", await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        string Context;
                        response.Headers.TryGetValue(Context_Where, out Context);
                        responseDictionary.Add("Context", Context);
                    }
                    responseDictionary.Add("Status", "Success");
                    return responseDictionary;
                }
                else if (Status == "Failed")
                {
                    if (Context_Where == "Body")
                    {
                        responseDictionary.Add("Context", await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        string Context;
                        response.Headers.TryGetValue(Context_Where, out Context);
                        responseDictionary.Add("Context", Context);
                    }
                    responseDictionary.Add("Status", "Failed");
                    return responseDictionary;
                }
                else
                {
                    throw new DingoDataException();
                }
            }
            catch
            {
                responseDictionary.Add("Status", "Failed");
                responseDictionary.Add("Error", "Unknow");
                return responseDictionary;
            }
        }
        
        /// <summary>
        /// 向服务器发送post请求，返回服务器回复Bytes数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async static Task<IBuffer> SendPostAsBytes(string url,string body)
        {
            try
            {
                HttpRequestMessage mSent = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                mSent.Content = new HttpStringContent(body, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json;charset=utf-8");
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.SendRequestAsync(mSent);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsBufferAsync();
            }
            catch
            {
                return null;
            }
        }
        
        /// <summary>
        /// 向服务器发送post请求，返回服务器回复InputStream数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async static Task<IInputStream> SendPostAsInputStream(string url, string body)
        {
            try
            {
                HttpRequestMessage mSent = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                mSent.Content = new HttpStringContent(body, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json;charset=utf-8");
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.SendRequestAsync(mSent);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsInputStreamAsync();
            }
            catch
            {
                return null;
            }
        }
    }
    
    /// <summary>
    /// 当Dingo后端返回了无法使用的信息时抛出的异常
    /// </summary>
    class DingoDataException:Exception
    {
        public DingoDataException() { }
    }
}
