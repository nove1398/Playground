using SignalRServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRServer.Services
{
    public class StudentService : IStudentService
    {
        public event EventHandler<StudentObject> StudentUpdate;

        private List<StudentObject> Students = new List<StudentObject>();

        public StudentService()
        {
            Students.Add(new StudentObject() { ID = 1, Age = 10, Name = "joe", Grades = new List<Grade> { new Grade { Score = 10, ID = 1 } } });
            Students.Add(new StudentObject() { ID = 2, Age = 10, Name = "joe", Grades = new List<Grade> { new Grade { Score = 10, ID = 1 } } });
            Students.Add(new StudentObject() { ID = 3, Age = 10, Name = "joe", Grades = new List<Grade> { new Grade { Score = 10, ID = 1 } } });
            Students.Add(new StudentObject() { ID = 4, Age = 10, Name = "joe", Grades = new List<Grade> { new Grade { Score = 10, ID = 1 } } });
            Students.Add(new StudentObject() { ID = 5, Age = 10, Name = "joe", Grades = new List<Grade> { new Grade { Score = 10, ID = 1 } } });
            Students.Add(new StudentObject() { ID = 6, Age = 10, Name = "joe", Grades = new List<Grade> { new Grade { Score = 10, ID = 1 } } });
        }

        public async Task RunAsync(CancellationToken stopToken)
        {
            Random random = new Random();

            while (!stopToken.IsCancellationRequested)
            {
                int i = (int)(random.NextDouble() * Students.Count);
                if (i < Students.Count)
                {
                    var student = Students[i];
                    var score = (student.Grades.First().Score = random.Next(50, 100)); ;
                    var grade = student.Grades.First();
                    grade.Score = score;
                    student.Age = random.Next(10, 35);
                    student.Name = student.Name;
                    student.Grades = new List<Grade> { grade };

                    StudentUpdate?.Invoke(this, student);
                }

                await Task.Delay(500, stopToken);
            }
        }
    }
}