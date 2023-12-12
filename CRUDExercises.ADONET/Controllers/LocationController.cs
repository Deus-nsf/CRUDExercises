using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CRUDExercises.EF.Entities;
using CRUDExercises.EF.Repositories;

namespace CRUDExercises.EF.Controllers;


internal class LocationController
{
	private readonly LocationRepository _locationRepository = new();


	/// <summary>
	/// INSERT Object INTO Class
	/// </summary>
	public async Task<string> AddLocation(int? idClient,
											int? idVehicule,
											int nbKm,
											DateTime dateDebut,
											DateTime? dateFin)
	{
		Location location = new(id: 0, idClient, idVehicule, nbKm, dateDebut, dateFin);
		NullablePropertiesCheck(location, Mode.CREATE);

		try
		{
			await _locationRepository.AddLocation(location);
		}
		catch (Exception ex)
		{
			return "Une erreur d'insertion en base est survenue :\n" + ex.Message;
		}

		return $"La location du {dateDebut.ToShortDateString()} à été ajouté en base.";
	}


	/// <summary>
	/// SELECT * FROM Class
	/// </summary>
	public async Task<List<string>> GetLocationList()
	{
		List<Location> locations;
		List<string> formattedLocations = new();

		try
		{
			locations = await _locationRepository.GetLocationList();
		}
		catch (Exception ex)
		{
			return new List<string>() { "Une erreur de récupération en base est survenue :\n" + ex.Message };
		}

		locations.ForEach(location => formattedLocations.Add(FormatLocation(location)));
		return formattedLocations;
	}


	/// <summary>
	/// SELECT * FROM Class WHERE Id = id
	/// </summary>
	public async Task<string> GetLocationById(int id)
	{
		Location? location;

		try
		{
			location = await _locationRepository.GetLocationById(id);
			if (location == null)
				return $"Aucune location ne correspond à l'identifiant {id} en base";
			else
				return FormatLocation(location);
		}
		catch (Exception ex)
		{
			return "Une erreur de récupération en base est survenue :\n" + ex.Message;
		}
	}


	/// <summary>
	/// UPDATE Class SET (params) WHERE Id = id
	/// </summary>
	public async Task<string> UpdateLocation(int id,
											int? idClient = 0,
											int? idVehicule = 0,
											int nbKm = 0,
											DateTime dateDebut = default,
											DateTime? dateFin = default)
	{
		Location location = new(id, idClient, idVehicule, nbKm, dateDebut, dateFin);
		NullablePropertiesCheck(location, Mode.UPDATE);

		Dictionary<string, dynamic?> parameters = RetrieveUpdateParameters(location);

		try
		{
			if (parameters.Count == 5)
				await _locationRepository.UpdateLocation(id, location);
			else
				await _locationRepository.UpdateLocationSpecificUnsafe(id, parameters);			
		}
		catch (Exception ex)
		{
			return "Une erreur de mise à jour en base est survenue :\n" + ex.Message;
		}

		return $"La location numéro {id} à été mis à jour.";
	}


	/// <summary>
	/// DELETE FROM Class WHERE Id = id 
	/// </summary>
	public async Task<string> DeleteLocation(int id)
	{
		try
		{
			await _locationRepository.DeleteLocation(id);
		}
		catch (Exception ex)
		{

			return "Une erreur de suppression en base est survenue :\n" + ex.Message;
		}

		return $"La location numéro {id} à été supprimée.";
	}


	/// <summary>
	/// Returns a string representation of the current object's properties
	/// </summary>
	public string FormatLocation(Location location)
	{
		return
			$"Identifiant : {location.Id}\n" +
			$"Identifiant client : {location.Id_Client}\n" +
			$"Identifiant véhicule : {location.Id_Vehicule}\n" +
			$"Nombre de kilomètres : {location.Nb_Km}\n" +
			$"Date de début : {location.Date_Debut.ToShortDateString()}\n" +
			$"Date de fin : {location.Date_Fin?.ToShortDateString()}\n";
	}


	/// <summary>
	/// Switches nullable properties to null with the corresponding modeset.
	/// Could be done via reflection, but for the sake of the exercise...
	/// </summary>
	/// <param name="location"></param>
	private void NullablePropertiesCheck(Location location, Mode mode)
	{
		if (mode == Mode.CREATE)
		{
			location.Id_Client = location.Id_Client <= 0 ? null : location.Id_Client;
			location.Id_Vehicule = location.Id_Vehicule <= 0 ? null : location.Id_Vehicule;
			location.Date_Fin = location.Date_Fin == default ? null : location.Date_Fin;
		}
		else if (mode == Mode.UPDATE)
		{
			location.Id_Client = location.Id_Client < 0 ? null : location.Id_Client;
			location.Id_Vehicule = location.Id_Vehicule < 0 ? null : location.Id_Vehicule;
			location.Date_Fin = location.Date_Fin == DateTime.MaxValue ? null : location.Date_Fin;
		}
	}


	/// <summary>
	/// Gets an object, tests all property types and values,
	/// then returns a dictionnary of the necessary parameters for the update.
	/// Could be done via reflection, but for the sake of the exercise...
	/// </summary>
	/// <param name="client"></param>
	/// <returns></returns>
	private Dictionary<string, dynamic?> RetrieveUpdateParameters(Location location)
	{
		Dictionary<string, dynamic?> inputParams = new()
		{
			{"Id_Client", location.Id_Client},
			{"Id_Vehicule", location.Id_Vehicule},
			{"Nb_Km", location.Nb_Km},
			{"Date_Debut", location.Date_Debut},
			{"Date_Fin", location.Date_Fin}
		};

		Dictionary<string, dynamic?> outputParams = new();

		for (int i = 0; i < inputParams.Count; i++)
		{
			dynamic? value = inputParams.ElementAt(i).Value;

			if (value is null)
				outputParams.Add(inputParams.ElementAt(i).Key, null);
			else if ((value is string && value != "") || (value is int && value != 0) ||(value is DateTime && value != default(DateTime)))
				outputParams.Add(inputParams.ElementAt(i).Key, value);
		}

		return outputParams;
	}
}
