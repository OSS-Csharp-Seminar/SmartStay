using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SmartStay.Infrastructure.Persistance;

namespace SmartStay.Infrastructure;

public class SmartStayDbContextFactory : IDesignTimeDbContextFactory<SmartStayDbContext>
{
    public SmartStayDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SmartStayDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=SmartStay;Username=postgres;Password=yourpassword");

        return new SmartStayDbContext(optionsBuilder.Options);
    }
}