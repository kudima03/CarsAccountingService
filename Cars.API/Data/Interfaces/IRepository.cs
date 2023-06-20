namespace Cars.API.Data.Interfaces;

public interface IRepository<T> : IDisposable
{
    IQueryable<T> GetAll();

    IQueryable<T> GetRange(int skip, int take);

    T GetById(long id);

    T Create(T entity);

    T Update(T entity);

    void Delete(T entity);
}