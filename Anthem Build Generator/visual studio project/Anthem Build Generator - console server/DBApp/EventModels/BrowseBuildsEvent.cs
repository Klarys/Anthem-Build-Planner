using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBApp.EventModels
{
    /// <summary>
    /// Event used for switching to BrowseBuildsView
    /// </summary>
    public class BrowseBuildsEvent
    {
        public bool Saved { get; private set; }

        /// <summary>
        /// Parameter determines if user want's to browse saved or all builds(default). 
        /// </summary>
        /// <param name="saved"></param>
        public BrowseBuildsEvent(bool saved = false)
        {
            Saved = saved;
        }
    }
}
