using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDExercises.ADONET.Views;


internal static class UserInterface
{
	//private static EmployeeController _employeeController => new EmployeeController();
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

			"5 - Ajouter un véhicule en rentrant ses informations.\n" +
			"6 - Afficher la liste des véhicules.\n" +
			"7 - Afficher un véhicule spécifique.\n" +
			"8 - Mettre à jour un véhicule.\n" +
			"9 - Supprimer un véhicule.\n\n" +

			"Q - Quitter le programme.\n\n" +

			"Votre choix : "
		);
	}


	public static void DisplayAddClient()
	{
		Console.WriteLine("Interface d'ajout d'un client (prénom, nom, salaire)...\n");

		Console.Write("Veuillez rentrer le prénom : ");
		string? firstName = Console.ReadLine();

		Console.Write("Veuillez rentrer le nom : ");
		string? lastName = Console.ReadLine();

		Console.Write("Veuillez rentrer le salaire : ");
		double salary;
		if(!double.TryParse(Console.ReadLine(), out salary))
		{
			Console.WriteLine("Format de salaire non valide");
			Console.WriteLine("Echec de création d'un employé.");
			return;
		}

		Console.WriteLine("\nCreation d'un employé en cours...");
		//string? creationMessage = _employeeController.AddEmployee(firstName, lastName, salary);
		//Console.WriteLine(creationMessage);
	}


	public static void DisplayClientList()
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


	public static void DisplayClientById()
	{
        Console.WriteLine("Affichage d'un employé par identifiant...\n");

		Console.Write("Veuillez rentrer l'identifiant de l'employé : ");
		int id;
		if(!int.TryParse(Console.ReadLine(), out id))
		{
			Console.WriteLine("Format d'identifiant non valide.");
			return;
		}

		//string? searchMessage = _employeeController.GetEmployeeById(id);
		//Console.WriteLine(searchMessage);
    }


	public static void UpdateClient()
	{

	}


	public static void DeleteClient()
	{

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
		if(!double.TryParse(Console.ReadLine(), out salary))
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
		if(!int.TryParse(Console.ReadLine(), out id))
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
