using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CRUDExercises.EF.Repositories;
//using CRUDExercises.EF.LegacyEntities;
using CRUDExercises.EF.Entities;

namespace CRUDExercises.EF.Controllers;


internal class ClientController
{
	private readonly ClientRepository _clientRepository = new();


	/// <summary>
	/// INSERT Object INTO Class
	/// </summary>
	public async Task<string> AddClient(string firstName,
										string lastName,
										DateTime birthDate,
										string? address = "",
										string? postalCode = "",
										string? city = "")
	{
		Client client = new(id: 0, firstName, lastName, birthDate, address, postalCode, city);
		NullablePropertiesCheck(client, Mode.CREATE);

		try
		{
			await _clientRepository.AddClient(client);
		}
		catch (Exception ex)
		{
			return "Une erreur d'insertion en base est survenue :\n" + ex.Message;
		}

		return $"Le client {lastName} {firstName} à été ajouté en base.";
	}


	/// <summary>
	/// SELECT * FROM Class
	/// </summary>
	public async Task<List<string>> GetClientList()
	{
		List<Client> clients;
		List<string> formattedClients = new();

		try
		{
			clients = await _clientRepository.GetClientList();
		}
		catch (Exception ex)
		{
			return new List<string>() { "Une erreur de récupération en base est survenue :\n" + ex.Message };
		}

		clients.ForEach(client => formattedClients.Add(FormatClient(client)));
		return formattedClients;
	}


	/// <summary>
	/// SELECT * FROM Class WHERE Id = id
	/// </summary>
	public async Task<string> GetClientById(int id)
	{
		Client? client;

		try
		{
			client = await _clientRepository.GetClientById(id);
			if (client == null)
				return $"Aucun client ne correspond à l'identifiant {id} en base";
			else
				return FormatClient(client);
		}
		catch (Exception ex)
		{
			return "Une erreur de récupération en base est survenue :\n" + ex.Message;
		}
	}


	/// <summary>
	/// UPDATE Class SET (params) WHERE Id = id
	/// </summary>
	public async Task<string> UpdateClient(int id,
											string firstName = "",
											string lastName = "",
											DateTime birthDate = default,
											string? address = "",
											string? postalCode = "",
											string? city = "")
	{
		Client client = new(id, firstName, lastName, birthDate, address, postalCode, city);
		NullablePropertiesCheck(client, Mode.UPDATE);

		Dictionary<string, dynamic?> parameters = RetrieveUpdateParameters(client);
		//if (firstName != "") parameters.Add("Nom", firstName);
		//if (lastName != "") parameters.Add("Prenom", lastName);
		//if (birthDate != default) parameters.Add("Date_Naissance", birthDate);
		//if (address != null) parameters.Add("Adresse", address);
		//if (postalCode != null) parameters.Add("Code_Postal", postalCode);
		//if (city != null) parameters.Add("Ville", city);

		try
		{
			if (parameters.Count == 6)
				await _clientRepository.UpdateClient(id, client);
			else
				await _clientRepository.UpdateClientSpecificUnsafe(id, parameters);			
		}
		catch (Exception ex)
		{
			return "Une erreur de mise à jour en base est survenue :\n" + ex.Message;
		}

		return $"Le client numéro {id} à été mis à jour.";
	}


	/// <summary>
	/// DELETE FROM Class WHERE Id = id 
	/// </summary>
	public async Task<string> DeleteClient(int id)
	{
		try
		{
			await _clientRepository.DeleteClient(id);
		}
		catch (Exception ex)
		{

			return "Une erreur de suppression en base est survenue :\n" + ex.Message;
		}

		return $"Le client numéro {id} à été supprimé.";
	}


	/// <summary>
	/// Returns a string representation of the current object's properties
	/// </summary>
	public string FormatClient(Client client)
	{
		return
			$"Identifiant : {client.Id}\n" +
			$"Nom : {client.Nom}\n" +
			$"Prénom : {client.Prenom}\n" +
			$"Date de naissance : {client.Date_Naissance.ToShortTimeString()}\n" +
			$"Adresse : {client.Adresse}\n" +
			$"Code Postal : {client.Code_Postal}\n" +
			$"Ville : {client.Ville}\n";
	}


	/// <summary>
	/// Switches nullable properties to null with the corresponding modeset.
	/// Could be done via reflection, but for the sake of the exercise...
	/// </summary>
	/// <param name="client"></param>
	private void NullablePropertiesCheck(Client client, Mode mode)
	{
		if (mode == Mode.CREATE)
		{
			client.Adresse = client.Adresse == "" ? null : client.Adresse;
			client.Code_Postal = client.Code_Postal == "" ? null : client.Code_Postal;
			client.Ville = client.Ville == "" ? null : client.Ville;
		}
		else if (mode == Mode.UPDATE)
		{
			client.Adresse = client.Adresse == " " ? null : client.Adresse;
			client.Code_Postal = client.Code_Postal == " " ? null : client.Code_Postal;
			client.Ville = client.Ville == " " ? null : client.Ville;
		}
	}


	/// <summary>
	/// Gets an object, tests all property types and values,
	/// then returns a dictionnary of the necessary parameters for the update.
	/// Could be done via reflection, but for the sake of the exercise...
	/// </summary>
	/// <param name="client"></param>
	/// <returns></returns>
	private Dictionary<string, dynamic?> RetrieveUpdateParameters(Client client)
	{
		Dictionary<string, dynamic?> inputParams = new()
		{
			{"Nom", client.Nom},
			{"Prenom", client.Prenom},
			{"Date_Naissance", client.Date_Naissance},
			{"Adresse", client.Adresse},
			{"Code_Postal", client.Code_Postal},
			{"Ville", client.Ville}
		};

		Dictionary<string, dynamic?> outputParams = new();

		for (int i = 0; i < inputParams.Count; i++)
		{
			dynamic? value = inputParams.ElementAt(i).Value;

			if (value is null)
				outputParams.Add(inputParams.ElementAt(i).Key, null);
			else if ((value is string && value != "") || (value is DateTime && value != default(DateTime)))
				outputParams.Add(inputParams.ElementAt(i).Key, value);
		}

		return outputParams;
	}
}


enum Mode
{
	CREATE = 1,
	UPDATE = 2
};