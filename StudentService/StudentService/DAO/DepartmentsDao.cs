using StudentService.Model;
using StudentService.Serialization;
using System.Collections.Generic;
using StudentService.Observer;

namespace StudentService.DAO
{
    public class DepartmentDao
    {
        private readonly List<Department> _departments;
        private readonly Storage<Department> _storage;

        public DAOSubject DepartmentSubject;

        public DepartmentDao()
        {
            _storage = new Storage<Department>("departments.txt");
            _departments = _storage.Load();
            DepartmentSubject = new DAOSubject();
        }

        private int GenerateId()
        {
            if (_departments.Count == 0) return 1;
            return _departments[^1].Id + 1;
        }

        public Department Create(Department department)
        {
            department.Id = GenerateId();
            _departments.Add(department);
            _storage.Save(_departments);
            DepartmentSubject.NotifyObservers();
            return department;
        }

        public Department? UpdateDepartment(Department department)
        {
            Department? oldDepartment = GetById(department.Id);
            if (oldDepartment == null) return null;

            oldDepartment.Code = department.Code;
            oldDepartment.Name = department.Name;
            oldDepartment.HeadProfessor = department.HeadProfessor;
            oldDepartment.Professors = department.Professors;

            _storage.Save(_departments);
            DepartmentSubject.NotifyObservers();
            return oldDepartment;
        }

        public Department? RemoveDepartment(int id)
        {
            Department? department = GetById(id);
            if (department == null) return null;

            _departments.Remove(department);
            _storage.Save(_departments);
            DepartmentSubject.NotifyObservers();
            return department;
        }

        private Department? GetById(int id)
        {
            return _departments.Find(d => d.Id == id);
        }

        public List<Department> GetAll()
        {
            return _departments;
        }
    }
}
