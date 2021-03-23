using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDSUServer.Models
{
    public class Ch_Partnerships
    {
        public int Id { get; set; }
        public virtual Athletes From { get; set; }

        public virtual Athletes To { get; set; }
    }
}
