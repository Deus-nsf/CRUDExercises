
using static CRUDExercises.ADONET.Views.UserInterface;


Console.WriteLine("\nBienvenue dans la gestion de location de véhicules.\n");

bool exit = false;
while (exit == false)
{
	DisplayWelcomeMessage();
	string? choice = Console.ReadLine();

	switch (choice)
	{
		case "0":
			await DisplayAddClient();
			break;
		case "1":
			await DisplayClientList();
			break;
		case "2":
			await DisplayClientById();
			break;
		case "3":
			await UpdateClient();
			break;
		case "4":
			await DeleteClient();
			break;

		//case "5":
		//	DisplayAddVehicle();
		//	break;
		//case "6":
		//	DisplayVehicleList();
		//	break;
		//case "7":
		//	DisplayVehicleById();
		//	break;
		//case "8":
		//	UpdateVehicle();
		//	break;
		//case "9":
		//	DeleteVehicle();
		//	break;

		case "q":
		case "Q":
			Console.WriteLine("\n\nSortie du programme...");
			exit = true;
			break;
		default:
			Console.WriteLine("\n\nVotre choix ne correspond pas à une des possibilités, veuillez recommencer...");
			break;
	}
}