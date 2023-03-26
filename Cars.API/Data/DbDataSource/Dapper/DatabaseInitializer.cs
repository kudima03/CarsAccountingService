using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Cars.API.Data.DbDataSource.Dapper;

public class DatabaseInitializer
{
    private readonly string _dbConnectionString;

    public DatabaseInitializer(IConfiguration configuration)
    {
        _dbConnectionString = configuration.GetValue<string>("DefaultConnection");
    }

    public void Initialize()
    {
        var command = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Setup", "InitialScript.sql"));

        using IDbConnection connection = new SqlConnection(_dbConnectionString);
        connection.Execute(command);
    }
}