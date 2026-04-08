using Microsoft.EntityFrameworkCore;
using SmartStay.Domain.Interfaces;
using SmartStay.Infrastructure.Persistance;

namespace SmartStay.Infrastructure;

public class Repository<T>: IRepository<T> where T: class
{
   protected readonly SmartStayDbContext _dbContext;
   protected readonly DbSet<T> _dbSet;

   public Repository(SmartStayDbContext dbContext) {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();//M.G: Reduces boilerplate 
   }

   public async Task DeleteAsync(T entity)
   {
       _dbSet.Remove(entity);
       await _dbContext.SaveChangesAsync();
   }

   public async Task<IEnumerable<T?>> GetAllAsync()
       => await _dbSet.ToListAsync();

   public async Task<T?> GetByIdAsync(Guid id)
       => await _dbSet.FindAsync(id);
   
   public async Task<T> AddAsync(T entity)
   {
       await _dbSet.AddAsync(entity);
       await _dbContext.SaveChangesAsync();
       return entity;//M.G: ef core updates object automatically.
   }

   public async Task UpdateAsync(T entity)
   {
       _dbSet.Update(entity);
       await _dbContext.SaveChangesAsync();
   }
}