using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces;
using SmartStay.Infrastructure.Persistance;

namespace SmartStay.Infrastructure.Repositories;

public class CancellationLogRepository : Repository<CancellationLog>, ICancellationLogRepository
{
    public CancellationLogRepository(SmartStayDbContext dbContext) : base(dbContext)
    {
    }
}