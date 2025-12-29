# 🎓 Student Service – WPF Application

## Opis projekta
Student Service je desktop aplikacija razvijena u C# i WPF-u (.NET 8) koja omogućava osnovno upravljanje studentskim podacima. Aplikacija koristi lokalno skladištenje podataka u CSV fajlovima i namenjena je radu u jednom korisničkom okruženju.

## Funkcionalnosti
- Upravljanje studentima (kreiranje, pregled, izmena, brisanje)
- Upravljanje ocenama
- Upravljanje indeksima
- Upravljanje departmanima (katedrama)
- Upravljanje adresama
- Pregled i osnovna obrada profesora i predmeta
- Automatsko osvežavanje prikaza korišćenjem observer mehanizma

## Tehnologije
- C#
- .NET 8 (net8.0-windows)
- WPF (XAML + code-behind)
- CSV fajlovi za čuvanje podataka
- FontAwesome.WPF (NuGet)

## Arhitektura
- Model – poslovni entiteti sa CSV serializacijom
- DAO sloj – CRUD operacije i rad sa fajlovima
- Observer – obaveštavanje UI-a o promenama
- GUI – WPF prozori za prikaz i unos podataka

*Primenjeni obrasci: DAO, Observer, jednostavan serializer.*  
*UI koristi „MVVM-like“ pristup (bez posebnih ViewModel klasa).*

## Pokretanje aplikacije
1. Otvoriti projekat u Visual Studio 2022+
2. Pokrenuti kao WPF aplikaciju (net8.0-windows)
3. Aplikacija automatski kreira potrebne CSV fajlove u `Data` folderu
4. Nije potrebna baza podataka niti dodatna konfiguracija

## Ograničenja
- Podaci se čuvaju u CSV fajlovima (bez baze podataka)
- Namenjeno single-user desktop korišćenju
- Potencijalni problemi sa relativnom putanjom do `Data` foldera
- Manje nekonzistentnosti u dijalozima i imenovanju (npr. *Adress / Address*)

## 👥 Članovi tima
- Bojana Milošević
- Dušan Lazić
