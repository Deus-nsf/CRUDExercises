using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CRUDExercises.EF.Entities;

using Microsoft.EntityFrameworkCore;
//using CRUDExercises.EF.LegacyEntities;

namespace CRUDExercises.EF.Repositories;


internal class ClientRepository
{
	private readonly LocationDbContext _locationDbContext = new();


	public async Task AddClient(Client client)
	{
		await _locationDbContext.Clients.AddAsync(client);
		await _locationDbContext.SaveChangesAsync();
	}


	public async Task<List<Client>> GetClientList()
	{
		return await _locationDbContext.Clients.ToListAsync();
	}


	public async Task<Client?> GetClientById(int id)
	{
		return await _locationDbContext.Clients.FindAsync(id);
	}


	public async Task UpdateClient(int id, Client client)
	{
		await _locationDbContext.Clients.Where(c => c.Id == id).ExecuteUpdateAsync
		(
			updates => updates.SetProperty(c => c.Nom, client.Nom)
								.SetProperty(c => c.Prenom, client.Prenom)
								.SetProperty(c => c.Date_Naissance, client.Date_Naissance)
								.SetProperty(c => c.Adresse, client.Adresse)
								.SetProperty(c => c.Code_Postal, client.Code_Postal)
								.SetProperty(c => c.Ville, client.Ville)
		);
	}


	public async Task UpdateClientSpecificUnsafe(int id, Dictionary<string, dynamic?> parameters)
	{
		StringBuilder updateQuery = new();
		updateQuery.Append("UPDATE CLIENT SET");

		foreach (var parameter in parameters)
			updateQuery.Append($" {parameter.Key.ToUpper()} = '{parameter.Value}',");

		updateQuery.Remove(updateQuery.Length - 1, 1); 
		updateQuery.Append($" WHERE ID = {id}");

		await _locationDbContext.Database.ExecuteSqlRawAsync(updateQuery.ToString());
	}


	public async Task DeleteClient(int id)
	{
		await _locationDbContext.Locations.Where(l => l.Id_Client == id).ExecuteDeleteAsync();
		await _locationDbContext.Clients.Where(c => c.Id == id).ExecuteDeleteAsync();
	}
}