using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace umvel.challenge.domain.Commons.EntitiesBase
{
    public class AuditableEntity : IAuditableEntity
    {
        public int CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public int LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }
    }
}