using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue.Lib.Models.Dto
{
    public class DataSyncDto
    {
        public int Id { get; set; }
        public bool IsWaterBodyPresent { get; set; }
        public bool HasBeenVisited { get; set; }
        public DateTime Date { get; set; }
        public int LastUpdatedBy { get; set; }
    }
}
