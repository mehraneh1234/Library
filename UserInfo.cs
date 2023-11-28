using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace library
{
    [Serializable]
    public class UserInfo : IComparable<UserInfo>
    {
        public UserInfo()
        {
        }
        private string _id;
        private string _name;
        private string _email;
        private string _type;
        private string _username;
        private string _password;
        public int CompareTo(UserInfo other)
        {
            return String.Compare(this._name, other._name, StringComparison.OrdinalIgnoreCase);
        }
        public string GetUserID()
        {
            return _id;
        }
        public string GetName()
        {
            return _name;
        }
        public string GetEmail()
        {
            return _email;
        }
        public string GetType()
        {
            return _type;
        }
        public string GetUsername()
        {
            return _username;
        }
        public string GetPassword()
        {
            return _password;
        }
        public void SetUserID(string id)
        {
            _id = id;
        }

        public void SetName(string name)
        {
            _name = name;
        }
        public void SetEmail(string email)
        {
            _email = email;
        }
        public void SetType(string type)
        {
            _type = type;
        }
        public void SetUsername(string username)
        {
            _username = username;
        }
        public void SetPassword(string password)
        {
            _password = password;
        }
    }
}
