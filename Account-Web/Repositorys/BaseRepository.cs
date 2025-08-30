using System.Data;
using Dapper;
using Npgsql;

namespace Account_Web.Repositorys;

public abstract class BaseRepository : IDisposable
{
    protected readonly IDbConnection _connection;
    protected IDbTransaction _transaction;

    protected BaseRepository(string connectionString)
    {
        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
    }
    
    // 開始交易
    public void BeginTransation()
    {
        if (_transaction == null)
            _transaction = _connection.BeginTransaction();
    }
    
    // 提交交易
    public void Commit()
    {
        _transaction?.Commit();
        _transaction?.Dispose();
        _transaction = null;
    }
    
    // 回滾交易
    public void Rollback()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
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
    protected IEnumerable<T> Query<T>(string sql, object param = null)
    {
        return _connection.Query<T>(sql, param, transaction: _transaction);
    }
    // 查詢單筆（找不到回default）
    protected T QueryFirstOrDefault<T>(string sql, object param = null)
    {
        return _connection.QueryFirstOrDefault<T>(sql, param, transaction: _transaction);
    }
    
    // 執行insert/update/delete 回傳影響數
    protected int Execute(string sql, object param = null)
    {
        return _connection.Execute(sql, param, transaction: _transaction);
    }
    
    // 新增，回傳ID
    protected int ExecuteScalarInt(string sql, object param = null)
    {
        return _connection.ExecuteScalar<int>(sql, param, transaction: _transaction);
    }
}