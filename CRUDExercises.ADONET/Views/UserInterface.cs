using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CRUDExercises.ADONET.Controllers;

namespace CRUDExercises.ADONET.Views;


internal static class UserInterface
{
	private static ClientController _clientController => new ClientController();
	//public static EmployeeController GetEmployeeController() => _employeeController;


	public static void DisplayWelcomeMessage()
	{
		Console.Write(
			"\nFaites un choix parmi les différentes fonctionnalités (touche entrée pour valider) :\n\n" +

			"0 - Ajouter un client en rentrant ses informations.\n" +
			"1 - Afficher la liste des clients.\n" +
			"2 - Afficher un client spécifique.\n" +
			"3 - Mettre à jour un client.\n" +
			"4 - Supprimer un client.\n\n" +

			//"5 - Ajouter un véhicule en rentrant ses informations.\n" +
			//"6 - Afficher la liste des véhicules.\n" +
			//"7 - Afficher un véhicule spécifique.\n" +
			//"8 - Mettre à jour un véhicule.\n" +
			//"9 - Supprimer un véhicule.\n\n" +

			"Q - Quitter le programme.\n\n" +

			"Votre choix : "
		);
	}


	public static async Task DisplayAddClient()
	{
		Console.WriteLine("Interface d'ajout d'un client (prénom, nom, etc...).\n");

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


	public static async Task UpdateClient()
	{
		Console.WriteLine("Interface de mise à jour d'un client par identifiant...\n");
		Console.WriteLine("Faites directement entrer pour ignorer un champ.\n");
		Console.WriteLine("Espace puis entrer pour écraser un champ optionnel.\n");

        Console.Write("Veuillez rentrer l'identifiant du client : ");
		int id;
		if (!int.TryParse(Console.ReadLine(), out id))
		{
			Console.WriteLine("Format d'identifiant non valide.");
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
			Console.WriteLine("Echec de création d'un client.");
			return;
		}

		Console.Write("Veuillez rentrer l'adresse (pas de code postal ou ville) (optionnel) : ");
		string? address = Console.ReadLine();

		Console.Write("Veuillez rentrer le code postal (optionnel) : ");
		string? postalCode = Console.ReadLine();

		Console.Write("Veuillez rentrer la ville (optionnel) : ");
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


	public static async Task DeleteClient()
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



	public static void DisplayAddVehicle()
	{
		Console.WriteLine("Interface d'ajout d'un employé (prénom, nom, salaire)...\n");

		Console.Write("Veuillez rentrer le prénom : ");
		string? firstName = Console.ReadLine();

		Console.Write("Veuillez rentrer le nom : ");
		string? lastName = Console.ReadLine();

		Console.Write("Veuillez rentrer le salaire : ");
		double salary;
		if (!double.TryParse(Console.ReadLine(), out salary))
		{
			Console.WriteLine("Format de salaire non valide");
			Console.WriteLine("Echec de création d'un employé.");
			return;
		}

		Console.WriteLine("\nCreation d'un employé en cours...");
		//string? creationMessage = _employeeController.AddEmployee(firstName, lastName, salary);
		//Console.WriteLine(creationMessage);
	}


	public static void DisplayVehicleList()
	{
		Console.WriteLine("Liste des employés :\n");

		//string employees = _employeeController.GetEmployees();

		//if (String.IsNullOrWhiteSpace(employees))
		//{
		//	Console.WriteLine("Vous n'avez pas encore saisi d'employés.\n");
		//	return;
		//}

		//Console.WriteLine(employees);
	}


	public static void DisplayVehicleById()
	{
		Console.WriteLine("Affichage d'un employé par identifiant...\n");

		Console.Write("Veuillez rentrer l'identifiant de l'employé : ");
		int id;
		if (!int.TryParse(Console.ReadLine(), out id))
		{
			Console.WriteLine("Format d'identifiant non valide.");
			return;
		}

		//string? searchMessage = _employeeController.GetEmployeeById(id);
		//Console.WriteLine(searchMessage);
	}


	public static void UpdateVehicle()
	{

	}


	public static void DeleteVehicle()
	{

	}
}
