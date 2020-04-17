using System;
using System.Collections.Generic;

namespace RepconWeb
{
    public partial class Session
    {
        public Session() => SessionProc = new HashSet<SessionProc>();
        public int SessionId { get; set; }
        public int? PcId { get; set; }
        public string Login { get; set; }
        public DateTime? StartT { get; set; }
        public DateTime? EndT { get; set; }
        public virtual Pc Pc { get; set; }
        public virtual ICollection<SessionProc> SessionProc { get; set; }
    }
}
