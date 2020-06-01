using AnthemBuilderLibrary;
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
    /// Class managing the ShellView
    /// </summary>
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEventModel>, IHandle<BrowseBuildsEvent>, IHandle<ReturnToMenuEvent>, IHandle<ShowBuildDetailsEventModel>,
        IHandle<CreateEditBuildEventModel>, IHandle<RegisterEvent>, IHandle<BackToLoginEvent>, IHandle<ShowGameStatusEvent>, IHandle<GameTipsEvent>
    {
        //private LoginViewModel _loginVM;
        private MainMenuViewModel _mainMenuVM;
        private BrowseBuildsViewModel _browseBuildVM;
        private BuildDetailsViewModel _buildDetailsVM;
        private CreateEditBuildViewModel _createEditBuildVM;
        private RegisterViewModel _registerVM;
        private IEventAggregator _events;
        private SimpleContainer _container;
        private int userId;
        private string login;
        private string userName;
        private int buildId;
        private bool Saved = false;

        /// <summary>
        /// Creates a new instance of LoginView right after the start of the application
        /// </summary>
        /// <param name="events"></param>
        /// <param name="container"></param>
        /// <param name="mainMenuVM"></param>
        /// <param name="browseBuildsVM"></param>
        public ShellViewModel(IEventAggregator events, SimpleContainer container, MainMenuViewModel mainMenuVM, BrowseBuildsViewModel browseBuildsVM)
        {
            _events = events;
            _mainMenuVM = mainMenuVM;
            _browseBuildVM = browseBuildsVM;
            _container = container;

            _events.Subscribe(this);

            ActivateItem(_container.GetInstance<LoginViewModel>()); 
        }

        /// <summary>
        /// Changes the currently shown view to MainMenuView
        /// </summary>
        /// <param name="message"></param>
        public void Handle(LogOnEventModel message) 
        {
            login = message.Login;
            userName = message.UserName;
            userId = message.UserId;
            _mainMenuVM.UserName = userName;
            ActivateItem(_mainMenuVM);
        }

        /// <summary>
        /// Changes the currently shown view to BrowseBuildsView
        /// </summary>
        /// <param name="message"></param>
        public void Handle(BrowseBuildsEvent message)
        {
            Saved = message.Saved;
            _browseBuildVM = new BrowseBuildsViewModel(_events, userId, Saved);
            ActivateItem(_browseBuildVM);
        }

        /// <summary>
        /// Changes the currently shown view to MainMenuView
        /// </summary>
        /// <param name="message"></param>
        public void Handle(ReturnToMenuEvent message)
        {
            ActivateItem(_mainMenuVM);
        }

        /// <summary>
        /// Changes the currently shown view to BuildDetailsView
        /// </summary>
        /// <param name="message"></param>
        public void Handle(ShowBuildDetailsEventModel message)
        {
            buildId = message.buildId;
            _buildDetailsVM = new BuildDetailsViewModel(_events, buildId, userId, Saved);         
            ActivateItem(_buildDetailsVM);
        }

        /// <summary>
        /// Changes the currently shown view to CreateEditBuildView with different default values
        /// </summary>
        /// <param name="message"></param>
        public void Handle(CreateEditBuildEventModel message)
        {
            if(message.createNew)
            {
                _createEditBuildVM = new CreateEditBuildViewModel(_events, message.createNew, userId);
            }
            else
            {
                _createEditBuildVM = new CreateEditBuildViewModel(_events, message.createNew, userId, message.editBuildId);
            }
            ActivateItem(_createEditBuildVM);
            
        }

        /// <summary>
        /// Changes the currently shown view to RegisterView
        /// </summary>
        /// <param name="message"></param>
        public void Handle(RegisterEvent message)
        {
            _registerVM = new RegisterViewModel(_events);
            ActivateItem(_registerVM);
        }

        /// <summary>
        /// Changes the currently shown view to LoginView
        /// </summary>
        /// <param name="message"></param>
        public void Handle(BackToLoginEvent message)
        {
            ActivateItem(_container.GetInstance<LoginViewModel>());
        }

        /// <summary>
        /// Changes the currently shown view to GameStatusView
        /// </summary>
        /// <param name="message"></param>
        public void Handle(ShowGameStatusEvent message)
        {
            ActivateItem(_container.GetInstance<GameStatusViewModel>());
        }

        /// <summary>
        /// Changes the currently shown view to GameTipsView
        /// </summary>
        /// <param name="message"></param>
        public void Handle(GameTipsEvent message)
        {
            ActivateItem(_container.GetInstance<GameTipsViewModel>());
        }
    }
}
