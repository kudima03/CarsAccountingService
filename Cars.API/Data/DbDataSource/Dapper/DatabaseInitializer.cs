using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Cars.API.Data.DbDataSource.Dapper;

public class DatabaseInitializer
{
    private readonly string _dbConnectionString;

    public DatabaseInitializer(IConfiguration configuration)
    {
        _dbConnectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public void Initialize()
    {
        string command = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Setup", "InitialScript.sql"));

        using IDbConnection connection = new SqlConnection(_dbConnectionString);
        connection.Execute(command);
    }
}