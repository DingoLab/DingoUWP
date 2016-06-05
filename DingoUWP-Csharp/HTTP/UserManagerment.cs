using DingoUWP_Csharp.Models;
using DingoUWP_Csharp.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using System.Runtime.Serialization.Json;
using Windows.Storage.Streams;
using System.IO;
using DingoUWP_Csharp.Models.UserMamagermentModel;
using System.Text.RegularExpressions;

namespace DingoUWP_Csharp.HTTP
{
    class UserManagerment
    {
        /// <summary>
        /// 获取用户注册结果
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pash"></param>
        /// <returns></returns>
        public async static Task<UserSignUp> Get_UserSignUpModel(string name,string pash)
        {
            try
            {
                UserSignUp model;
                string body = "name=" + name + "&pash=" + pash;
                Dictionary<string, string> dict = await BaseService.HTTP_SendPostAsDictionary(apiURL.API_UserSignUp, body);
                if(dict["Status"]=="Success")
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new UserSignUp()
                    {
                        Status = "Success",
                        Context = null,
                        UID = tempjson["uid"].GetString()
                    };
                    return model;
                }
                else
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new UserSignUp()
                    {
                        Status = "Failed",
                        Context = tempjson["error"].GetString(),
                        UID = null
                    };
                    return model;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取请求用户认证结果
        /// </summary>
        /// <returns></returns>
        public async static Task<UserAuthentication> Get_UserAuthenticationModel(TOKEN token,int tel,string email,string rname,string prcid, byte[] pic,string addr)
        {
            try
            {
                UserAuthentication model;
                string[] headkey = new string[1];
                string[] headvalue = new string[1];
                headkey[0] = "token";headvalue[0] = token.Token;
                string body = "tel=" + tel.ToString() + "&emial" + email + "&rname=" + rname + "&prcid=" + prcid + "&pic=" + pic.ToString() + "&addr" + addr;
                Dictionary<string, string> dict = await BaseService.HTTP_SendPostAsDictionary(apiURL.API_Authentication, body,headkey,headvalue);
                if (dict["Status"] == "Success")
                {
                    model = new UserAuthentication()
                    {
                        Status = "Success",
                        Context = null
                    };
                    return model;
                }
                else
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new UserAuthentication()
                    {
                        Status = "Failed",
                        Context = tempjson["error"].GetString()
                    };
                    return model;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 查询身份认证状态
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async static Task<GetAuthenticationStatus> Get_AuthenticationStatusModel(TOKEN token)
        {
            try
            {
                GetAuthenticationStatus model;
                string[] headkey = new string[1];
                string[] headvalue = new string[1];
                headkey[0] = "token"; headvalue[0] = token.Token;
                string body = "";
                Dictionary<string, string> dict = await BaseService.HTTP_SendPostAsDictionary(apiURL.API_GetAuthenticationStatus, body,headkey,headvalue);
                if (dict["Status"] == "Success")
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new GetAuthenticationStatus()
                    {
                        Status = "Success",
                        Context = null,
                        AuthenticationStatus= tempjson["status"].GetString()
                    };
                    return model;
                }
                else
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new GetAuthenticationStatus()
                    {
                        Status = "Failed",
                        Context = tempjson["error"].GetString(),
                        AuthenticationStatus=null
                    };
                    return model;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="nameortel"></param>
        /// <param name="pash"></param>
        /// <returns></returns>
        public async static Task<UserLogIn> Get_UserLogInModel(string nameortel,string pash)
        {
            string body;
            //string url;
            if(Regex.IsMatch(nameortel,@"^1\d{2}\d{8}"))
            {
                body = "tel+" + nameortel;
            }
            else
            {
                body = "name+" + nameortel;
            }
            try
            {
                UserLogIn model;
                body += "&pash=" + pash;
                Dictionary<string, string> dict = await BaseService.HTTP_SendPostAsDictionary(apiURL.API_UserLogIn, body);
                if (dict["Status"] == "Success")
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new UserLogIn()
                    {
                        Status = "Success",
                        Context = null,
                        Token =new TOKEN
                        {
                            Token= tempjson["token"].GetString()
                            //Maturity=8
                        }
                    };
                    return model;
                }
                else
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new UserLogIn()
                    {
                        Status = "Failed",
                        Context = tempjson["error"].GetString(),
                        Token = new TOKEN
                        {
                            Token = null
                        }
                    };
                    return model;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async static Task<UserLogOut> Get_UserLogOutModel(TOKEN token)
        {
            try
            {
                UserLogOut model;
                string[] headkey = new string[1];
                string[] headvalue = new string[1];
                headkey[0] = "token"; headvalue[0] = token.Token;
                string body = "";
                Dictionary<string, string> dict = await BaseService.HTTP_SendPostAsDictionary(apiURL.API_UserLogOut, body,headkey,headvalue);
                if (dict["Status"] == "Success")
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new UserLogOut()
                    {
                        Status = "Success",
                        Context = tempjson["status"].GetString()
                    };
                    return model;
                }
                else
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new UserLogOut()
                    {
                        Status = "Failed",
                        Context = tempjson["error"].GetString()
                    };
                    return model;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async static Task<GetUserInfo> Get_GetUserInfo(TOKEN token,string uid)
        {
            try
            {
                GetUserInfo model;
                string[] headkey = new string[2];
                string[] headvalue = new string[2];
                headkey[0] = "token"; headvalue[0] = token.Token;
                headkey[1] = "uid"; headvalue[1] = uid;
                string body = "";
                Dictionary<string, string> dict = await BaseService.HTTP_SendPostAsDictionary(apiURL.API_GetUserInfo, body,headkey,headvalue);
                if (dict["Status"] == "Success")
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new GetUserInfo()
                    {
                        Status = "Success",
                        Context = null,
                        UID = tempjson["uid"].GetString(),
                        Name= tempjson["name"].GetString(),
                        Tel= tempjson["tel"].GetString(),
                        Email= tempjson["email"].GetString()
                    };
                    return model;
                }
                else
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new GetUserInfo()
                    {
                        Status = "Failed",
                        Context = tempjson["error"].GetString(),
                        UID = null,
                        Name = null,
                        Tel = null,
                        Email = null
                    };
                    return model;
                }
            }
            catch
            {
                return null;
            }
        }
        
        /// <summary>
        /// 获取用户头像
        /// </summary>
        /// <param name="token"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async static Task<GetUserHeadImage> Get_GetUserHeadImage(TOKEN token,string uid)
        {
            try
            {
                GetUserHeadImage model;
                string[] headkey = new string[2];
                string[] headvalue = new string[2];
                headkey[0] = "token"; headvalue[0] = token.Token;
                headkey[1] = "uid";headvalue[1] = uid;
                string body = "";
                Dictionary<string, string> dict =await BaseService.HTTP_SendPostAsDictionary(apiURL.API_GetUserHeadImage,body,headkey,headvalue);
                if(dict["Status"]=="Success")
                {
                    model = new GetUserHeadImage()
                    {
                        Status = "Success",
                        Context = null,
                        HeadImage = await DataConvert.BytesToImage(await DataConvert.StreamToBytes(DataConvert.StringToStream(dict["Context"])))
                    };
                    return model;
                }
                else
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new GetUserHeadImage()
                    {
                        Status = "Failed",
                        Context = tempjson["error"].GetString(),
                        HeadImage = null
                    };
                    return model;
                }
            }
            catch
            {
                return null;
            }
        }
        
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="name"></param>
        /// <param name="tel"></param>
        /// <param name="email"></param>
        /// <param name="rname"></param>
        /// <param name="prcid"></param>
        /// <param name="pic"></param>
        /// <param name="addr"></param>
        /// <returns></returns>
        public async static Task<ChangeUserInfo> Get_ChangeUserInfo(TOKEN token,string name, int tel, string email, string rname, string prcid, byte[] pic, string addr)
        {
            try
            {
                ChangeUserInfo model;
                string[] headkey = new string[1];
                string[] headvalue = new string[1];
                headkey[0] = "token"; headvalue[0] = token.Token;
                string body = "name=" + name + "&tel=" + tel.ToString() + "&emial=" + email + "&rname=" + rname + "&prcid=" + prcid + "&pic=" + pic.ToString() + "&addr=" + addr;
                Dictionary<string, string> dict = await BaseService.HTTP_SendPostAsDictionary(apiURL.API_ChangeUserInfo, body,headkey,headvalue);
                if(dict["Status"]=="Success")
                {
                    model = new ChangeUserInfo()
                    {
                        Status = "Success",
                        Context = null
                    };
                    return model;
                }
                else
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new ChangeUserInfo()
                    {
                        Status = "Failed",
                        Context = tempjson["error"].GetString()
                    };
                    return model;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="token"></param>
        /// <param name="pash"></param>
        /// <returns></returns>
        public async static Task<ChangeUserPassword> Get_ChangeUserPassword(TOKEN token,string pash)
        {
            try
            {
                ChangeUserPassword model;
                string[] headkey = new string[1];
                string[] headvalue = new string[1];
                headkey[0] = "token"; headvalue[0] = token.Token;
                string body="pash="+pash;
                Dictionary<string, string> dict = await BaseService.HTTP_SendPostAsDictionary(apiURL.API_ChangeUserPassword, body,headkey,headvalue);
                if(dict["Status"]=="Success")
                {
                    model = new ChangeUserPassword()
                    {
                        Status = "Success",
                        Context = null
                    };
                    return model;
                }
                else
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new ChangeUserPassword()
                    {
                        Status = "Failed",
                        Context = tempjson["error"].GetString()
                    };
                    return model;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 增改收货地址
        /// </summary>
        /// <param name="token"></param>
        /// <param name="addr"></param>
        /// <param name="zip"></param>
        /// <param name="aid"></param>
        /// <returns></returns>
        public async static Task<ChangeShippingAddress> Get_ChangeShippingAddress(TOKEN token,string addr,string zip,string aid)
        {
            try
            {
                ChangeShippingAddress model;
                string[] headkey = new string[1];
                string[] headvalue = new string[1];
                headkey[0] = "token"; headvalue[0] = token.Token;
                string body = "addr=" + addr + "&zip" + zip + "&aid" + aid + "&opt=AF";
                Dictionary<string, string> dict = await BaseService.HTTP_SendPostAsDictionary(apiURL.API_ChangeUserInfo, body, headkey, headvalue);
                if(dict["Status"]=="Success")
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new ChangeShippingAddress()
                    {
                        Status = "Success",
                        Context = "null",
                        AddressNumber = tempjson["aid"].GetString()
                    };
                    return model;
                }
                else
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new ChangeShippingAddress()
                    {
                        Status = "Failed",
                        Context = tempjson["error"].GetString(),
                        AddressNumber = null
                    };
                    return model;
                }
            }
            catch
            {
                return null;
            }
        }
        
        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="token"></param>
        /// <param name="aid"></param>
        /// <returns></returns>
        public async static Task<ChangeShippingAddress> Get_ChangeShippingAddress(TOKEN token, string aid)
        {
            try
            {
                ChangeShippingAddress model;
                string[] headkey = new string[1];
                string[] headvalue = new string[1];
                headkey[0] = "token"; headvalue[0] = token.Token;
                string body = "aid" + aid + "&opt=DEL";
                Dictionary<string, string> dict = await BaseService.HTTP_SendPostAsDictionary(apiURL.API_ChangeUserInfo, body, headkey, headvalue);
                if (dict["Status"] == "Success")
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new ChangeShippingAddress()
                    {
                        Status = "Success",
                        Context = "null",
                        AddressNumber = tempjson["aid"].GetString()
                    };
                    return model;
                }
                else
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new ChangeShippingAddress()
                    {
                        Status = "Failed",
                        Context = tempjson["error"].GetString(),
                        AddressNumber = null
                    };
                    return model;
                }
            }
            catch
            {
                return null;
            }
        }
        
        /// <summary>
        /// 获取收货地址
        /// </summary>
        /// <param name="token"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async static Task<GetShippingAddress> Get_GetShippingAddress(TOKEN token,string uid)
        {
            try
            {
                GetShippingAddress model;
                string[] headkey = new string[1];
                string[] headvalue = new string[1];
                headkey[0] = "token"; headvalue[0] = token.Token;
                string body = "uid" + uid;
                Dictionary<string, string> dict = await BaseService.HTTP_SendPostAsDictionary(apiURL.API_GetShippingAddress, body, headkey, headvalue);
                if(dict["Status"]=="Success")
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new GetShippingAddress()
                    {
                        Status = "Success",
                        Context = null,
                        AddressInfo = tempjson["addr"].GetString()
                    };
                    return model;
                }
                else
                {
                    JsonObject tempjson = JsonObject.Parse(dict["Context"]);
                    model = new GetShippingAddress()
                    {
                        Status = "Failed",
                        Context = tempjson["error"].GetString(),
                        AddressInfo = null
                    };
                    return model;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
