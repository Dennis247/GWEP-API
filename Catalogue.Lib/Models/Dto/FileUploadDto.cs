using Catalogue.Lib.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue.Lib.Models.Dto
{

    public class FileUploadDto
    {
        public int Id { get; set; }
        public string filePath { get; set; }
        public string fileName { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }

    }
}
