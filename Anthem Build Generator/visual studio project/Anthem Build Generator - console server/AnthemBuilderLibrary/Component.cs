using System;
using System.Collections.Generic;
using System.Text;

namespace AnthemBuilderLibrary
{
    /// <summary>
    /// Class containing information about a Component item
    /// </summary>
    public class Component
    {
        public int ComponentId { get; set; }
        public string Name { get; set; }
        public int ShieldReinforcement { get; set; }
        public int ArmorReinforcement { get; set; }
        public string NormalEffect { get; set; }
        public string SpecialEffect { get; set; }
        public int ClassId { get; set; }
        public int RarityId { get; set; }

        public string ComponentName
        {
            get
            {
                return $"{ Name }";
            }
        }
    }
}
