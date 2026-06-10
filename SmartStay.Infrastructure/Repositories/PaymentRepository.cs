using SmartStay.Domain.Entities;
using SmartStay.Domain.Interfaces;
using SmartStay.Infrastructure.Persistance;

namespace SmartStay.Infrastructure.Repositories;

public class PaymentRepository: Repository<Payment>, IPaymentRepository
{
    public PaymentRepository(SmartStayDbContext dbContext) : base(dbContext)
    {
    }
}