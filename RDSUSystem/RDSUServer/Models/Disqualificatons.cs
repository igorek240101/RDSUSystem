using System.ComponentModel.DataAnnotations;

namespace RDSUServer.Models
{
    public class Disqualificatons
    {
        public int Id { get; set; }
        public virtual Scorecards Scorecard { get; set; }

        public int Period { get; set; }

    }
}
