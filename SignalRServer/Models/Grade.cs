using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRServer.Models
{
    public class Grade
    {
        public int ID { get; set; }
        public int Score { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}