using Data.Repositories;
using Domain.Models;

namespace Business.Services;

public class ClientService(ClientRepository ClientRepository)
{
    private readonly ClientRepository _clientRepository = ClientRepository;

    public async Task<IEnumerable<Client>> GetAllClientAsync()
    {
        var entities = await _clientRepository.GetAllAsync();
        var clients = entities.Select(client => new Client { Id = client.Id, ClientName = client.ClientName});
        return clients;
    }
}

