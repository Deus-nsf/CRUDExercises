using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDExercises.EF.LegacyEntities;

namespace CRUDExercises.EF.Repositories;


internal class ClientRepository
{
	public async Task AddClient(Client client)
	{
		
	}


	public async Task<List<Client>> GetClientList()
	{
		return new();
	}


	public async Task<Client> GetClientById(int id)
	{
		return null;
	}


	public async Task UpdateClient(int id, Dictionary<string, dynamic?> parameters)
	{
		
	}


	public async Task DeleteClient(int id)
	{
		
	}
}
