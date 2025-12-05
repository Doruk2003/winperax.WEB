using Winperax.Api.Models;
using Winperax.Api.Repositories;
using Winperax.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Winperax.Api.Services;

public class UserService
{
    private readonly IGenericRepository<User> _userRepo;

    public UserService(IGenericRepository<User> userRepo)
    {
        _userRepo = userRepo;
    }

    public Task<IEnumerable<User>> GetAllUsersAsync() => _userRepo.GetAllAsync();
    public Task<User?> GetUserByIdAsync(string id) => _userRepo.GetByIdAsync(id);
    public Task AddUserAsync(User user) => _userRepo.AddAsync(user);
    public Task UpdateUserAsync(string id, User user) => _userRepo.UpdateAsync(id, user);
    public Task DeleteUserAsync(string id) => _userRepo.DeleteAsync(id);
}
