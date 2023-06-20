using Cars.API.Data.Interfaces;
using Cars.API.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Cars.API.Data.DbDataSource.Dapper;

public class DapperCarsRepository : IAsyncRepository<Car>
{
    private readonly IDbConnection _connection;

    private bool _disposed;

    public DapperCarsRepository(IConfiguration configuration)
    {
        _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public Car Create(Car entity)
    {
        string statement = CarsQueryManager.GetInsertStatement(entity);
        long id = _connection.QuerySingleOrDefault<long>(statement);

        return _connection.QuerySingleOrDefault<Car>(CarsQueryManager.GetSelectByIdStatement(id));
    }

    public async Task<Car> CreateAsync(Car entity)
    {
        string statement = CarsQueryManager.GetInsertStatement(entity);
        long id = await _connection.QuerySingleOrDefaultAsync<long>(statement);

        return await _connection.QuerySingleOrDefaultAsync<Car>(CarsQueryManager.GetSelectByIdStatement(id));
    }

    public void Delete(Car entity)
    {
        string statement = CarsQueryManager.GetDeleteStatement(entity.Id);
        _connection.Execute(statement);
    }

    public async Task DeleteAsync(Car entity)
    {
        string statement = CarsQueryManager.GetDeleteStatement(entity.Id);
        await _connection.ExecuteAsync(statement);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public IQueryable<Car> GetAll()
    {
        string statement = CarsQueryManager.GetSelectAllStatement();

        return _connection.Query<Car>(statement).AsQueryable();
    }

    public IQueryable<Car> GetRange(int skip, int take)
    {
        string statement = CarsQueryManager.GetSelectRangeStatement(skip, take);

        return _connection.Query<Car>(statement).AsQueryable();
    }

    public async Task<IQueryable<Car>> GetAllAsync()
    {
        string statement = CarsQueryManager.GetSelectAllStatement();

        return (await _connection.QueryAsync<Car>(statement)).AsQueryable();
    }

    public async Task<IQueryable<Car>> GetRangeAsync(int skip, int take)
    {
        string statement = CarsQueryManager.GetSelectRangeStatement(skip, take);

        return (await _connection.QueryAsync<Car>(statement)).AsQueryable();
    }

    public Car GetById(long id)
    {
        string statement = CarsQueryManager.GetSelectByIdStatement(id);

        return _connection.QuerySingleOrDefault<Car>(statement);
    }

    public async Task<Car> GetByIdAsync(long id)
    {
        string statement = CarsQueryManager.GetSelectByIdStatement(id);

        return await _connection.QuerySingleOrDefaultAsync<Car>(statement);
    }

    public Car Update(Car entity)
    {
        string statement = CarsQueryManager.GetUpdateStatement(entity);
        _connection.Execute(statement);

        return _connection.QuerySingleOrDefault<Car>(CarsQueryManager.GetSelectByIdStatement(entity.Id));
    }

    public async Task<Car> UpdateAsync(Car entity)
    {
        string statement = CarsQueryManager.GetUpdateStatement(entity);
        await _connection.ExecuteAsync(statement);

        return await _connection.QuerySingleOrDefaultAsync<Car>(CarsQueryManager.GetSelectByIdStatement(entity.Id));
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _connection.Dispose();
        }

        _disposed = true;
    }
}