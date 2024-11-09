using StudentService.Model.Enums;
using System;
using StudentService.Serialization;

namespace StudentService.Model
{
    public class Student : ISerializable
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Index { get; set; }
        public int YearOfStudy { get; set; }
        public StudentStatus Status { get; set; }
        public double AvgGrade { get; set; }


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
                Index,
                YearOfStudy.ToString(),
                Status.ToString(),
                AvgGrade.ToString(),
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
            Index = values[7];
            YearOfStudy = int.Parse(values[8]);
            Status = (StudentStatus)Enum.Parse(typeof(StudentStatus), values[9]);
            AvgGrade = int.Parse(values[10]);
        }




    }
}