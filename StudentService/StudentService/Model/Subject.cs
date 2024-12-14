using StudentService.Model.Enums;
using StudentService.Serialization;
using System;
using System.Collections.Generic;

namespace StudentService.Model
{
    public class Subject : ISerializable
    {
        public int Id { get; set;}
        public string Code { get; set; }
        public string Name { get; set; }
        public Semester Semester { get; set; }
        public int YearOfStudy { get; set; }
        public Professor Professor { get; set; }
        public int Espb { get; set; }
        public List<Student> PassedStudents { get; set; }
        public List<Student> AttendingStudents { get; set; }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Code,
                Name,
                Semester.ToString(),
                YearOfStudy.ToString(),
                Professor.Id.ToString(),
                Espb.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            if (values.Length < 7) throw new ArgumentException("Insufficient CSV values.");
            Id = int.Parse(values[0]);
            Code = values[1];
            Name = values[2];

            if (!Enum.TryParse(values[3], out Semester semester))
            {
                throw new ArgumentException($"Invalid semester value: {values[3]}");
            }
            Semester = semester;
            YearOfStudy = int.Parse(values[4]);
            Professor = new Professor() { Id = int.Parse(values[5]) };
            Espb = int.Parse(values[6]);

        }
    }
}
