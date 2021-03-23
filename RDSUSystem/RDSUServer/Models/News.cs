using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDSUServer.Models
{
    public class News
    {
        public int Id { get; set; }

        public virtual Trainers Trainer { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public string Text { get; set; }

        public DateTime IssueDate { get; set; }
    }
}
