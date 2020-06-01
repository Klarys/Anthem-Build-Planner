using System;
using System.Collections.Generic;
using System.Text;

namespace AnthemBuilderLibrary
{
    /// <summary>
    /// Class for tips meant for specific class
    /// </summary>
    public class ClassTip : Tip
    {
        /// <summary>
        /// Sets both title and contents of the tip in correct form
        /// </summary>
        /// <param name="title"></param>
        /// <param name="contents"></param>
        public override void SetTip(string title, string contents)
        {
            Title = "CLASS TIP | " + title;
            Contents = contents + " Can only be used on this class.";
        }
    }
}
