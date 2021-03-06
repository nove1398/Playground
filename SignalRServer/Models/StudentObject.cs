﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRServer.Models
{
    public class StudentObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Grade> Grades { get; set; }
        public ClassRoom ClassRoom { get; set; }
    }
}