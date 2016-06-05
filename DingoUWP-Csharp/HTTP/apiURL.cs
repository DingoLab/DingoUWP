using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingoUWP_Csharp.HTTP
{
    /// <summary>
    /// Dingo后端提供的API
    /// </summary>
    static class apiURL
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        public static string API_UserSignUp = "";
        
        /// <summary>
        /// 用户身份认证
        /// </summary>
        public static string API_Authentication = "";
        
        /// <summary>
        /// 查询身份认证状态
        /// </summary>
        public static string API_GetAuthenticationStatus = "";
        
        /// <summary>
        /// 用户登录
        /// </summary>
        public static string API_UserLogIn = "";
        
        /// <summary>
        /// 用户登出
        /// </summary>
        public static string API_UserLogOut = "";
        
        /// <summary>
        /// 获取用户信息
        /// </summary>
        public static string API_GetUserInfo = "";
        
        /// <summary>
        /// 获取用户头像
        /// </summary>
        public static string API_GetUserHeadImage = "";
        
        /// <summary>
        /// 修改用户信息
        /// </summary>
        public static string API_ChangeUserInfo = "";
        
        /// <summary>
        /// 修改用户密码
        /// </summary>
        public static string API_ChangeUserPassword = "";
        
        /// <summary>
        /// 修改收货地址
        /// </summary>
        public static string API_ChangeShippingAddress = "";
        
        /// <summary>
        /// 查询收货地址
        /// </summary>
        public static string API_GetShippingAddress = "";

        //以下为任务管理部分API

        /// <summary>
        /// 任务发布（代收快递）
        /// </summary>
        public static string API_PublishMission = "";
        
        /// <summary>
        /// 任务的修改或删除
        /// </summary>
        public static string API_MissionChangeOrDelete = "";
        
        /// <summary>
        /// 获取任务（代收快递）
        /// </summary>
        public static string API_GetMission = "";
        
        /// <summary>
        /// 协商价格
        /// </summary>
        public static string API_NegotiatePrice = "";
        
        /// <summary>
        /// 获取任务信息
        /// </summary>
        public static string API_GetMissionInfo = "";
        
        /// <summary>
        /// 支付和确认
        /// </summary>
        public static string API_PaymentAndConfirm = "";
        
        /// <summary>
        /// 可代收状态的修改或添加
        /// </summary>
        public static string API_ChangeOrAddCanInwardCollectionStatus = "";
        
        /// <summary>
        /// 可代收状态的删除或完成
        /// </summary>
        public static string API_DeleteOrCompleteCanInwardCollectionStatus = "";
        
        /// <summary>
        /// 可代收状态查询
        /// </summary>
        public static string API_GetCanInwardCollectionStatus = "";
        
        /// <summary>
        /// 消息通告
        /// </summary>
        public static string API_NotifyMessages = "";
    }
}
