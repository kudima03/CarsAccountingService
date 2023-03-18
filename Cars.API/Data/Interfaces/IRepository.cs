namespace Cars.API.Data.Interfaces;

public interface IRepository<T> : IDisposable
{
    IEnumerable<T> GetAll();
    IEnumerable<T> GetRange(int skip, int take);
    T GetById(long id);
    T Create(T entity);
    T Update(T entity);
    void Delete(T entity);
}