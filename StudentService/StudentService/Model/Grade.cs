using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using StudentService.Serialization;

namespace StudentService.Model 
{
    public class Grade : ISerializable
    {
        public int Id { get; set; }
        public Student PassedStudent { get; set; }
        public Subject Subject { get; set; }
        public int Value { get; set; }
        public DateOnly Date { get; set; }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                PassedStudent.Id.ToString(),
                Subject.Id.ToString(),
                Value.ToString(),
                Date.ToString("dd.MM.yyyy."),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            PassedStudent = new Student() { Id = int.Parse(values[1]) };
            Subject = new Subject() { Id = int.Parse(values[2]) };
            Value = int.Parse(values[3]);
            Date = DateOnly.ParseExact(values[4], "dd.MM.yyyy.");
        }
    }
}
