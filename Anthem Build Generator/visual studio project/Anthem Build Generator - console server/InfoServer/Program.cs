using AnthemBuilderLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace InfoServer
{
    /// <summary>
    /// InfoServer uses tcp socket connection to provide game's servers status and game's latest news to DBApp
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            GameNews gameNews = new GameNews();
            GameStatus gameStatus = new GameStatus();
            int dotsInLink = 0;
            bool incorrectLink = true;
            
            Console.WriteLine("Server starting, please pick starting parameters.");
            char input = '0';
            do
            {
                Console.WriteLine("Please pick NA server status, working/not working (w/n):");
                input = Char.ToLower(Console.ReadKey(true).KeyChar);
                Console.WriteLine(input);
                if (input == 'w')
                {
                    gameStatus.NAstatus = true;
                }
                else if (input == 'n')
                {
                    gameStatus.NAstatus = false;
                }
            } while (input != 'w' && input != 'n');

            input = '0';
            do
            {
                Console.WriteLine("Please pick EU server status, working/not working (w/n):");
                input = Char.ToLower(Console.ReadKey(true).KeyChar);
                if (input == 'w')
                {
                    gameStatus.EUstatus = true;
                }
                else if (input == 'n')
                {
                    gameStatus.EUstatus = false;
                }
            } while (input != 'w' && input != 'n');

            Console.WriteLine("Please pick a title for the latest news:");
            gameNews.title = Console.ReadLine();
            Console.WriteLine("Please input the link for the latest news:");
            do
            {
                dotsInLink = 0;
                gameNews.link = Console.ReadLine();
                for (int i = 1; i < gameNews.link.Length; i++)
                {
                    if(gameNews.link[i] == '.')
                    {
                        dotsInLink++;
                    }
                    if(dotsInLink==2)
                    {
                        incorrectLink = false;
                    }
                }

            } while (incorrectLink);
            
            //Console.WriteLine(gameStatus.NAstatus + " " + gameStatus.EUstatus + " " + gameInfo.title + " " + gameInfo.link);

            Console.WriteLine("Server starting...");

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int port = 55555;
            int queue = 15;

            try
            {
                IPAddress ip = IPAddress.Any;
                IPEndPoint endPoint = new IPEndPoint(ip, port);

                socket.Bind(endPoint);
                socket.Listen(queue);

                while(true)
                {
                    Socket connectionSocket = socket.Accept();

                    Task.Run(() =>
                    {
                        Console.WriteLine("New connection.");

                        ServerMessage receivedMessage = ConnectionHelper.ReceiveMessage(connectionSocket);
                        ServerMessage response = new ServerMessage();

                        if (receivedMessage.comment == "SERVERSTATUS")
                        {
                            response.comment = "SERVERSTATUS";
                            Console.WriteLine("Client requested: serverstatus check.");
                            response.message = gameStatus;
                        }
                        else if (receivedMessage.comment == "GAMENEWS")
                        {
                            response.comment = "GAMENEWS";
                            Console.WriteLine("Client requested: gamenews check.");
                            response.message = gameNews;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect request from the client.");
                        }

                        Console.WriteLine("Sending a response for client's request...");
                        ConnectionHelper.SendMessage(connectionSocket, response);
                        Console.WriteLine("Response sent.");
                    });

                }

            }
            catch(SocketException e)
            {
                Console.WriteLine($"Socket exception!  {e}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Other exception:  {e}");
            }
            finally
            {
                Console.WriteLine("Server crashed.");
            }
        }

    }
}
