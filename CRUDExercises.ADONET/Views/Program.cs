
using static CRUDExercises.EF.Views.UserInterface;


Console.WriteLine("\nBienvenue dans la gestion de location de véhicules.\n");

bool exit = false;
while (exit == false)
{
	DisplayWelcomeMessage();
	string? choice = Console.ReadLine();
	Console.WriteLine();

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
			await DisplayUpdateClient();
			break;
		case "4":
			await DisplayDeleteClient();
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
			Console.WriteLine("Sortie du programme...");
			exit = true;
			break;
		default:
			Console.WriteLine("Votre choix ne correspond pas à une des possibilités, veuillez recommencer...");
			break;
	}
}