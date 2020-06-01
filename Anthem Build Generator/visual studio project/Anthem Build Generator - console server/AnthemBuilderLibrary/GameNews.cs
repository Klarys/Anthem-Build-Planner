using System;
using System.Collections.Generic;
using System.Text;

namespace AnthemBuilderLibrary
{
    /// <summary>
    /// Class used in getting latest news from a server, 
    /// contains the title and the http link for the official post. Serializable
    /// </summary>
    [Serializable]
    public class GameNews
    {
        public string title;
        public string link;
    }
}
