using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CRUDExercises.EF.Controllers;

namespace CRUDExercises.EF.Views;


internal static class UserInterface
{
	private static ClientController _clientController => new ClientController();
	private static LocationController _locationController => new LocationController();


	public static void DisplayWelcomeMessage()
	{
		Console.Write(
			"\nFaites un choix parmi les différentes fonctionnalités (touche entrée pour valider) :\n\n" +

			"0 - Ajouter un client en rentrant ses informations.\n" +
			"1 - Afficher la liste des clients.\n" +
			"2 - Afficher un client spécifique.\n" +
			"3 - Mettre à jour un client.\n" +
			"4 - Supprimer un client.\n\n" +

			"5 - Ajouter une location en rentrant ses informations.\n" +
			"6 - Afficher la liste des locations.\n" +
			"7 - Afficher une location spécifique.\n" +
			"8 - Mettre à jour une location.\n" +
			"9 - Supprimer une location.\n\n" +

			"Q - Quitter le programme.\n\n" +

			"Votre choix : "
		);
	}


	public static async Task DisplayAddClient()
	{
		Console.WriteLine("Interface d'ajout d'un client (prénom, nom, etc...).");
		Console.WriteLine("Faites directement entrer pour ignorer un champ optionnel.\n");


		Console.Write("Veuillez rentrer le nom (obligatoire) : ");
		string lastName = Console.ReadLine() ?? "";


		Console.Write("Veuillez rentrer le prénom (obligatoire) : ");
		string firstName = Console.ReadLine() ?? "";

		if (string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(firstName))
		{
			Console.WriteLine("\nFormat de nom / prénom non valide.");
			Console.WriteLine("Echec de création d'un client.");
			return;
		}


		Console.Write("Veuillez rentrer la date de naissance (JJ/MM/AAAA) (obligatoire) : ");
		DateTime birthDate;

		if (!DateTime.TryParse(Console.ReadLine(), new CultureInfo("fr-FR"), out birthDate))
		{
			Console.WriteLine("\nFormat de date non valide.");
			Console.WriteLine("Echec de création d'un client.");
			return;
		}


		Console.Write("Veuillez rentrer l'adresse (pas de code postal ou ville) (optionnel) : ");
		string? address = Console.ReadLine();


		Console.Write("Veuillez rentrer le code postal (optionnel) : ");
		string? postalCode = Console.ReadLine();


		Console.Write("Veuillez rentrer la ville (optionnel) : ");
		string? city = Console.ReadLine();


		Console.WriteLine("\nCreation d'un client en cours...");
		string? creationMessage = await _clientController.AddClient
		(
			firstName,
			lastName,
			birthDate,
			address,
			postalCode,
			city
		);

		Console.WriteLine(creationMessage);
	}


	public static async Task DisplayClientList()
	{
		Console.WriteLine("Liste des clients :\n");

		List<string> clients = await _clientController.GetClientList();

		if (clients.Count == 0)
		{
			Console.WriteLine("Vous n'avez pas encore saisi de clients.\n");
			return;
		}

		clients.ForEach(Console.WriteLine);
	}


	public static async Task DisplayClientById()
	{
		Console.WriteLine("Affichage d'un client par identifiant...\n");

		Console.Write("Veuillez rentrer l'identifiant du client : ");
		int id;
		if (!int.TryParse(Console.ReadLine(), out id))
		{
			Console.WriteLine("Format d'identifiant non valide.");
			return;
		}

		string? searchMessage = await _clientController.GetClientById(id);
		Console.WriteLine(searchMessage);
	}


