using StudentService.Model;
using StudentService.Serialization;
using System.Collections.Generic;
using StudentService.Observer;

namespace StudentService.DAO
{
    public class GradeDao
    {
        private readonly List<Grade> _grades;
        private readonly Storage<Grade> _storage;

        public DAOSubject StudentGrade;

        public GradeDao()
        {
            _storage = new Storage<Grade>("grades.txt");
            _grades = _storage.Load() ?? new List<Grade>();  // Safe guard in case Load() returns null
            StudentGrade = new DAOSubject();
        }

        private int GenerateId()
        {
            return _grades.Count == 0 ? 1 : _grades[^1].Id + 1;
        }

        public Grade Create(Grade grade)
        {
            grade.Id = GenerateId();
            _grades.Add(grade);
            _storage.Save(_grades);
            StudentGrade.NotifyObservers();
            return grade;
        }

        public Grade? UpdateGrade(Grade grade)
        {
            var oldGrade = GetById(grade.Id);
            if (oldGrade == null) return null;

            oldGrade.PassedStudent = grade.PassedStudent;
            oldGrade.Subject = grade.Subject;
            oldGrade.Value = grade.Value;
            oldGrade.Date = grade.Date;

            _storage.Save(_grades);
            StudentGrade.NotifyObservers();
            return oldGrade;
        }

        public Grade? RemoveGrade(int id)
        {
            var grade = GetById(id);
            if (grade == null) return null;

            _grades.Remove(grade);
            _storage.Save(_grades);
            StudentGrade.NotifyObservers();
            return grade;
        }

        private Grade? GetById(int id)
        {
            return _grades.Find(g => g.Id == id);
        }

        public IEnumerable<Grade> GetAll() // Use IEnumerable for more flexibility
        {
            return _grades;
        }
    }
}
