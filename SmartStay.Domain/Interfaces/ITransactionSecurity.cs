namespace SmartStay.Domain.Interfaces;

/// <summary>
/// Class for securing that all of your methods inside transaction are executed or none!
/// <para>
/// <para>
/// <b>!!!Use methods from repository that don't save on their own or else rollback can't do its work!!!</b>
/// </para>
/// </para>
/// </summary>
public interface ITransactionSecurity
{
   /// <summary>
   /// Save changes made in transaction
   /// </summary>
   Task SaveChangesAsync();
   /// <summary>
   /// Must be called before try/catch to start transaction
   /// </summary>
   Task BeginTransactionAsync();
   /// <summary>
   /// Commits all changes made in transaction
   /// </summary>
   Task CommitAsync();
   /// <summary>
   /// best to call inside catch(){}  so it rolls back if anything goes wrong in try{}
   /// </summary>
   /// <returns></returns>
   Task RollbackAsync();
   
}