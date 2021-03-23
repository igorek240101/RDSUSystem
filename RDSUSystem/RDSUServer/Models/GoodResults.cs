using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RDSUServer.Models
{
    public class GoodResults
    {
        public int Id { get; set; }
        public virtual Scorecards Scorecard { get; set; }

        public int Placement { get; set; }

        public int Points { get; set; }
    }
}
