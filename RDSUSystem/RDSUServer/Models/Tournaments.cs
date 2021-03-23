using System;

namespace RDSUServer.Models
{
    public class Tournaments
    {
        public int Id { get; set; }

        public virtual RDSUWorkers Planner { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }


        public string PlannerFullName { get; set; }

        public DateTime Date { get; set; }
    }
}
