using System;
using System.Collections.Generic;
using System.Text;

namespace AnthemBuilderLibrary
{
    /// <summary>
    /// Class containing information about a build
    /// </summary>
    public class Build
    {
        public int BuildId { get; set; }
        public string Name { get; set; }
        public string AdditionalNotes { get; set; }
        public int AuthorId { get; set; }
        public int Rating { get; set; }
        public int StrikeSystemId { get; set; }
        public int AssaultSystemId { get; set; }
        public int SupportSystemId { get; set; }
        public int ClassId { get; set; }

        //additional 
        public string Class { get; set; }
    }
}