	public static async Task DisplayUpdateClient()
	{
		Console.WriteLine("Interface de mise à jour d'un client par identifiant...");
		Console.WriteLine("Faites directement entrer pour ignorer un champ.");
		Console.WriteLine("Espace puis entrer pour écraser un champ nullable à null.\n");


        Console.Write("Veuillez rentrer l'identifiant du client à modifier (obligatoire) : ");
		int id;

		if (!int.TryParse(Console.ReadLine(), out id))
		{
			Console.WriteLine("\nFormat d'identifiant non valide.");
			Console.WriteLine("Echec de modification d'un client.");
			return;
		}


        Console.Write("Veuillez rentrer le nom : ");
		string lastName = Console.ReadLine() ?? "";


		Console.Write("Veuillez rentrer le prénom : ");
		string firstName = Console.ReadLine() ?? "";


		Console.Write("Veuillez rentrer la date de naissance (JJ/MM/AAAA) : ");
		DateTime birthDate = new();
		string birthDateString = Console.ReadLine() ?? "";

		if (!string.IsNullOrEmpty(birthDateString) && 
			!DateTime.TryParse(birthDateString, new CultureInfo("fr-FR"), out birthDate))
		{
			Console.WriteLine("\nFormat de date non valide.");
			Console.WriteLine("Echec de modification d'un client.");
			return;
		}


		Console.Write("Veuillez rentrer l'adresse (pas de code postal ou ville) (nullable) : ");
		string? address = Console.ReadLine();


		Console.Write("Veuillez rentrer le code postal (nullable) : ");
		string? postalCode = Console.ReadLine();


		Console.Write("Veuillez rentrer la ville (nullable) : ");
		string? city = Console.ReadLine();


		Console.WriteLine("\nMise à jour d'un client en cours...");
		string? creationMessage = await _clientController.UpdateClient
		(
			id,
			firstName,
			lastName,
			birthDate,
			address,
			postalCode,
			city
		);

		Console.WriteLine(creationMessage);
	}


	public static async Task DisplayDeleteClient()
	{
		Console.WriteLine("Interface de suppression d'un client par identifiant...\n");

		Console.Write("Veuillez rentrer l'identifiant du client : ");
		int id;
		if (!int.TryParse(Console.ReadLine(), out id))
		{
			Console.WriteLine("Format d'identifiant non valide.");
			return;
		}

		string? deleteMessage = await _clientController.DeleteClient(id);
		Console.WriteLine(deleteMessage);
	}

	// -----------------------------------------------
	// -------------------LOCATION--------------------
	// -----------------------------------------------

	public static async Task DisplayAddLocation()
	{
		Console.WriteLine("Interface d'ajout d'une location (client, véhicule...).");
		Console.WriteLine("Faites directement entrer pour ignorer un champ optionnel.\n");


		Console.Write("Veuillez rentrer l'identifiant du client (optionnel) : ");
		string idClientString = Console.ReadLine() ?? "";
		int idClient = 0;

		if(!(idClientString == "") && !int.TryParse(idClientString, out idClient))
		{
			Console.WriteLine("\nFormat d'identifiant non valide.");
			Console.WriteLine("Echec de création d'une location.");
			return;
		}

		
		Console.Write("Veuillez rentrer l'identifiant du véhicule (optionnel) : ");
		string idVehiculeString = Console.ReadLine() ?? "";
		int idVehicule = 0;

		if(!(idVehiculeString == "") && !int.TryParse(idVehiculeString, out idVehicule))
		{
			Console.WriteLine("\nFormat d'identifiant non valide.");
			Console.WriteLine("Echec de création d'une location.");
			return;
		}


		Console.Write("Veuillez rentrer le nombre de kilomètres (obligatoire) : ");
		string kilometresString = Console.ReadLine() ?? "";
		int nbKm = 0;

		if((kilometresString == "") || !int.TryParse(kilometresString, out nbKm))
		{
			Console.WriteLine("\nFormat d'identifiant non valide.");
			Console.WriteLine("Echec de création d'une location.");
			return;
		}


		Console.Write("Veuillez rentrer la date de début de location (JJ/MM/AAAA) (obligatoire) : ");
		string dateDebutString = Console.ReadLine() ?? "";
		DateTime dateDebut;

		if ((dateDebutString == "") || !DateTime.TryParse(dateDebutString, new CultureInfo("fr-FR"), out dateDebut))
		{
			Console.WriteLine("\nFormat de date non valide.");
			Console.WriteLine("Echec de création d'une location.");
			return;
		}


		Console.Write("Veuillez rentrer la date de fin de location (JJ/MM/AAAA) (optionnel) : ");
		string dateFinString = Console.ReadLine() ?? "";
		DateTime dateFin = default;

		if (!(dateFinString == "") && !DateTime.TryParse(dateFinString, new CultureInfo("fr-FR"), out dateFin))
		{
			Console.WriteLine("\nFormat de date non valide.");
			Console.WriteLine("Echec de création d'une location.");
			return;
		}


		Console.WriteLine("\nCreation d'une location en cours...");
		string? creationMessage = await _locationController.AddLocation
		(
			idClient,
			idVehicule,
			nbKm,
			dateDebut,
			dateFin
		);

		Console.WriteLine(creationMessage);
	}


