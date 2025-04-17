using Data.Repositories;
using Domain.Models;

namespace Business.Services;

public class StatusService(StatusRepository StatusRepository)
{
    private readonly StatusRepository _statusRepository = StatusRepository;

    public async Task<IEnumerable<Status>> GetAllStatusAsync()
    {
        var entities = await _statusRepository.GetAllAsync();
        var statuses = entities.Select(status => new Status { Id = status.Id, StatusName = status.StatusName });
        return statuses;
    }

    public async Task<Status?> GetStatusByIdAsync(int statusId)
    {
        var entity = await _statusRepository.GetAsync(x => x.Id == statusId);
        return entity == null ? null : new Status { Id = entity.Id, StatusName = entity.StatusName };
    }
}

