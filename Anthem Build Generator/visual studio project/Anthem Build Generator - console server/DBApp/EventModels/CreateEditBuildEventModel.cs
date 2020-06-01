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
    public class CreateEditBuildEventModel
    {
        public bool createNew;
        public int editBuildId;

        /// <summary>
        /// Parameter determines if user want's to create a new build or edit an existing one
        /// </summary>
        /// <param name="saved"></param>
        public CreateEditBuildEventModel(bool _createNew, int _editBuildId = -1)
        {
            createNew = _createNew;
            if(_editBuildId != -1)
            {
                editBuildId = _editBuildId;
            }           
        }
    }
}
