using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentService.Serialization;
using StudentService.Model.Enums;

namespace StudentService.Model
{
    public class Department : ISerializable
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Professor HeadProfessor { get; set; }
        public List<Professor> Professors { get; set; }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Code,
                Name,
                HeadProfessor.Id.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Code = values[1];
            Name = values[2];
            HeadProfessor = new Professor() { Id = int.Parse(values[3]) };
        }
    }
}
