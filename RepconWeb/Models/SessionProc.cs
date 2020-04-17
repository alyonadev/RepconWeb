using System;
using System.Collections.Generic;

namespace RepconWeb
{
    public partial class SessionProc
    {
        public int SpId { get; set; }
        public int? ProcId { get; set; }
        public int? SessionId { get; set; }
        public DateTime? StartProc { get; set; }
        public DateTime? EndProc { get; set; }
        public virtual Process Proc { get; set; }
        public virtual Session Session { get; set; }
    }
}
