using System.Data;
using Dapper;
using Npgsql;
using Account_Web.Models;

namespace Account_Web.Repositorys;

public class UserRepository: BaseRepository
{
    public UserRepository(string connectionString) : base(connectionString)
    {
    }

    public IEnumerable<User> GetAllUsers()
    {
        string sql = @"SELECT ""Id"", ""UserId"", ""UserName"", ""Password"", ""FactoryId""
                      , ""Email"", ""CreatedAt"", ""UpdatedAt"" FROM ""Users"";";
        return Query<User>(sql);
    }

    public User GetUserById(int id)
    {
        string sql = @"SELECT ""Id"", ""UserId"", ""UserName"", ""Password"", ""FactoryId""
                      , ""Email"", ""CreatedAt"", ""UpdatedAt"" 
                      FROM ""Users""
                      WHERE ""Id"" = @Id;";
        return QueryFirstOrDefault<User>(sql, new { Id = id });
    }

    public int InsertUser(User user)
    {
        string sql = @"INSERT INTO ""Users"" (""UserId"", ""UserName"", ""Password"", ""FactoryId"", ""Email"", 
                       ""CreatedAt"", ""UpdatedAt"") VALUES (@UserId, @UserName, @Password, @FactoryId, @Email, NOW(), NOW())
                       RETURNING ""Id"";";
        return ExecuteScalarInt(sql, user);
    }

    public int UpdateUser(User user)
    {
        string sql = @"UPDATE ""Users"" 
                       SET ""UserName"" = @UserName,
                           ""Password"" = @Password,
                           ""FactoryId"" = @FactoryId,
                           ""Email"" = @Email,
                           ""UpdatedAt"" = NOW()
                       WHERE ""Id"" = @Id;";
        return Execute(sql, user);
    }

    public int DeleteUser(int id)
    {
        string sql = @"DELETE FROM ""Users"" WHERE ""Id"" = @Id;";
        return Execute(sql, new { Id = id });
    }
}