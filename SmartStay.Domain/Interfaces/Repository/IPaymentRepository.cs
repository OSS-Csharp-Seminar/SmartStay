using SmartStay.Domain.Entities;

namespace SmartStay.Domain.Interfaces;

public interface IPaymentRepository:IRepository<Payment>
{
    Task<Payment?> GetByBookingIdAsync(Guid bookingId);
}