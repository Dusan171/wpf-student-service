using StudentService.Model;
using StudentService.Observer;
using StudentService.Serialization;
using System.Collections.Generic;

namespace StudentService.DAO
{
    public class IndexDao
    {
        private readonly List<StudentIndex> _indexes;
        private readonly Storage<StudentIndex> _storage;

        public DAOSubject IndexStudent;

        public IndexDao()
        {
            _storage = new Storage<StudentIndex>("indexes.txt");
            _indexes = _storage.Load();
            IndexStudent = new DAOSubject();
        }

        private int GenerateId()
        {
            if (_indexes.Count == 0) return 1;
            return _indexes[^1].Id + 1;
        }

        public StudentIndex Create(StudentIndex index)
        {
            index.Id = GenerateId();
            _indexes.Add(index);
            _storage.Save(_indexes);
            IndexStudent.NotifyObservers();
            return index;
        }

        public StudentIndex? UpdateIndex(StudentIndex index)
        {
            StudentIndex? oldIndex = GetById(index.Id);
            if (oldIndex == null) return null;

            oldIndex.CourseCode = index.CourseCode;
            oldIndex.RegisterNumber = index.RegisterNumber;
            oldIndex.RegisterYear = index.RegisterYear;

            _storage.Save(_indexes);
            IndexStudent.NotifyObservers();
            return oldIndex;
        }

        public StudentIndex? RemoveIndex(int id)
        {
            StudentIndex? index = GetById(id);
            if (index == null) return null;

            _indexes.Remove(index);
            _storage.Save(_indexes);
            IndexStudent.NotifyObservers();
            return index;
        }

        private StudentIndex? GetById(int id)
        {
            return _indexes.Find(i => i.Id == id);
        }

        public List<StudentIndex> GetAll()
        {
            return _indexes;
        }
    }
}