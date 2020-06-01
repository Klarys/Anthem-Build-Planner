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
    /// Class managing GameTipsView
    /// </summary>
    public class GameTipsViewModel : Screen
    {
        private IEventAggregator events;
        private string tip1;
        private string tip2;
        private string tip3;
        private string tip4;
        private string tip5;

        public string Tip1
        {
            get { return tip1; }
            set
            {
                tip1 = value;
                NotifyOfPropertyChange(() => Tip1);
            }
        }

        public string Tip2
        {
            get { return tip2; }
            set
            {
                tip2 = value;
                NotifyOfPropertyChange(() => Tip2);
            }
        }

        public string Tip3
        {
            get { return tip3; }
            set
            {
                tip3 = value;
                NotifyOfPropertyChange(() => Tip3);
            }
        }

        public string Tip4
        {
            get { return tip4; }
            set
            {
                tip4 = value;
                NotifyOfPropertyChange(() => Tip4);
            }
        }
        public string Tip5
        {
            get { return tip5; }
            set
            {
                tip5 = value;
                NotifyOfPropertyChange(() => Tip5);
            }
        }

        public GameTipsViewModel(IEventAggregator _events)
        {
           events = _events;

            GeneralTip generalTip = new GeneralTip();
            generalTip.SetTip("Gathering", "You can gather resources for crafting in the freeplay mode!");
            Tip1 = generalTip.ShowTip();
            generalTip.SetTip("Crafting", "You can craft different items in the Forge panel!");
            Tip2 = generalTip.ShowTip();

            ClassTip classTip = new ClassTip();
            classTip.SetTip("Triple dash", "You can hold ctrl key to perform three fast dashes on the interceptor class.");
            Tip3 = classTip.ShowTip();
            classTip.SetTip("Air raid", "You can use your ultimate during flight on the Ranger class.");
            Tip4 = classTip.ShowTip();
            classTip.SetTip("Flying like a bird", "You can hover much longer on the Storm class.");
            Tip5 = classTip.ShowTip();
        }


        /// <summary>
        /// Handles the "Menu" button
        /// </summary>
        public void ReturnToMenu()
        {
            events.PublishOnUIThread(new ReturnToMenuEvent());
        }
    }
}
