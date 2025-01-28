using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Factories;

public class StaffTrackDbContextFactory : IDesignTimeDbContextFactory<StaffTrackDb>
{
    public StaffTrackDb CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StaffTrackDb>();

        optionsBuilder.UseSqlServer("Server=(local); Database=StaffTrackAppDb; Trusted_Connection=True; TrustServerCertificate=True;");

        return new StaffTrackDb(optionsBuilder.Options);
    }
}
