using Account_Web.Models;

namespace Account_Web.Repositorys;

public class DomainCategoryRepository: BaseRepository<DomainCategory>
{
    public DomainCategoryRepository(IConfiguration config) : base(config.GetConnectionString("DefaultConnection")!)
    {

    }

    public async Task<int> CreateDomain(DomainCategory entity)
    {
        // 確認是否已存在相同的 CategoryName
        var existing = await GetAllAsync();
        if (existing.Any(e => e.Name == entity.Name))
        {
            throw new Exception($"DomainCategory with Name '{entity.Name}' already exists.");
        }
        return await CreateAsync(entity);
    }
}
