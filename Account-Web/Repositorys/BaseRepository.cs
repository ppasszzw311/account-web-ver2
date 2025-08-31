using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using Account_Web.Models;
using Dapper;
using Npgsql;

namespace Account_Web.Repositorys;

public abstract class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : BaseModel
{
    protected readonly IDbConnection _connection;
    protected IDbTransaction _transaction;

    protected BaseRepository(string connectionString)
    {
        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
    }

    private bool IsTransactionActive =>
        _transaction != null &&
        _transaction.Connection != null &&
        _transaction.Connection.State == ConnectionState.Open;
    
    // 開始交易
    public void BeginTransaction()
    {
        if (IsTransactionActive) return;
        _transaction = _connection.BeginTransaction();
    }
    
    // 提交交易
    public void Commit()
    {
        if (!IsTransactionActive) return;
        _transaction!.Commit();
        _transaction.Dispose();
        _transaction = null;
    }
    
    // 回滾交易
    public void Rollback()
    {
        if (!IsTransactionActive) return;
        _transaction!.Rollback();
        _transaction.Dispose();
        _transaction = null;
    }

    public void Dispose()
    {
        if (_transaction != null)
        {
            _transaction.Dispose();
            _transaction = null;
        }
        if (_connection != null && _connection.State != ConnectionState.Closed)
        {
            _connection.Close();
            _connection.Dispose();
        }
    }
    
    // 查詢多筆
    protected Task<IEnumerable<T>> QueryAsync(string sql, object param = null)
        => _connection.QueryAsync<T>(sql, param, transaction: _transaction);
    // 查詢單筆（找不到回default）
    protected Task<T> QueryFirstOrDefaultAsync(string sql, object param = null)
        => _connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction: _transaction);
    
    // 執行insert/update/delete 回傳影響數
    protected Task<int> ExecuteAsync(string sql, object param = null)
        => _connection.ExecuteAsync(sql, param, transaction: _transaction);
    
    // 新增，回傳ID
    protected Task<int> ExecuteScalarIntAsync(string sql, object param = null) 
        => _connection.ExecuteScalarAsync<int>(sql, param, transaction: _transaction);

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var sql = $"SELECT * FROM {GetTableName()};";
        return await QueryAsync(sql);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var sql = $@"SELECT * FROM {GetTableName()} WHERE ""Id"" = @Id";
        return await QueryFirstOrDefaultAsync(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(T entity)
    {
        var now = DateTime.UtcNow;
        entity.CreatedAt = now;
        entity.UpdatedAt = now;
        var (columns, parameters) = GetColumnsAndParameters(excludeId: true);
        var sql = $@"INSERT INTO {GetTableName()} ({columns}) VALUES ({parameters}) RETURNING ""Id"";";
        return await ExecuteScalarIntAsync(sql, entity);
    }
    
    public async Task<bool> UpdateAsync(T entity) 
    {
        entity.UpdatedAt = DateTime.UtcNow;
        
        var setClause = GetSetClause(excludeId: true, extraExcludes: new[] {"CreatedAt","UpdatedAt"}); 
        var sql = $@"UPDATE {GetTableName()} SET {setClause} ""UpdatedAt"" = @UpdatedAt WHERE ""Id"" = @Id;"; 
        var affectedRows = await ExecuteAsync(sql, entity); 
        return affectedRows > 0; 
    } 
    public async Task<bool> DeleteAsync(int id) 
    { 
        var sql = $@"DELETE FROM {GetTableName()} WHERE ""Id"" = @Id"; 
        var affectedRows = await ExecuteAsync(sql, new { Id = id }); 
        return affectedRows > 0; 
    }

    private string GetTableName()
    {
        // 嘗試讀取 [Table("xxx")] 特性
        var attr = typeof(T).GetCustomAttribute<TableAttribute>();
        if (attr != null)
        {
            if (!string.IsNullOrWhiteSpace(attr.Schema))
                return $@"""{attr.Schema}"".""{attr.Name}""";
            return $@"""{attr.Name}"""; // PostgreSQL 用雙引號保持大小寫
        }

        // 如果沒有標註 [Table]，就用類別名稱
        return $@"""{typeof(T).Name}""";
    }


    private static IEnumerable<PropertyInfo> GetProperties(bool excludeId, IEnumerable<string>? extraExcludes = null)
    {
        var properties = typeof(T)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.CanRead && p.CanWrite);
        if (excludeId)
            properties = properties.Where(p => !string.Equals(p.Name, "Id", StringComparison.OrdinalIgnoreCase));
        if (extraExcludes != null)
        {
            var block = new HashSet<string>(extraExcludes, StringComparer.OrdinalIgnoreCase);
            properties = properties.Where(p => !block.Contains(p.Name));
        }
        return properties;
    }
    
    private static (string columns, string parameters) GetColumnsAndParameters(bool excludeId) 
    { 
        var properties = GetProperties(excludeId);
        var columns = string.Join(", ", properties.Select(p => $@"""{p.Name}""")); 
        var parameters = string.Join(", ", properties.Select(p => $"@{p.Name}"));
        return (columns, parameters);
    }
    
    private static string GetSetClause(bool excludeId, IEnumerable<string>? extraExcludes = null) 
    { 
        var properties = GetProperties(excludeId, extraExcludes); 
        return string.Join(", ", properties.Select(p => $@"""{p.Name}"" = @{p.Name}")); 
    }

}