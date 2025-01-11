using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentService.Serialization;

namespace StudentService.Model
{
    public class Professor : ISerializable
    {
        public string DisplayText => $"{Name} {Surname} - {Vocation}";
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public string Vocation { get; set; }
        public int YearsOfService { get; set; }
        public List<Subject> Subjects { get; set; }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Surname,
                Name,
                DateOfBirth.ToString("dd.MM.yyyy."),
                Address,
                Phone,
                Email,
                IdNumber,
                Vocation,
                YearsOfService.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Surname = values[1];
            Name = values[2];
            DateOfBirth = DateOnly.ParseExact(values[3], "dd.MM.yyyy.");
            Address = values[4];
            Phone = values[5];
            Email = values[6];
            IdNumber = values[7];
            Vocation = values[8];
            YearsOfService = int.Parse(values[9]);
        }


    }
}
