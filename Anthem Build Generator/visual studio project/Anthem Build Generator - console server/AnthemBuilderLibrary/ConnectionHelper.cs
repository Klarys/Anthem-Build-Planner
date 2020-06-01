using AnthemBuilderLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AnthemBuilderLibrary
{
    /// <summary>
    /// Class containing methods used for server connection
    /// </summary>
    public static class ConnectionHelper
    {
        /// <summary>
        /// Receiving a message
        /// </summary>
        /// <param name="socket"></param>
        /// <returns>Message in deserialized form</returns>
        public static ServerMessage ReceiveMessage(Socket socket)
        {         
            IEnumerable<Byte> receivedData = new Byte[0];
            int numberOfBytesRead = 0;
            SerializedServerMessage serializedMessage = new SerializedServerMessage();

            using (NetworkStream stream = new NetworkStream(socket))
            {
                if (stream.CanRead)
                {
                    do
                    {
                        byte[] readBuffer = new byte[1024];
                        numberOfBytesRead = stream.Read(readBuffer, 0, readBuffer.Length);
                        Byte[] tmp = new Byte[numberOfBytesRead];
                        Array.Copy(readBuffer, 0, tmp, 0, numberOfBytesRead);
                        receivedData = receivedData.Concat(tmp);


                    } while (stream.DataAvailable);

                    serializedMessage.Data = receivedData.ToArray();
                }
                else
                {
                    Console.WriteLine("Unable to read the stream.");
                } 
            }

            return DeserializeMessage(serializedMessage);
        }

        /// <summary>
        /// Sending a message
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="message">Message in deserialized form</param>
        public static void SendMessage(Socket socket, ServerMessage message)
        {
            using (NetworkStream stream = new NetworkStream(socket))
            {
                SerializedServerMessage serializedMessage = SerializeMessage(message);
                stream.Write(serializedMessage.Data, 0, serializedMessage.Data.Length);
            }
        }

        /// <summary>
        /// Serializes the message
        /// </summary>
        /// <param name="message">Message in deserialized form</param>
        /// <returns>Message in serialized form</returns>
        public static SerializedServerMessage SerializeMessage(ServerMessage message)
        {
            using (var stream = new MemoryStream())
            {
                SerializedServerMessage serializedMessage = new SerializedServerMessage();
                (new BinaryFormatter()).Serialize(stream, message);
                serializedMessage.Data = stream.ToArray();
                return serializedMessage;
            }
        }

        /// <summary>
        /// Deserializes the message
        /// </summary>
        /// <param name="serializedMessage">Message in serialized form</param>
        /// <returns>Message in deserialized form</returns>
        public static ServerMessage DeserializeMessage(SerializedServerMessage serializedMessage)
        {
            using (var stream = new MemoryStream(serializedMessage.Data))
            {
                return (new BinaryFormatter()).Deserialize(stream) as ServerMessage;
            }

        }
    }

}
