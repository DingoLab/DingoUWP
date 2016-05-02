using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingoUWP_Csharp.Models
{
    public class UserInfo:UserBase
    {
        private string _tel;
        public string Tel
        {
            get
            {
                return _tel;
            }
            set
            {
                _tel = value;
            }
        }
        public string _email;
        private string Email
        {
            get
            {
                return _email;
            }
            set
            {

            }
        }
    }
}
