using StudentService.Model;
using StudentService.Observer;
using StudentService.Serialization;
using System.Collections.Generic;
using System;

namespace StudentService.DAO
{
    public class SubjectDao
    {
        private readonly List<Subject> _subjects;
        private readonly Storage<Subject> _storage;

        public DAOSubject SubjectSubject;

        public SubjectDao()
        {
            _storage = new Storage<Subject>("subjects.txt");
            try
            {
                _subjects = _storage.Load();
            }
            catch (Exception ex)
            {
                // Handle loading failure (file missing, invalid format, etc.)
                Console.WriteLine($"Error loading subjects: {ex.Message}");
                _subjects = new List<Subject>(); // Initialize empty list in case of failure
            }

            SubjectSubject = new DAOSubject();
        }

        private int GenerateId()
        {
            if (_subjects.Count == 0) return 1;
            return _subjects[^1].Id + 1;
        }

        public Subject Create(Subject subject)
        {
            subject.Id = GenerateId();
            _subjects.Add(subject);
            try
            {
                _storage.Save(_subjects);
            }
            catch (Exception ex)
            {
                // Handle saving failure
                Console.WriteLine($"Error saving subject: {ex.Message}");
                return null; // Return null to indicate failure
            }
            SubjectSubject.NotifyObservers();
            return subject;
        }

        public Subject? UpdateSubject(Subject subject)
        {
            Subject? oldSubject = GetById(subject.Id);
            if (oldSubject == null) return null;

            oldSubject.Code = subject.Code;
            oldSubject.Name = subject.Name;
            oldSubject.Semester = subject.Semester;
            oldSubject.YearOfStudy = subject.YearOfStudy;
            oldSubject.Professor = subject.Professor;
            oldSubject.Espb = subject.Espb;

            try
            {
                _storage.Save(_subjects);
            }
            catch (Exception ex)
            {
                // Handle saving failure
                Console.WriteLine($"Error updating subject: {ex.Message}");
                return null;
            }

            SubjectSubject.NotifyObservers();
            return oldSubject;
        }

        public Subject? RemoveSubject(int id)
        {
            Subject? subject = GetById(id);
            if (subject == null) return null;

            _subjects.Remove(subject);

            try
            {
                _storage.Save(_subjects);
            }
            catch (Exception ex)
            {
                // Handle saving failure
                Console.WriteLine($"Error removing subject: {ex.Message}");
                return null;
            }

            SubjectSubject.NotifyObservers();
            return subject;
        }

        private Subject? GetById(int id)
        {
            return _subjects.Find(s => s.Id == id);
        }

        public List<Subject> GetAll()
        {
            return _subjects;
        }
    }
}
