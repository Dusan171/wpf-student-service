using StudentService.Model;
using StudentService.Observer;
using StudentService.Serialization;
using System.Collections.Generic;

namespace StudentService.DAO
{
    public class StudentDao
    {
        private readonly List<Student> _students;
        private readonly Storage<Student> _storage;

        public DAOSubject StudentSubject;

        public StudentDao()
        {
            _storage = new Storage<Student>("students.txt");
            _students = _storage.Load();
            StudentSubject = new DAOSubject();
        }

        private int GenerateId()
        {
            if (_students.Count == 0) return 1;
            return _students[^1].Id + 1;
        }

        public Student Create(Student student)
        {
            student.Id = GenerateId();
            _students.Add(student);
            _storage.Save(_students);
            StudentSubject.NotifyObservers();
            return student;
        }

        public Student? UpdateStudent(Student student)
        {
            Student? oldStudent = GetById(student.Id);
            if (oldStudent == null) return null;

            oldStudent.Surname = student.Surname;
            oldStudent.Name = student.Name;
            oldStudent.DateOfBirth = student.DateOfBirth;
            oldStudent.Address = student.Address;
            oldStudent.Phone = student.Phone;
            oldStudent.Email = student.Email;
            oldStudent.Index = student.Index;
            oldStudent.YearOfStudy = student.YearOfStudy;
            oldStudent.Status = student.Status;
            oldStudent.AvgGrade = student.AvgGrade;

            _storage.Save(_students);
            StudentSubject.NotifyObservers();
            return oldStudent;
        }

        public Student? RemoveStudent(int id)
        {
            Student? student = GetById(id);
            if (student == null) return null;

            _students.Remove(student);
            _storage.Save(_students);
            StudentSubject.NotifyObservers();
            return student;
        }

        private Student? GetById(int id)
        {
            return _students.Find(s => s.Id == id);
        }

        public List<Student> GetAll()
        {
            return _students;
        }
    }
}
