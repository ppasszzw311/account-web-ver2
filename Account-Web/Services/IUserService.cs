using Account_Web.Models;

namespace Account_Web.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(int id);
    Task<int> CreateUser(User user);
    Task CreateTwoUsers(User user1, User user2);
}