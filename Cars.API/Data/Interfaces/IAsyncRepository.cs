namespace Cars.API.Data.Interfaces;

public interface IAsyncRepository<T> : IRepository<T>
{
    Task<IQueryable<T>> GetAllAsync();

    Task<IQueryable<T>> GetRangeAsync(int skip, int take);

    Task<T> GetByIdAsync(long id);

    Task<T> CreateAsync(T entity);

    Task<T> UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}