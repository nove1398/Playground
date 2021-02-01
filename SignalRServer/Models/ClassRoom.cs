using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRServer.Models
{
    public class ClassRoom
    {
        public int ClassRoomId { get; set; }
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}