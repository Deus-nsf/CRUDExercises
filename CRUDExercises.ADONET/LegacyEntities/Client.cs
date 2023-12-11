using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDExercises.EF.LegacyEntities;


internal class Client
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public DateTime Date_Naissance { get; set; }
    public string? Adresse { get; set; }
    public string? Code_Postal { get; set; }
    public string? Ville { get; set; }


    public Client(int id,
                    string firstName,
                    string lastName,
                    DateTime birthDate,
                    string? address,
                    string? postalCode,
                    string? city)
    {
        Id = id;
        Nom = lastName;
        Prenom = firstName;
        Date_Naissance = birthDate;
        Adresse = address;
        Code_Postal = postalCode;
        Ville = city;
    }
}
