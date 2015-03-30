using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB14WarehouseService.Messages.Events
{
    public interface IItemsCollected
    {
        String OrderId { get; set; }
    }
}
