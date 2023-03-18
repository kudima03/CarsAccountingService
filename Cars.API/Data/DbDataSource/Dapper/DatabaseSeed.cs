﻿using System.Data;
using System.Text.Json;
using Cars.API.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Cars.API.Data.DbDataSource.Dapper;

public class DatabaseSeed
{
    private readonly string _dbConnectionString;

    public DatabaseSeed(IConfiguration configuration)
    {
        _dbConnectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public void Seed()
    {
        using IDbConnection connection = new SqlConnection(_dbConnectionString);
        if (connection.QuerySingle<int>($"SELECT COUNT(*) FROM {CarsQueryManager.TableName}") != 0) return;

        var cars = GetCarsFromFile();
        foreach (var item in cars)
        {
            var str = CarsQueryManager.GetInsertStatement(item);
            connection.Execute(str);
        }
    }

    private IEnumerable<Car> GetCarsFromFile()
    {
        var text = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Setup", "Demo cars.json"));

        return JsonSerializer.Deserialize<IEnumerable<Car>>(text);
    }
}