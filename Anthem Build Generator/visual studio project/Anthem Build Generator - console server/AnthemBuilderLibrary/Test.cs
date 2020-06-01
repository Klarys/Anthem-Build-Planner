using System;
using System.Collections.Generic;
using System.Text;

namespace AnthemBuilderLibrary
{
    /// <summary>
    /// Class used for tests
    /// </summary>
    public class Test
    {
        public int Id { get; set; }

        public int TestCol { get; set; }

        public string FullText
        {
            get
            {
                return $"{ Id } { TestCol }";
            }
        }
    }
}
