using Data.Repositories;
using Domain.Models;

namespace Business.Services;

public class UserService(UserRepository userRepository)
{
    private readonly UserRepository _userRepository = userRepository;

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var entities = await _userRepository.GetAllAsync();
        var users = entities.Select(user => new User { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName });
        return users;
    }
}

