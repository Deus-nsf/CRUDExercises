
using static CRUDExercises.EF.Views.UserInterface;


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
		//	DisplayAddLocation();
		//	break;
		//case "6":
		//	DisplayLocationList();
		//	break;
		//case "7":
		//	DisplayLocationById();
		//	break;
		//case "8":
		//	UpdateLocation();
		//	break;
		//case "9":
		//	DeleteLocation();
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