using Account_Web.Models;

namespace Account_Web.Repositorys;

public class FactoryRepository: BaseRepository<Factory>
{
    public FactoryRepository(IConfiguration config) : base(config.GetConnectionString("DefaultConnection")!)
    {
        
    }
}