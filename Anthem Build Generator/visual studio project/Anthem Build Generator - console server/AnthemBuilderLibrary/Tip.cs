using System;
using System.Collections.Generic;
using System.Text;

namespace AnthemBuilderLibrary
{
    /// <summary>
    /// abstract class for tips
    /// </summary>
    public abstract class Tip
    {
        public string Title { get; set; }

        public string Contents { get; set; }

        public abstract void SetTip(string title, string contents);

        public string ShowTip()
        {
            return "► " +Title + ": " + Contents;
        }
    }
}
