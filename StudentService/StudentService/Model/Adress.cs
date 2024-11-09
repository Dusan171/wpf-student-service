using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using StudentService.Serialization;

namespace StudentService.Model
{
    public class Adress : ISerializable
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Street,
                Number.ToString(),
                Town,
                Country
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Street = values[1];
            Number = int.Parse(values[2]);
            Town = values[3];
            Country = values[4];
        }
    }
}
