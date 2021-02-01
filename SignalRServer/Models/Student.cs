using System.Collections.Generic;

namespace SignalRServer.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public int? ClassRoomId { get; set; }
        public ClassRoom ClassRoom { get; set; }

        public ICollection<Grade> Grades { get; set; }
    }
}