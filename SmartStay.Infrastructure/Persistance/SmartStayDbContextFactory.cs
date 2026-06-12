using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SmartStay.Infrastructure.Persistance;

namespace SmartStay.Infrastructure;

public class SmartStayDbContextFactory : IDesignTimeDbContextFactory<SmartStayDbContext>
{
    public SmartStayDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SmartStayDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=SmartStay;Username=myuser;Password=mypassword");
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));

        return new SmartStayDbContext(optionsBuilder.Options);
    }
}