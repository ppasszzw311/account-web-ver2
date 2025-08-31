using Account_Web.Models;
using Account_Web.Repositorys;

namespace Account_Web.Services;

public class FactoryService : IFactoryService
{
    private readonly FactoryRepository _repo;

    public FactoryService(FactoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Factory>> GetAllFactories()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Factory> GetFactoryById(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task<int> CreateFactory(Factory user)
    {
        return await _repo.CreateAsync(user);
    }

    public async Task CreateMockFactory(List<Factory> factories)
    {
        try
        {
            _repo.BeginTransaction();
            foreach (var factory in factories)
            {
                await _repo.CreateAsync(factory);
            }
            _repo.Commit();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _repo.Rollback();
            throw;
        }
    }
}