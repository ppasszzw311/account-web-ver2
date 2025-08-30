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

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public User GetUserById(int id)
    {
        return _userRepository.GetUserById(id);
    }

    public int CreateUser(User user)
    {
        return _userRepository.InsertUser(user);
    }

    public void CreateTwoUsers(User user1, User user2)
    {
        try
        {
            _userRepository.BeginTransation();

            _userRepository.InsertUser(user1);
            _userRepository.InsertUser(user2);
            
            _userRepository.Commit();
        }
        catch (Exception e)
        {
            _userRepository.Rollback();
            throw;
        }
    }
}