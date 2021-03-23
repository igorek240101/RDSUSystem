using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RDSUServer.Models
{
    public class Ch_FullNames
    {
        public int Id { get; set; }
        public virtual Athletes Athlet { get; set; }
        public string NewFullName { get; set; }
    }
}
