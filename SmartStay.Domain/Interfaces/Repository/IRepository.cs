namespace SmartStay.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T?>> GetAllAsync();
    Task<T?> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    
    
    //M.G: Lower methods are needed for use with class TransactionSecurity
    Task<T?> AddWithoutSavingAsync(T entity);
    void UpdateWithoutSaving(T entity);
    void DeleteWithoutSaving(T entity);
}