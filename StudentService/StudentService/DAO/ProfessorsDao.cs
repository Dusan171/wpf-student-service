using StudentService.Model;
using StudentService.Observer;
using StudentService.Serialization;
using System.Collections.Generic;

namespace StudentService.DAO
{
    public class ProfessorDao
    {
        private readonly List<Professor> _professors;
        private readonly Storage<Professor> _storage;

        public DAOSubject ProfessorSubject;

        public ProfessorDao()
        {
            _storage = new Storage<Professor>("professors.txt");
            _professors = _storage.Load();
            ProfessorSubject = new DAOSubject();
        }

        private int GenerateId()
        {
            if (_professors.Count == 0) return 1;
            return _professors[^1].Id + 1;
        }

        public Professor Create(Professor professor)
        {
            professor.Id = GenerateId();
            _professors.Add(professor);
            _storage.Save(_professors);
            ProfessorSubject.NotifyObservers();
            return professor;
        }

        public Professor? UpdateProfessor(Professor professor)
        {
            Professor? oldProfessor = GetById(professor.Id);
            if (oldProfessor == null) return null;

            oldProfessor.Surname = professor.Surname;
            oldProfessor.Name = professor.Name;
            oldProfessor.DateOfBirth = professor.DateOfBirth;
            oldProfessor.Address = professor.Address;
            oldProfessor.Phone = professor.Phone;
            oldProfessor.Email = professor.Email;
            oldProfessor.IdNumber = professor.IdNumber;
            oldProfessor.Vocation = professor.Vocation;
            oldProfessor.YearsOfService = professor.YearsOfService;
            oldProfessor.Subjects = professor.Subjects;

            _storage.Save(_professors);
            ProfessorSubject.NotifyObservers();
            return oldProfessor;
        }

        public Professor? RemoveProfessor(int id)
        {
            Professor? professor = GetById(id);
            if (professor == null) return null;

            _professors.Remove(professor);
            _storage.Save(_professors);
            ProfessorSubject.NotifyObservers();
            return professor;
        }

        private Professor? GetById(int id)
        {
            return _professors.Find(p => p.Id == id);
        }

        public List<Professor> GetAll()
        {
            return _professors;
        }
    }
}
