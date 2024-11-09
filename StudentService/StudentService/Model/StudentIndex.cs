using StudentService.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StudentService.Model
{
    public class StudentIndex : ISerializable
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public int RegisterNumber { get; set; }
        public int RegisterYear { get; set; }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                CourseCode,
                RegisterNumber.ToString(),
                RegisterYear.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            CourseCode = values[1];
            RegisterNumber = int.Parse(values[2]);
            RegisterYear = int.Parse(values[3]);
        }

    }
}
