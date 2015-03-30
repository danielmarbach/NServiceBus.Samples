using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB14WarehouseService.Messages.Commands
{
    public class CollectItems
    {
        public String OrderId { get; set; }
		public String CartId { get; set; }
    }
}
