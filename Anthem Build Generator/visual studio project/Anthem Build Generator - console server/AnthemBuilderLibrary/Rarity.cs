using System;
using System.Collections.Generic;
using System.Text;

namespace AnthemBuilderLibrary
{
    /// <summary>
    /// Class containing Id, Name and Power of an ingame Rarity level
    /// </summary>
    public class Rarity
    {
        public int RarityId { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
    }
}
