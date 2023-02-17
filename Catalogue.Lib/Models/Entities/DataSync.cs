using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue.Lib.Models.Entities
{
    public class DataSync :BaseEntity
    {
        public string? SyncedBy { get; set; }
        public int ? TotalCount { get; set; }
    }
}
