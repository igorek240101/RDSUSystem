using System.ComponentModel.DataAnnotations;

namespace RDSUServer.Models
{
    public class Ch_Transfers
    {
        public int Id { get; set; }
        public virtual Athletes Danser { get; set; }

        public virtual Trainers Trainer { get; set; }

        public bool DanserWord { get; set; }

        public bool FromWord { get; set; }

        public bool ToWord { get; set; }
    }
}
