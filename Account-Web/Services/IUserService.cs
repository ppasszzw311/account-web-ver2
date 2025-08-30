using Account_Web.Models;

namespace Account_Web.Services;

public interface IUserService
{
    IEnumerable<User> GetAllUsers();
    User GetUserById(int id);
    int CreateUser(User user);
    void CreateTwoUsers(User user1, User user2);
}