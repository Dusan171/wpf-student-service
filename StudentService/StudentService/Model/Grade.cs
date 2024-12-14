using System;
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
            return new string[]
           {
                Id.ToString(),
                PassedStudent?.Id.ToString() ?? "0", // Ako PassedStudent nije null, koristi njegov Id, inače 0
                Subject?.Id.ToString() ?? "0", // Isto za Subject
                Value.ToString(),
                Date.ToString("dd.MM.yyyy.")
           };
        }

        public void FromCSV(string[] values)
        {
            if (values.Length != 5) throw new ArgumentException("Invalid number of values in CSV.");

            if (!int.TryParse(values[0], out int id)) throw new FormatException("Invalid Id format.");
            Id = id;

            if (!int.TryParse(values[1], out int studentId)) throw new FormatException("Invalid Student ID format.");
            PassedStudent = new Student() { Id = studentId };

            if (!int.TryParse(values[2], out int subjectId)) throw new FormatException("Invalid Subject ID format.");
            Subject = new Subject() { Id = subjectId };

            if (!int.TryParse(values[3], out int value)) throw new FormatException("Invalid grade value.");
            Value = value;

            if (!DateOnly.TryParseExact(values[4], "dd.MM.yyyy.", out DateOnly date))
                throw new FormatException("Invalid Date format.");
            Date = date;
        }
    }
}
