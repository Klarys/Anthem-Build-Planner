using System;
using System.Collections.Generic;
using System.Text;

namespace AnthemBuilderLibrary
{
    /// <summary>
    /// Class containing information about a Weapon gear
    /// </summary>
    public class Weapon
    {
        public int WeaponId { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public int Rpm { get; set; }
        public int Ammo { get; set; }
        public int OptimalRange { get; set; }
        public string SpecialEffect { get; set; }
        public int ClassId { get; set; }
        public int RarityId { get; set; }
    }
}
