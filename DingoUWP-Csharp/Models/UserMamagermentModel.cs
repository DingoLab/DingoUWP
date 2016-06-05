using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace DingoUWP_Csharp.Models
{
    /// <summary>
    /// 用户登录成功时后端返回的临时Token，包含一个字符序列和到期时间
    /// </summary>
    public struct TOKEN
    {
        /// <summary>
        /// 后端生成的token值，用于检测用户登录
        /// </summary>
        public string Token;
        ///// <summary>
        ///// token过期时间
        ///// </summary>
        //public DateTime Maturity;
    }

    class BaseModel
    {
        public string Status { get; set; }
        public string Context { get; set; }
    }
    namespace UserMamagermentModel
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        class UserSignUp:BaseModel
        {
            public string UID { get; set; }
        }
        
        /// <summary>
        /// 请求用户认证
        /// </summary>
        class UserAuthentication:BaseModel
        {}

        /// <summary>
        /// 查询用户身份认证状态
        /// </summary>
        class GetAuthenticationStatus:BaseModel
        {
            public string AuthenticationStatus { get; set; }
        }
        
        /// <summary>
        /// 用户登录
        /// </summary>
        class UserLogIn:BaseModel
        {
            public TOKEN Token { get; set; }
        }
       
        /// <summary>
        /// 用户登出
        /// </summary>
        class UserLogOut:BaseModel
        {}
        
        /// <summary>
        /// 获取用户信息
        /// </summary>
        class GetUserInfo:BaseModel
        {
            public string UID { get; set; }
            public string Name { get; set; }
            public string Tel { get; set; }
            public string Email { get; set; }
        }
        
        /// <summary>
        /// 获取用户头像
        /// </summary>
        class GetUserHeadImage:BaseModel
        {
            public BitmapImage HeadImage{ get; set; }
        }
        
        /// <summary>
        /// 修改用户信息
        /// </summary>
        class ChangeUserInfo:BaseModel
        {}
        
        /// <summary>
        /// 修改用户密码
        /// </summary>
        class ChangeUserPassword:BaseModel
        { }
       
        /// <summary>
        /// 收货地址的增删改
        /// </summary>
        class ChangeShippingAddress:BaseModel
        {
            public string AddressNumber { get; set; }
        }
        
        /// <summary>
        /// 查询收货地址
        /// </summary>
        class GetShippingAddress :BaseModel
        {
            public string AddressInfo { get; set; }
        }
    }
}
