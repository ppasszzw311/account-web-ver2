using Account_Web.Models;
using Account_Web.Repositorys;

namespace Account_Web.Services;

public class UserService : IUserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User> GetUserById(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<int> CreateUser(User user)
    {
        return await _userRepository.CreateAsync(user);
    }

    public async Task CreateTwoUsers(User user1, User user2)
    {
        try
        {
            _userRepository.BeginTransaction();

            await _userRepository.CreateAsync(user1);
            await _userRepository.CreateAsync(user2);
            
            _userRepository.Commit();
        }
        catch (Exception e)
        {
            _userRepository.Rollback();
            throw;
        }
    }
}