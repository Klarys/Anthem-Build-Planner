using AnthemBuilderLibrary;
using Caliburn.Micro;
using DBApp.EventModels;
using DBApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DBApp.ViewModels
{
    /// <summary>
    /// Class managing BrowseBuildsView
    /// </summary>
    public class BrowseBuildsViewModel : Screen
    {
        
        private IEventAggregator _events;
        public BindableCollection<Build> Builds { get; set; }

        private string _selectedBuildName;

        private string _selectedBuildAdditionalNotes;

        private string _selectedBuildClassIcon;

        private Build _selectedBuild;

        private int UserId;

        public string SelectedBuildAdditionalNotes
        {
            get { return _selectedBuildAdditionalNotes; }
            set
            {
                _selectedBuildAdditionalNotes = value;
                NotifyOfPropertyChange(() => SelectedBuildAdditionalNotes);
            }
        }

        public string SelectedBuildName
        {
            get { return _selectedBuildName; }
            set
            {
                _selectedBuildName = value;
                NotifyOfPropertyChange(() => SelectedBuildName);
            }
        }

        public string SelectedBuildClassIcon
        {
            get { return _selectedBuildClassIcon; }
            set
            {
                _selectedBuildClassIcon = value;
                NotifyOfPropertyChange(() => SelectedBuildClassIcon);
            }
        }

        public Build SelectedBuild
        {
            get { return _selectedBuild; }
            set
            {
                _selectedBuild = value;
                SelectedBuildName = SelectedBuild.Name;
                SelectedBuildAdditionalNotes = SelectedBuild.AdditionalNotes;
                switch (SelectedBuild.ClassId)
                {
                    case 1:
                        SelectedBuildClassIcon = "/Images/RangerIcon.jpg";
                        break;
                    case 2:
                        SelectedBuildClassIcon = "/Images/ColossusIcon.jpg";
                        break;
                    case 3:
                        SelectedBuildClassIcon = "/Images/StormIcon.jpg";
                        break;
                    case 4:
                        SelectedBuildClassIcon = "/Images/InterceptorIcon.jpg";
                        break;
                }
                
                NotifyOfPropertyChange(() => SelectedBuild);
                NotifyOfPropertyChange(() => SelectedBuildName);
                NotifyOfPropertyChange(() => SelectedBuildAdditionalNotes);
                NotifyOfPropertyChange(() => SelectedBuildClassIcon);
            }
        }



        public BrowseBuildsViewModel(IEventAggregator events, int userId, bool Saved = false)
        {
            _events = events;
            UserId = userId;
            //pobranie listy buildow z bazy
            DataAccess db = new DataAccess();
            if(Saved == false)
            {
                Builds = new BindableCollection<Build>(db.GetBuildsWithClasses());
            }
            else
            {
                Builds = new BindableCollection<Build>(db.GetSavedBuildsWithClasses(UserId));
            }
            
        }

        /// <summary>
        /// Handles the "Menu" button
        /// </summary>
        public void ReturnToMenu()
        {
            _events.PublishOnUIThread(new ReturnToMenuEvent());
        }

        /// <summary>
        /// Handles the "Show Build Details" button
        /// </summary>
        public void ShowBuildDetails()
        {
            if(SelectedBuild != null)
            {
                _events.PublishOnUIThread(new ShowBuildDetailsEventModel(SelectedBuild.BuildId));
            }
        }

    }
}
