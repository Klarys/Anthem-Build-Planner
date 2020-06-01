using System;
using System.Collections.Generic;
using System.Text;

namespace AnthemBuilderLibrary
{
    /// <summary>
    /// Used in communication with a server, contains deserialized message
    /// </summary>
    [Serializable]
    public class ServerMessage
    {
        /// <summary>
        /// contains keywords informing a receiver what kind of response sender wants
        /// </summary>
        public string comment;
        public object message;

        public static implicit operator ServerMessage(GameStatus v)
        {
            throw new NotImplementedException();
        }
    }
}
