using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBApp.EventModels
{
    /// <summary>
    /// Event used for switching to BuildDetailsView
    /// </summary>
    public class ShowBuildDetailsEventModel : Screen
    {
        public int buildId;
        public ShowBuildDetailsEventModel(int buildid)
        {
            buildId = buildid;
        }
    }
}
