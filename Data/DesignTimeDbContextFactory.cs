using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
            ?? "Server=localhost,1433;Database=FashionBingo;User Id=sa;Password=makesomeMoney$9;TrustServerCertificate=True;";

        optionsBuilder.UseSqlServer(connectionString);
        return new AppDbContext(optionsBuilder.Options);
    }
}
