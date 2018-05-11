using System;
using Realms;

namespace Mowards.Models
{
    public class LoginInfo : RealmObject
    {
        public string Username
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string Token
        {
            get;
            set;
        }
    }
}
