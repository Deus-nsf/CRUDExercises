using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CRUDExercises.EF.Entities;

using Microsoft.EntityFrameworkCore;

namespace CRUDExercises.EF.Repositories;


internal class LocationRepository
{
	private readonly LocationDbContext _locationDbContext = new();


	public async Task AddLocation(Location location)
	{
		await _locationDbContext.Locations.AddAsync(location);
		await _locationDbContext.SaveChangesAsync();
	}


	public async Task<List<Location>> GetLocationList()
	{
		return await _locationDbContext.Locations.ToListAsync();
	}


	public async Task<Location?> GetLocationById(int id)
	{
		return await _locationDbContext.Locations.FindAsync(id);
	}


	public async Task UpdateLocation(int id, Location location)
	{
		await _locationDbContext.Locations.Where(l => l.Id == id).ExecuteUpdateAsync
		(
			updates => updates.SetProperty(l => l.Id_Client, location.Id_Client)
								.SetProperty(l => l.Id_Vehicule, location.Id_Vehicule)
								.SetProperty(l => l.Nb_Km, location.Nb_Km)
								.SetProperty(l => l.Date_Debut, location.Date_Debut)
								.SetProperty(l => l.Date_Fin, location.Date_Fin)
		);
	}


	public async Task UpdateLocationSpecificUnsafe(int id, Dictionary<string, dynamic?> parameters)
	{
		StringBuilder updateQuery = new();
		updateQuery.Append("UPDATE LOCATION SET");

		foreach (var parameter in parameters)
			updateQuery.Append($" {parameter.Key.ToUpper()} = '{parameter.Value}',");

		updateQuery.Remove(updateQuery.Length - 1, 1); 
		updateQuery.Append($" WHERE ID = {id}");

		await _locationDbContext.Database.ExecuteSqlRawAsync(updateQuery.ToString());
	}


	public async Task DeleteLocation(int id)
	{
		await _locationDbContext.Locations.Where(l => l.Id == id).ExecuteDeleteAsync();
	}
}
