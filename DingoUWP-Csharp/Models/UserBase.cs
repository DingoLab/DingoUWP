using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DingoUWP_Csharp.Models
{
    /// <summary>
    /// 用户登录成功时后端返回的临时Token，包含一个字符序列和到期时间
    /// </summary>
    public struct TOKEN
    {
        string token;
        DateTime maturity;
    }
    /// <summary>
    /// 用户登陆基础类型
    /// </summary>
    public class UserBase:INotifyPropertyChanged
    {
        //验证错误时的提示信息
        private const string PhoneNumber_ERROE = "手机号码格式错误";
        private const string Password_ERROE1 = "密码太长";
        private const string Password_ERROE2 = "密码太短";
        private const string Password_ERROE3 = "密码不得为空";
        private const string Password_ERROE4 = "密码不得含有空格";

        //数据合法性校验使用的正则表达式
        private const string PhoneNumber_REGEX = @"^((\+86)|(\(\+86\)))?(1\d{2}\d{8})?$";

        //用户ID,唯一（由后端生成）
        public string ID;

        //用户电话号码，仅支持中国手机电话号码
        private string _phonename;
        public string PhoneNumber
        {
            get
            {
                return _phonename;
            }
            set
            {
                if (Regex.IsMatch(value, PhoneNumber_REGEX))
                {
                    PhoneNumberWarnning = String.Empty;
                    _phonename = value;
                    NotifyPropertyChanged("PhoneNumber");
                }
                else
                {
                    PhoneNumberWarnning = PhoneNumber_ERROE;
                }
            }
        }
        //电话号码错误提示
        private string _PhoneNumberWarring;
        public string PhoneNumberWarnning
        {
            get
            {
                return _PhoneNumberWarring;
            }
            set
            {
                _PhoneNumberWarring = value;
                NotifyPropertyChanged("PhoneNumberWarnning");
            }
        }
        //用户密码，长度小于等于24，大于等于6，不得为空，不得含有空格
        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if(value.Length>24)
                {
                    PasswordWarnning = Password_ERROE1;
                }
                else if(value.Length<6)
                {
                    PasswordWarnning = Password_ERROE2;
                }
                else if(value.Length==0)
                {
                    PasswordWarnning = Password_ERROE3;
                }
                else if(value.IndexOf(' ')!=-1)
                {
                    PasswordWarnning = Password_ERROE4;
                }
                else
                {
                    PasswordWarnning = String.Empty;
                    _password = value;
                    NotifyPropertyChanged("Password");
                }
            }
        }

        //密码错误提示
        private string _PasswordWarnning;
        public string PasswordWarnning
        {
            get
            {
                return _PasswordWarnning;
            }
            set
            {
                _PasswordWarnning = value;
                NotifyPropertyChanged("PasswordWarnning");
            }
        }

        //临时Token
        public TOKEN Token;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public static class UserBaseViewModel
    {
        private static UserBase _UserBaseViewModel = new UserBase();
        public static UserBase GetUserBase()
        {
            return _UserBaseViewModel;
        }
    }
}
