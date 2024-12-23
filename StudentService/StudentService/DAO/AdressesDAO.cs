using StudentService.Model;
using StudentService.Observer;
using StudentService.Serialization;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;

namespace StudentService.DAO
{
    public class AddressDao
    {
        private readonly List<Adress> _addresses;
        private readonly Storage<Adress> _storage;

        public DAOSubject AddressSubject;

        public AddressDao()
        {
            _storage = new Storage<Adress>("addresses.txt");
            _addresses = _storage.Load();
            AddressSubject = new DAOSubject();
        }

        private int GenerateId()
        {
            if (_addresses.Count == 0) return 1;
            return _addresses[^1].Id + 1;
        }

        public Adress Create(Adress address)
        {
            address.Id = GenerateId();
            _addresses.Add(address);
            _storage.Save(_addresses);
            AddressSubject.NotifyObservers();
            return address;
        }

        public Adress? UpdateAddress(Adress address)
        {
            Adress? oldAddress = GetById(address.Id);
            if (oldAddress == null) return null;

            oldAddress.Street = address.Street;
            oldAddress.Number = address.Number;
            oldAddress.Town = address.Town;
            oldAddress.Country = address.Country;

            _storage.Save(_addresses);
            AddressSubject.NotifyObservers();
            return oldAddress;
        }

        public Adress? RemoveAddress(int id)
        {
            Adress? address = GetById(id);
            if (address == null) return null;

            _addresses.Remove(address);
            _storage.Save(_addresses);
            AddressSubject.NotifyObservers();
            return address;
        }

        private Adress? GetById(int id)
        {
            return _addresses.Find(a => a.Id == id);
        }

        public List<Adress> GetAll()
        {
            return _addresses;
        }
    }
}
