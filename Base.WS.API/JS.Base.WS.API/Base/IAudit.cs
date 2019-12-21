using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Base.WS.API.Base
{
    interface IAudit
    {
        DateTime? CreationTime { get; set; }
        long? CreatorUserId { get; set; }
        DateTime? LastModificationTime { get; set; }
        long? LastModifierUserId { get; set; }
        long? DeleterUserId { get; set; }
        DateTime? DeletionTime { get; set; }
        bool IsActive { get; set; }
        bool IsDeleted { get; set; }
    }
}