	public static async Task DisplayLocationList()
	{
		Console.WriteLine("Liste des locations :\n");

		List<string> locations = await _locationController.GetLocationList();

		if (locations.Count == 0)
		{
			Console.WriteLine("Vous n'avez pas encore saisi de locations.\n");
			return;
		}

		locations.ForEach(Console.WriteLine);
	}


	public static async Task DisplayLocationById()
	{
		Console.WriteLine("Affichage d'une location par identifiant...\n");

		Console.Write("Veuillez rentrer l'identifiant de la location : ");
		int id;
		if (!int.TryParse(Console.ReadLine(), out id))
		{
			Console.WriteLine("Format d'identifiant non valide.");
			return;
		}

		string? searchMessage = await _locationController.GetLocationById(id);
		Console.WriteLine(searchMessage);
	}


	public static async Task UpdateLocation()
	{
		Console.WriteLine("Interface d'ajout d'une location (client, véhicule...).");
		Console.WriteLine("Faites directement entrer pour ignorer un champ.");
		Console.WriteLine("Espace puis entrer pour écraser un champ nullable à null.\n");


		Console.Write("Veuillez rentrer l'identifiant de la location à modifier (obligatoire) : ");
		int id;

		if (!int.TryParse(Console.ReadLine(), out id))
		{
			Console.WriteLine("\nFormat d'identifiant non valide.");
			Console.WriteLine("Echec de modification d'une location.");
			return;
		}


		Console.Write("Veuillez rentrer l'identifiant du client (nullable) : ");
		string idClientString = Console.ReadLine() ?? "";
		int idClient = 0;

		if (idClientString == " ")
			idClient = -1;
		else if (!(idClientString == "") && !int.TryParse(idClientString, out idClient))
		{
			Console.WriteLine("\nFormat d'identifiant non valide.");
			Console.WriteLine("Echec de modification d'une location.");
			return;
		}

		
		Console.Write("Veuillez rentrer l'identifiant du véhicule (nullable) : ");
		string idVehiculeString = Console.ReadLine() ?? "";
		int idVehicule = 0;

		if (idVehiculeString == " ")
			idVehicule = -1;
		else if(!(idVehiculeString == "") && !int.TryParse(idVehiculeString, out idVehicule))
		{
			Console.WriteLine("\nFormat d'identifiant non valide.");
			Console.WriteLine("Echec de modification d'une location.");
			return;
		}


		Console.Write("Veuillez rentrer le nombre de kilomètres : ");
		string kilometresString = Console.ReadLine() ?? "";
		int nbKm = 0;

		if(!(kilometresString == "") || !int.TryParse(kilometresString, out nbKm))
		{
			Console.WriteLine("\nFormat d'identifiant non valide.");
			Console.WriteLine("Echec de modification d'une location.");
			return;
		}


		Console.Write("Veuillez rentrer la date de début de location (JJ/MM/AAAA) : ");
		string dateDebutString = Console.ReadLine() ?? "";
		DateTime dateDebut = default;

		if (!string.IsNullOrEmpty(dateDebutString) && 
			!DateTime.TryParse(Console.ReadLine(), new CultureInfo("fr-FR"), out dateDebut))
		{
			Console.WriteLine("\nFormat de date non valide.");
			Console.WriteLine("Echec de modification d'une location.");
			return;
		}


		Console.Write("Veuillez rentrer la date de fin de location (JJ/MM/AAAA) (nullable) : ");
		string dateFinString = Console.ReadLine() ?? "";
		DateTime dateFin = default;

		if (dateFinString == " ")
			dateFin = DateTime.MaxValue;
		else if (!string.IsNullOrEmpty(dateFinString) &&
			!DateTime.TryParse(dateFinString, new CultureInfo("fr-FR"), out dateFin))
		{
			Console.WriteLine("\nFormat de date non valide.");
			Console.WriteLine("Echec de modification d'une location.");
			return;
		}


		Console.WriteLine("\nModification d'une location en cours...");
		string? creationMessage = await _locationController.UpdateLocation
		(
			id,
			idClient,
			idVehicule,
			nbKm,
			dateDebut,
			dateFin
		);

		Console.WriteLine(creationMessage);
	}


	public static async Task DeleteLocation()
	{
		Console.WriteLine("Interface de suppression d'une location par identifiant...\n");

		Console.Write("Veuillez rentrer l'identifiant de la location : ");
		int id;
		if (!int.TryParse(Console.ReadLine(), out id))
		{
			Console.WriteLine("Format d'identifiant non valide.");
			return;
		}

		string? deleteMessage = await _locationController.DeleteLocation(id);
		Console.WriteLine(deleteMessage);
	}
}
