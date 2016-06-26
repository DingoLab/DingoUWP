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

    /// <summary>
    /// 响应基础类型
    /// </summary>
    class BaseModelResponse
    {
        public string Status { get; set; }
        public string Context { get; set; }
    }
    /// <summary>
    /// 用户管理过程中的响应类型模型
    /// </summary>
    namespace UserMamagermentResponse
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        class UserSignUp:BaseModelResponse
        {
            public string UID { get; set; }
        }
        
        /// <summary>
        /// 请求用户认证
        /// </summary>
        class UserAuthentication:BaseModelResponse
        {}

        /// <summary>
        /// 查询用户身份认证状态
        /// </summary>
        class GetAuthenticationStatus:BaseModelResponse
        {
            public string AuthenticationStatus { get; set; }
        }
        
        /// <summary>
        /// 用户登录
        /// </summary>
        class UserLogIn:BaseModelResponse
        {
            public TOKEN Token { get; set; }
        }
       
        /// <summary>
        /// 用户登出
        /// </summary>
        class UserLogOut:BaseModelResponse
        {}
        
        /// <summary>
        /// 获取用户信息
        /// </summary>
        class GetUserInfo:BaseModelResponse
        {
            public string UID { get; set; }
            public string Name { get; set; }
            public string Tel { get; set; }
            public string Email { get; set; }
        }
        
        /// <summary>
        /// 获取用户头像
        /// </summary>
        class GetUserHeadImage:BaseModelResponse
        {
            public BitmapImage HeadImage{ get; set; }
        }
        
        /// <summary>
        /// 修改用户信息
        /// </summary>
        class ChangeUserInfo:BaseModelResponse
        {}
        
        /// <summary>
        /// 修改用户密码
        /// </summary>
        class ChangeUserPassword:BaseModelResponse
        { }
       
        /// <summary>
        /// 收货地址的增删改
        /// </summary>
        class ChangeShippingAddress:BaseModelResponse
        {
            public string AddressNumber { get; set; }
        }
        
        /// <summary>
        /// 查询收货地址
        /// </summary>
        class GetShippingAddress :BaseModelResponse
        {
            public string AddressInfo { get; set; }
        }
    }
}
