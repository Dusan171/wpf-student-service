using StudentService.Model;
using StudentService.Observer;
using StudentService.Serialization;
using System.Collections.Generic;

namespace StudentService.DAO
{
    public class AdressDao
    {
        private readonly List<Adress> _adresses;
        private readonly Storage<Adress> _storage;

        public DAOSubject AdressStudent;
        public AdressDao()
        {
            _storage = new Storage<Adress>("adresses.txt");
            _adresses = _storage.Load();
            AdressStudent = new DAOSubject();
        }

        private int GenerateId()
        {
            if (_adresses.Count == 0) return 1;
            return _adresses[^1].Id + 1;
        }

        public Adress Create(Adress adress)
        {
            adress.Id = GenerateId();
            _adresses.Add(adress);
            _storage.Save(_adresses);
            AdressStudent.NotifyObservers();
            return adress;
        }

        public Adress? UpdateAdress(Adress adress)
        {
            Adress? oldAdress = GetById(adress.Id);
            if (oldAdress is null) return null;

            oldAdress.Street = adress.Street;
            oldAdress.Number = adress.Number;
            oldAdress.Town = adress.Town;
            oldAdress.Country = adress.Country;

            _storage.Save(_adresses);
             AdressStudent.NotifyObservers();
            return oldAdress;
        }

        public Adress? RemoveAdress(int id)
        {
            Adress? adress = GetById(id);
            if (adress == null) return null;

            _adresses.Remove(adress);
            _storage.Save(_adresses);
            AdressStudent.NotifyObservers();
            return adress;
        }

        private Adress? GetById(int id)
        {
            return _adresses.Find(v => v.Id == id);
        }

        public List<Adress> GetAll()
        {
            return _adresses;
        } 
    }
}
