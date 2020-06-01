using System;
using System.Collections.Generic;
using System.Text;

namespace AnthemBuilderLibrary
{

    /// <summary>
    /// Class containing infomations about a user
    /// </summary>
    public class User
    {
        public int UserId  { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string Description { get; set; }

    }
}
