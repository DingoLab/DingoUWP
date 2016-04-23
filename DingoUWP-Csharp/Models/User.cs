using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.DataAnnotations;

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
    /// 用户基础类型
    /// </summary>
    public class User:INotifyPropertyChanged,INotifyDataErrorInfo
    {
        //验证错误时的提示信息
        private const string NAME_ERROE1 = "用户名太短";
        private const string NAME_ERROE2 = "用户名太长";
        private const string NAME_ERROE3 = "用户名不得含有表情";

        //用于保存验证错误信息。key 保存所验证的字段名称；value 保存对应的字段的验证错误信息列表
        private Dictionary<String, List<String>> errors = new Dictionary<string, List<string>>();
        //用户ID,唯一（数字）
        private string _id;
        public string ID
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        //用户名，不能含有表情
        private string _name;
        [Display(Name="用户名")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                
                _name = value;
            }
        }

        //临时Token
        public TOKEN Token;

        public bool IsNameValid(string value)
        {
            bool isValid = true;
            if (value.Contains(" "))
            {
                AddError("Name", NAME_ERROE3);
                isValid = false;
            }
            else
            {
                RemoveError("Name", NAME_ERROE3);
            }
            if (value.Length < 4)
            {
                AddError("Name", NAME_ERROE1);
                isValid = false;
            }
            else
            {
                RemoveError("Name", NAME_ERROE1);
            }
            if (value.Length > 25)
            {
                AddError("Name", NAME_ERROE2);
                isValid = false;
            }
            else
            {
                RemoveError("Name", NAME_ERROE2);
            }
            return isValid;
        }

        public void AddError(string propertyName,string error)
        {
            if (!errors.ContainsKey(propertyName))
            {
                errors[propertyName] = new List<string>();
            }
            if (!errors[propertyName].Contains(error))
            {
                errors[propertyName].Add(error);
                RaiseErrorsChanged(propertyName);
            }
        }

        public void RemoveError(string propertyName, string error)
        {
            if (errors.ContainsKey(propertyName) && errors[propertyName].Contains(error)) 
            {
                errors[propertyName].Remove(error);
                if (errors[propertyName].Count == 0)
                {
                    errors.Remove(propertyName);
                }
                RaiseErrorsChanged(propertyName);
            }
        }
        public bool HasErrors
        {
            get
            {
                return errors.Count > 0;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }
        public IEnumerable GetErrors(string propertyName)
        {
            if (errors.ContainsKey(propertyName))
            {
                return errors[propertyName];
            }
            else
            {
                return null;
            }
        }

    }
}
