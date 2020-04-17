using System;
using System.Collections.Generic;

namespace RepconWeb
{
    public partial class Classroom
    {
        public Classroom() => Pc = new HashSet<Pc>();
        public int CrId { get; set; }
        public int? ClassNum { get; set; }
        public int? PlaceNum { get; set; }
        public virtual ICollection<Pc> Pc { get; set; }
    }
}
