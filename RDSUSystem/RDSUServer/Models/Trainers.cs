using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RDSUServer.Models
{
    public class Trainers
    {
        public int Id { get; set; }
        public virtual Users User { get; set; }

        public string Name { get; set; }

        public string TrainerFullName { get; set; }

        public string Location { get; set; }

        public string Phone { get; set; }

        public bool Status { get; set; }
    }
}
