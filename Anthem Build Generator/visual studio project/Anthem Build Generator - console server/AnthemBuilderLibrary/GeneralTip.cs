using System;
using System.Collections.Generic;
using System.Text;

namespace AnthemBuilderLibrary
{
    /// <summary>
    /// Class for general tips
    /// </summary>
    public class GeneralTip : Tip
    {
        /// <summary>
        /// Sets both title and contents of the tip in correct form
        /// </summary>
        /// <param name="title"></param>
        /// <param name="contents"></param>
        public override void SetTip(string title, string contents)
        {
            Title = "GENERAL TIP | " + title;
            Contents = contents + " Can be used on every class.";
        }
    }
}
