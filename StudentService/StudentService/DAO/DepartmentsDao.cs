using StudentService.Model;
using StudentService.Observer;
using StudentService.Serialization;
using System.Collections.Generic;

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
            // if (_departments.Count == 0) return 1;
            // return _departments[^1].Id + 1;
            // Ako je lista prazna, vraća ID kao 1, inače koristi UUID generaciju.
            return _departments.Count == 0 ? 1 : Guid.NewGuid().GetHashCode();
        }
        private List<Department> LoadDepartments()
        {
            try
            {
                return _storage.Load();
            }
            catch (Exception ex)
            {
                // Logovanje greške
                Console.WriteLine($"Error loading departments: {ex.Message}");
                return new List<Department>(); // Ako dođe do greške, vraća praznu listu.
            }
        }

        public Department Create(Department department)
        /*{
            department.Id = GenerateId();
            _departments.Add(department);
            _storage.Save(_departments);
            DepartmentSubject.NotifyObservers();
            return department;
        }*/
        {
            try
            {
                department.Id = GenerateId();
                _departments.Add(department);
                _storage.Save(_departments); // Spremanje novih podataka u fajl
                DepartmentSubject.NotifyObservers(); // Obaveštavanje observera
                return department;
            }
            catch (Exception ex)
            {
                // Obrada greške prilikom kreiranja departmana
                Console.WriteLine($"Error creating department: {ex.Message}");
                return null;
            }
        }

        public Department? UpdateDepartment(Department department)
        /*{
            Department? oldDepartment = GetById(department.Id);
            if (oldDepartment == null) return null;

            oldDepartment.Code = department.Code;
            oldDepartment.Name = department.Name;
            oldDepartment.HeadProfessor = department.HeadProfessor;
            oldDepartment.Professors = department.Professors;

            _storage.Save(_departments);
            DepartmentSubject.NotifyObservers();
            return oldDepartment;
        }*/
        {
            try
            {
                var oldDepartment = GetById(department.Id);
                if (oldDepartment == null) return null;

                oldDepartment.Code = department.Code;
                oldDepartment.Name = department.Name;
                oldDepartment.HeadProfessor = department.HeadProfessor; // Ažuriranje profesora
                oldDepartment.Professors = department.Professors;

                _storage.Save(_departments); // Spremanje izmenjenih podataka
                DepartmentSubject.NotifyObservers(); // Obaveštavanje observera
                return oldDepartment;
            }
            catch (Exception ex)
            {
                // Obrada greške prilikom ažuriranja departmana
                Console.WriteLine($"Error updating department: {ex.Message}");
                return null;
            }
        }

        public Department? RemoveDepartment(int id)
        /*{
            Department? department = GetById(id);
            if (department == null) return null;

            _departments.Remove(department);
            _storage.Save(_departments);
            DepartmentSubject.NotifyObservers();
            return department;
        }*/
        {
            try
            {
                var department = GetById(id);
                if (department == null) return null;

                _departments.Remove(department);
                _storage.Save(_departments); // Spremanje promena
                DepartmentSubject.NotifyObservers(); // Obaveštavanje observera
                return department;
            }
            catch (Exception ex)
            {
                // Obrada greške prilikom brisanja departmana
                Console.WriteLine($"Error removing department: {ex.Message}");
                return null;
            }
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
