using System;
using System.Collections.Generic;
using System.Text;

namespace AnthemBuilderLibrary
{
    /// <summary>
    /// Class containing information about a Support system gear
    /// </summary>
    public class SupportSystem
    {
        public int SupportSystemId { get; set; }
        public string Name { get; set; }
        public int Charges { get; set; }
        public int Recharge { get; set; }
        public int Radius { get; set; }
        public int ClassId { get; set; }
        public int RarityId { get; set; }
    }
}
