using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RDSUServer.Models
{
    public class RDSUWorkers
    {
        public int Id { get; set; }
        public virtual Users User { get; set; }

        public bool Status { get; set; }
    }
}
