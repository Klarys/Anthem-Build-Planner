using AnthemBuilderLibrary;
using Caliburn.Micro;
using DBApp.EventModels;
using DBApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBApp.ViewModels
{
    /// <summary>
    /// Class managing RegisterView
    /// </summary>
    class RegisterViewModel : Screen
    {
        private IEventAggregator _events;

        private string _userName;
        private string _logIn;
        private string _email;
        private string _password;
        private string _description;
        private string _result;
        private User NewUser = new User(); 

        public RegisterViewModel(IEventAggregator events)
        {
            _events = events;
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


        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NewUser.Description = Description;
                NotifyOfPropertyChange(() => Description);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }


        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NewUser.Password = Password;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NewUser.Email = Email;
                NotifyOfPropertyChange(() => Email);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }


        public string LogIn
        {
            get { return _logIn; }
            set
            {
                _logIn = value;
                NewUser.Login = LogIn;
                NotifyOfPropertyChange(() => LogIn);
                NotifyOfPropertyChange(() => CanRegister);
            }
        }


        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NewUser.UserName = UserName;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanRegister);
            }

        }

        /// <summary>
        /// Enables/disables the "Register" button
        /// </summary>
        public bool CanRegister
        {
            get
            {
                if(LogIn != null && LogIn != "" && Password != null && Password.Length > 6 && UserName != null && UserName != "" && Email != null && Email != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Handles the "Register" button, checks if user's input is correct and adds a new user in the database
        /// </summary>
        public void Register()
        {
            bool canRegister = true;
            DataAccess db = new DataAccess();
            if(db.UserExits(LogIn))
            {
                canRegister = false;
                Result = "Incorrect Login!";
            }
            if(db.EmailExits(Email))
            {
                canRegister = false;
                Result = "Incorrect email!";
            }
            if (!IsValidEmail(NewUser.Email))
            {
                canRegister = false;
                Result = "Incorrect email!";
            }
            
            if(canRegister)
            {
                NewUser.Salt = SaltHelper.GetRandomSalt();
                NewUser.Password = NewUser.Password + NewUser.Salt;
                NewUser.Password = HashingHelper.GetHashedPassword(NewUser.Password);
                db.RegisterNewUser(NewUser);
                _events.PublishOnUIThread(new LogOnEventModel(NewUser.Login, NewUser.UserName, db.GetNewUserId(NewUser)));
            }
            else
            {
                Result += "TIP: Password should be min. 6 character long, and only description can be skipped.";
            }
        }
 
        /// <summary>
        /// Handles the "Menu" button
        /// </summary>
        public void Back()
        {
            _events.PublishOnUIThread(new BackToLoginEvent());
        }

        /// <summary>
        /// Checks if the user's email is in correct form
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
