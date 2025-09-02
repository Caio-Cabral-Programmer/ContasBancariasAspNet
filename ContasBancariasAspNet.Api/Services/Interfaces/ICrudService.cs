namespace ContasBancariasAspNet.Api.Services.Interfaces;

public interface ICrudService<TId, T>
{
    Task<IEnumerable<T>> FindAllAsync();
    Task<T> FindByIdAsync(TId id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(TId id, T entity);
    Task DeleteAsync(TId id);
}