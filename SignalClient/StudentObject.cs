using System;
using System.Collections.Generic;

namespace SignalClient
{
    public class StudentObject
    {
        public event EventHandler UpdatedInstance;

        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Grade> Grades { get; set; } = new List<Grade>();

        public void UpdateStudent(StudentObject newStudent)
        {
            this.ID = newStudent.ID;
            this.Name = newStudent.Name;
            this.Age = newStudent.Age;
            this.Grades = newStudent.Grades;

            this.UpdatedInstance?.Invoke(this, new EventArgs());
        }
    }
}