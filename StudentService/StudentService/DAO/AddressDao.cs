using StudentService.Model;
using StudentService.Serialization;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StudentService.DAO
{
    public class AddressDao
    {
        private readonly List<Adress> _adresses;
        private readonly Storage<Adress> _storage;


        public AddressDao()
        {
            _storage = new Storage<Adress>("adresses.txt");
            _adresses = _storage.Load();
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
            return adress;
        }

        public Adress? UpdateVehicle(Adress adress)
        {
            Adress? oldAdress = GetById(adress.Id);
            if (oldAdress is null) return null;

            oldAdress.Street = adress.Street;
            oldAdress.Number = adress.Number;
            oldAdress.Town = adress.Town;
            oldAdress.Country = adress.Country;



            _storage.Save(_adresses);
            return oldAdress;
        }

        public Adress? RemoveVehicle(int id)
        {
            Adress? adress = GetById(id);
            if (adress == null) return null;

            _adresses.Remove(adress);
            _storage.Save(_adresses);
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
