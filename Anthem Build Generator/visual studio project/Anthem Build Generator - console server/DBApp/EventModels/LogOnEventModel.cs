using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBApp.EventModels
{
    /// <summary>
    /// Event used after user logs in
    /// </summary>
    public class LogOnEventModel
    {
        private string _login;
        private string _userName;

        private int _userId;

        public LogOnEventModel(string login, string userName, int userId)
        {
            Login = login;
            UserName = userName;
            UserId = userId;
        }

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
            }
        }
      public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
            }
        }
    }
}
