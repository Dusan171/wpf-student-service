using StudentService.Model;
using StudentService.Observer;
using StudentService.Serialization;
using System.Collections.Generic;

namespace StudentService.DAO
{
    public class GradeDao
    {
        private readonly List<Grade> _grades;
        private readonly Storage<Grade> _storage;
        public DAOSubject GradeSubject;

        public GradeDao()
        {
            _storage = new Storage<Grade>("grades.txt");
            _grades = _storage.Load();
            GradeSubject = new DAOSubject();
        }

        private int GenerateId()
        {
            if (_grades.Count == 0) return 1;
            return _grades[^1].Id + 1;
        }

        public Grade Create(Grade grade)
        {
            grade.Id = GenerateId();
            _grades.Add(grade);
            _storage.Save(_grades);
            GradeSubject.NotifyObservers();
            return grade;
        }

        public Grade? UpdateGrade(Grade grade)
        {
            Grade? oldGrade = GetById(grade.Id);
            if (oldGrade == null) return null;

            oldGrade.Value = grade.Value;
            oldGrade.Date = grade.Date;
            oldGrade.PassedStudent = grade.PassedStudent;
            oldGrade.Subject = grade.Subject;

            _storage.Save(_grades);
            GradeSubject.NotifyObservers();
            return oldGrade;
        }

        public Grade? RemoveGrade(int id)
        {
            Grade? grade = GetById(id);
            if (grade == null) return null;

            _grades.Remove(grade);
            _storage.Save(_grades);
            GradeSubject.NotifyObservers();
            return grade;
        }

        public Grade? GetById(int id)
        {
            return _grades.Find(g => g.Id == id);
        }

        public List<Grade> GetAll()
        {
            return _grades;
        }
    }
}
