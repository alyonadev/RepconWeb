using System;
using System.Collections.Generic;

namespace RepconWeb
{
    public partial class Admn
    {
        public string Login { get; set; }
        public string Pwd { get; set; }
        public DateTime? LastSignIn { get; set; }
        public string SignHash { get; set; }
    }
}
