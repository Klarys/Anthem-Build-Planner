using System;
using System.Collections.Generic;
using System.Text;

namespace AnthemBuilderLibrary
{
    /// <summary>
    /// Class used in getting game servers status from a server. Serializable
    /// </summary>
    [Serializable]
    public class GameStatus
    {
        public bool EUstatus; //true - working, false - not working
        public bool NAstatus;
    }
}
