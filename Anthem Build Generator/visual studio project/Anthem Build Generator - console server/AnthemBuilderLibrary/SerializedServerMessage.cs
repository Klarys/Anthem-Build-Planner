using System;
using System.Collections.Generic;
using System.Text;

namespace AnthemBuilderLibrary
{
    /// <summary>
    /// Used in communication with a server, contains serialized message
    /// </summary>
    [Serializable]
    public class SerializedServerMessage
    {
        public Byte[] Data { get; set; }
    }
}
