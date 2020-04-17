using System;
using System.Collections.Generic;

namespace RepconWeb
{
    public partial class Process
    {
        public Process() => SessionProc = new HashSet<SessionProc>();
        public int ProcId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int? Size { get; set; }
        public virtual ICollection<SessionProc> SessionProc { get; set; }
    }
}
