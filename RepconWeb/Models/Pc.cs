using System;
using System.Collections.Generic;

namespace RepconWeb
{
    public partial class Pc
    {
        public Pc() => Session = new HashSet<Session>();
        public int PcId { get; set; }
        public int? CrId { get; set; }
        public string PcType { get; set; }
        public string Macaddr { get; set; }

        public virtual Classroom Cr { get; set; }
        public virtual ICollection<Session> Session { get; set; }
    }
}
