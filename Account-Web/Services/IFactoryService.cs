using Account_Web.Models;

namespace Account_Web.Services;

public interface IFactoryService
{
    Task<IEnumerable<Factory>> GetAllFactories();
    Task<Factory> GetFactoryById(int id);
    Task<int> CreateFactory(Factory user);
    Task CreateMockFactory(List<Factory> factories);
}