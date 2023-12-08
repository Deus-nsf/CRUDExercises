
using static CRUDExercises.ADONET.Views.UserInterface;


Console.WriteLine("\nBienvenue dans la gestion de location de véhicules.\n");

bool exit = false;
while (exit == false)
{
	DisplayWelcomeMessage();
	string? choice = Console.ReadLine();

	switch (choice)
	{
		case "1":
			DisplayAddClient();
			break;
		case "2":
			DisplayClientList();
			break;
		case "3":
			DisplayClientById();
			break;
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