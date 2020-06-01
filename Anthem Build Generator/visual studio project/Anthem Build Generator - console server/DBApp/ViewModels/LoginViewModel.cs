using AnthemBuilderLibrary;
using Caliburn.Micro;
using DBApp.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBApp.Helpers;

namespace DBApp.ViewModels
{
    /// <summary>
    /// Class managing LoginView
    /// </summary>
    public class LoginViewModel : Screen
    {
        private string _login;
        private string _password;
        private string _result;
        private IEventAggregator _events;

        public LoginViewModel(IEventAggregator events)
        {
            _events = events;
        }

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                NotifyOfPropertyChange(() => Login);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                NotifyOfPropertyChange(() => Result);
            }
        }

        /// <summary>
        /// Handles the "Log in" button, checks if user's login and password are correct
        /// </summary>
        public void LogOn() 
        {
            if(Login?.Length > 0 && Password?.Length > 0)
            {
                DataAccess db = new DataAccess();

                string salt = db.GetSalt(Login);
                string hashedPassword = Password;
                hashedPassword = hashedPassword + salt;
                hashedPassword = HashingHelper.GetHashedPassword(hashedPassword);
                List<User> tmp = db.GetUser(Login, hashedPassword);
                if (tmp.Any())
                {
                    Result = "Zalogowano pomyślnie!";
                    _events.PublishOnUIThread(new LogOnEventModel(tmp[0].Login, tmp[0].UserName, tmp[0].UserId));
                }
                else
                {
                    Result = "Niepoprawne dane logowania";
                }
            }
            else
            {
                Result = "Wpisz dane logowania!";
            }
        }

        /// <summary>
        /// Handles the "Register" button
        /// </summary>
        public void Register()
        {
            _events.PublishOnUIThread(new RegisterEvent());
        }


    }
}
