using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace umvel.challenge.domain.Commons.EntitiesBase
{
    public interface IAuditableEntity
    {
        int CreatedBy { get; set; }

        DateTime Created { get; set; }

        int LastModifiedBy { get; set; }

        DateTime? LastModified { get; set; }
    }
}
