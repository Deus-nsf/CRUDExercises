﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CRUDExercises.ADONET.Entities;
using CRUDExercises.ADONET.Repositories;

namespace CRUDExercises.ADONET.Controllers;


internal class ClientController
{
	private readonly ClientRepository _clientRepository = new();


	public async Task<string> AddClient(string firstName,
										string lastName,
										DateTime birthDate,
										string? address = "",
										string? postalCode = "",
										string? city = "")
	{
		address = address == "" ? null : address;
		postalCode = postalCode == "" ? null : postalCode;
		city = city == "" ? null : city;

		Client client = new(id: -1, firstName, lastName, birthDate, address, postalCode, city);

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


	public async Task<string> GetClientById(int id)
	{
		Client client;

		try
		{
			client = await _clientRepository.GetClientById(id);
			return FormatClient(client);
		}
		catch (Exception ex)
		{
			return "Une erreur de récupération en base est survenue :\n" + ex.Message;
		}
	}


	public async Task<string> UpdateClient(int id,
											string firstName = "",
											string lastName = "",
											DateTime birthDate = default,
											string? address = "",
											string? postalCode = "",
											string? city = "")
	{
		address = address == "" ? null : address;
		postalCode = postalCode == "" ? null : postalCode;
		city = city == "" ? null : city;

		Client client = new(id: -1, firstName, lastName, birthDate, address, postalCode, city);

		Dictionary<string, dynamic?> parameters = RetrieveUpdateParameters(client);
		//if (firstName != "") parameters.Add("Nom", firstName);
		//if (lastName != "") parameters.Add("Prenom", lastName);
		//if (birthDate != default) parameters.Add("Date_Naissance", birthDate);
		//if (address != null) parameters.Add("Adresse", address);
		//if (postalCode != null) parameters.Add("Code_Postal", postalCode);
		//if (city != null) parameters.Add("Ville", city);

		try
		{
			client = await _clientRepository.GetClientById(id);
			await _clientRepository.UpdateClient(id, parameters);	
		}
		catch (Exception ex)
		{
			return "Une erreur de mise à jour en base est survenue :\n" + ex.Message;
		}

		return $"Le client {lastName} {firstName} à été mis à jour en base.";
	}


	public async Task<bool> DeleteClient()
	{
		return true;
	}


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
	/// Gets an object, tests all property types and values,
	/// then returns a dictionnary of the necessary parameters for the update.
	/// </summary>
	/// <param name="client"></param>
	/// <returns>outputParams</returns>
	private Dictionary<string, dynamic?> RetrieveUpdateParameters(Client client)
	{


		Dictionary<string, dynamic?> inputParams = new();
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
