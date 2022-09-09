using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    //general properties for any entity
    public abstract record AuditableEntity
    {
        public DateTime created { get; set; }
        public string createdBy { get; set; }
        public DateTime? lastModified { get; set; }
        public string lastModifiedBy { get; set; }

    }
}
