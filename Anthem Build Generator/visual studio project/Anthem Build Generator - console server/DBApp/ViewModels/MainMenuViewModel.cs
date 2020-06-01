using Caliburn.Micro;
using DBApp.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBApp.ViewModels
{
    /// <summary>
    /// Class managing MainMenuView
    /// </summary>
    public class MainMenuViewModel : Screen
    {
        private string _login;
        private string _userName;
        private IEventAggregator _events;

        public MainMenuViewModel(IEventAggregator events)
        {
            _events = events;
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

        /// <summary>
        /// Handles the "Browse Builds" button
        /// </summary>
        public void Browse()
        {
            _events.PublishOnUIThread(new BrowseBuildsEvent(false));
        }

        /// <summary>
        /// Handles the "Create Build" button
        /// </summary>
        public void CreateBuild()
        {
            _events.PublishOnUIThread(new CreateEditBuildEventModel(true));
        }

        /// <summary>
        /// Handles the "Saved builds" button
        /// </summary>
        public void SavedBuilds()
        {
            _events.PublishOnUIThread(new BrowseBuildsEvent(true));
        }

        /// <summary>
        /// Handles the "Game Status" button
        /// </summary>
        public void GameStatus()
        {
            _events.PublishOnUIThread(new ShowGameStatusEvent());
        }

        /// <summary>
        /// Handles the "Basic tips" button
        /// </summary>
        public void GameTips()
        {
            _events.PublishOnUIThread(new GameTipsEvent());
        }
    }
}
