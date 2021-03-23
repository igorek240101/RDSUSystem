using System;
using System.ComponentModel.DataAnnotations;

namespace RDSUServer.Models
{
    public class Athletes
    {
        public virtual Users Id { get; set; }

        [Key] 
        public int Number { get; set; }

        public virtual Trainers Trainer { get; set; }

        public virtual Athletes Athlet { get; set; }

        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool isMale { get; set; }

        public int? Height { get; set; }

        public byte St { get; set; }

        public byte La { get; set; }

        public int Status { get; set; }
    }
}
