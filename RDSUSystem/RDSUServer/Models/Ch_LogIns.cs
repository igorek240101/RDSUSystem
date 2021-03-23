using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDSUServer.Models
{
    public class Ch_LogIns
    {
        public int Id { get; set; }

        public virtual Trainers Trainer { get; set; }

        public string FullName { get; set; }

        public DateTime BirthDay { get; set; }

        public bool isMale { get; set; }

        public string Email { get; set; }
    }
}
