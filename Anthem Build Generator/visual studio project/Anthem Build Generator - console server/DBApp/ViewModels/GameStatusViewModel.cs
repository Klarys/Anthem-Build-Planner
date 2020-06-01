using AnthemBuilderLibrary;
using Caliburn.Micro;
using DBApp.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace DBApp.ViewModels
{
    /// <summary>
    /// Class managing GameStatusView
    /// </summary>
    public class GameStatusViewModel : Screen
    {
        private IEventAggregator events;
        private string gameNewsLink;
        private string gameNewsTitle;
        private string nastatus;
        private string eustatus;

        public string EUstatus
        {
            get { return eustatus; }
            set
            {
                eustatus = value;
                NotifyOfPropertyChange(() => EUstatus);
            }
        }


        public string NAstatus
        {
            get { return nastatus; }
            set
            {
                nastatus = value;
                NotifyOfPropertyChange(() => NAstatus);
            }
        }


        public string GameNewsTitle
        {
            get { return gameNewsTitle; }
            set
            {
                gameNewsTitle = value;
                NotifyOfPropertyChange(() => GameNewsTitle);
            }
        }


        public string GameNewsLink
        {
            get { return gameNewsLink; }
            set
            {
                gameNewsLink = value;
                NotifyOfPropertyChange(() => GameNewsLink);
            }
        }

        /// <summary>
        /// Gets the required information from the InfoServer
        /// </summary>
        /// <param name="_events"></param>
        public GameStatusViewModel(IEventAggregator _events)
        {
            events = _events;

            GameNews gameNews = GetGameNews();
            GameStatus gameStatus = GetGameStatus();

            GameNewsLink = gameNews.link;
            GameNewsTitle = gameNews.title;
            if(gameStatus.EUstatus)
            {
                EUstatus = "working correctly";
            }
            else
            {
                EUstatus = "server down";
            }
            if (gameStatus.NAstatus)
            {
                NAstatus = "working correctly";
            }
            else
            {
                NAstatus = "server down";
            }
        }

        /// <summary>
        /// Handles the "Menu" button
        /// </summary>
        public void ReturnToMenu()
        {
            
            events.PublishOnUIThread(new ReturnToMenuEvent());
        }

        /// <summary>
        /// Opens the http link for latest in user's browser
        /// </summary>
        public void OpenNews()
        {
            try
            {
                System.Diagnostics.Process.Start(GameNewsLink);
            }
            catch(Exception e)
            {
                System.Diagnostics.Process.Start("https://www.ea.com/pl-pl/games/anthem");
            }
            
        }

        /// <summary>
        /// Handles the Refresh button for servers' status
        /// </summary>
        public void RefreshServerStatus()
        {
            GameStatus gameStatus = GetGameStatus();
            if (gameStatus.EUstatus)
            {
                EUstatus = "working correctly";
            }
            else
            {
                EUstatus = "server down";
            }
            if (gameStatus.NAstatus)
            {
                NAstatus = "working correctly";
            }
            else
            {
                NAstatus = "server down";
            }
        }

        /// <summary>
        /// Handles the Refresh button for game news
        /// </summary>
        public void RefreshGameNews()
        {
            GameNews gameNews = GetGameNews();
            GameNewsLink = gameNews.link;
            GameNewsTitle = gameNews.title;
        }

        /// <summary>
        /// Gets the status of the game servers from the InfoServer
        /// </summary>
        private static GameStatus GetGameStatus()
        {
            ServerMessage request = new ServerMessage();
            ServerMessage response = new ServerMessage();
            request.comment = "SERVERSTATUS";

            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Connect("localhost", 55555);
                    ConnectionHelper.SendMessage(socket, request);
                    response = ConnectionHelper.ReceiveMessage(socket);
                }
            }
            catch (Exception e)
            {
                GameStatus tmp = new GameStatus();
                tmp.EUstatus = false;
                tmp.NAstatus = false;
                response.message = tmp;
                 
            }

            return response.message as GameStatus;
        }

        /// <summary>
        /// Gets latest news from the InfoServer
        /// </summary>
        private static GameNews GetGameNews()
        {
            ServerMessage request = new ServerMessage();
            ServerMessage response = new ServerMessage();
            request.comment = "GAMENEWS";

            try
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Connect("localhost", 55555);
                    ConnectionHelper.SendMessage(socket, request);
                    response = ConnectionHelper.ReceiveMessage(socket);
                }
            }
            catch (Exception e)
            {
                GameNews tmp = new GameNews();
                tmp.title = "UNABLE TO CONNECT TO THE INFO SERVER. PLEASE REFRESH!";
                tmp.link = "https://www.ea.com/pl-pl/games/anthem";
                response.message = tmp;
            }

            return response.message as GameNews;
        }
    }
}
