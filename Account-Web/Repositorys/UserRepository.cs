using Account_Web.Models;

namespace Account_Web.Repositorys;

public class UserRepository: BaseRepository<User>
{
    public UserRepository(IConfiguration config) : base(config.GetConnectionString("DefaultConnection")!)
    {
    }
}