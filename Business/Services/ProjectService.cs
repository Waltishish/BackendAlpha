using Data.Entities;
using Data.Repositories;
using Domain.Models;

namespace Business.Services;

public class ProjectService(ProjectRepository ProjectRepository)
{
    private readonly ProjectRepository _projectRepository = ProjectRepository;

    public async Task<bool> CreateProjectASync(AddProjectFormData projectFormData)
    {
        if (projectFormData == null)
            return false;

        var entity = new ProjectEntity
        {
            ProjectName = projectFormData.ProjectName,
            Description = projectFormData.Description,
            StartDate = projectFormData.StartDate,
            EndDate = projectFormData.EndDate,
            Budget = projectFormData.Budget,
            ClientId = projectFormData.ClientId,
            UserId = projectFormData.UserId,
            StatusId = 1
        };

        var result = await _projectRepository.AddAsync(entity);
        return result;
    }
     public async Task<IEnumerable<Project>> GetAllProjectsAsync(bool orderByDescending = false)
    {
        var entities = await _projectRepository.GetAllAsync();
        var projects = entities.Select(entity => new Project
        {
            Id = entity.Id,
            ProjectName = entity.ProjectName,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Budget = entity.Budget,
            Created = entity.Created,
            Client = new Client
            {
                Id = entity.Client.Id,
                ClientName = entity.Client.ClientName,
            },
            User = new User
            {
                Id = entity.User.Id,
                FirstName = entity.User.FirstName,
                LastName = entity.User.LastName,
            },
            Status = new Status
            {
                Id = entity.Status.Id,
                StatusName = entity.Status.StatusName,
            }
        });
        
        return orderByDescending
            ? projects.OrderByDescending(entity => entity.Created)
            : projects.OrderBy(entity => entity.Created);
    }

    public async Task<Project?> GetProjectByIdAsync(string projectId)
    {
        var entity = await _projectRepository.GetAsync(x => x.Id == projectId);
        return entity == null ? null : new Project()
        {
            Id = entity.Id,
            ProjectName = entity.ProjectName,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Budget = entity.Budget,
            Created = entity.Created,
            Client = new Client
            {
                Id = entity.Client.Id,
                ClientName = entity.Client.ClientName,
            },
            User = new User
            {
                Id = entity.User.Id,
                FirstName = entity.User.FirstName,
                LastName = entity.User.LastName,
            },
            Status = new Status
            {
                Id = entity.Status.Id,
                StatusName = entity.Status.StatusName,
            }
        };
    }


    public async Task<bool> UpdateProjectASync(UpdateProjectFormData projectFormData)
    {
        if (projectFormData == null)
            return false;

        var entity = new ProjectEntity
        {
            Id = projectFormData.Id,
            ProjectName = projectFormData.ProjectName,
            Description = projectFormData.Description,
            StartDate = projectFormData.StartDate,
            EndDate = projectFormData.EndDate,
            Budget = projectFormData.Budget,
            ClientId = projectFormData.ClientId,
            UserId = projectFormData.UserId,
            StatusId = projectFormData.StatusId,
        };

        var result = await _projectRepository.UpdateAsync(entity);
        return result;
    }

    public async Task<bool> DeleteProjectAsync(string projectId)
    {
        if (string.IsNullOrEmpty(projectId))
            return false;

        var result = await _projectRepository.DeleteAsync(x => x.Id == projectId);
        return result;
    }
}

