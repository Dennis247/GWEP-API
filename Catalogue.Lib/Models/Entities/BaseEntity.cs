using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue.Lib.Models.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Created = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public DateTime Created { get; set; }
    }
}
