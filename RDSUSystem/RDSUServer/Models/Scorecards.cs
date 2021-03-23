using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDSUServer.Models
{
    public class Scorecards
    {
        public int Id { get; set; }

        public virtual Athletes Athletes1 { get; set; }

        public virtual Athletes Athletes2 { get; set; }

        public virtual Categories Category { get; set; }

        public virtual Trainers Trainer { get; set; }

        public int Number { get; set; }
    }
}
