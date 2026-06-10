using Microsoft.EntityFrameworkCore.Storage;
using SmartStay.Domain.Interfaces;
using SmartStay.Infrastructure.Persistance;

namespace SmartStay.Infrastructure.Services;

///<inheritdoc/>    
public class TransactionSecurity : ITransactionSecurity
{
   private readonly SmartStayDbContext _dbContext; 
   private IDbContextTransaction _transaction;
   
   public TransactionSecurity(SmartStayDbContext dbContext)
    {
        _dbContext = dbContext;
    }
   
    ///<inheritdoc/>    
    public async Task SaveChangesAsync()
    => await _dbContext.SaveChangesAsync();
    
    ///<inheritdoc/>    
    public async Task BeginTransactionAsync()
    => _transaction = await _dbContext.Database.BeginTransactionAsync();
    
    ///<inheritdoc/>    
    public Task CommitAsync()
    => _transaction.CommitAsync();

    ///<inheritdoc/>    
    public Task RollbackAsync()
    => _transaction.RollbackAsync(); 
}