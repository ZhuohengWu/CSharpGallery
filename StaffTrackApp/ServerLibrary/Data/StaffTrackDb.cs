using BaseLibrary.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Data;

public class StaffTrackDb : DbContext
{
    public StaffTrackDb(DbContextOptions<StaffTrackDb> options) : base(options)
    {
        
    }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<GeneralDepartment> GeneralDepartments { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Branch> Branches { get; set; }

    public DbSet<Country> Countrys { get; set; }
    public DbSet<City> Citys { get; set; }
    public DbSet<Town> Towns { get; set; }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<SystemRole> SystemRoles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RefreshTokenInfo> RefreshTokenInfos { get; set; }


    public DbSet<Vacation> Vacations { get; set; }
    public DbSet<VacationType> VacationTypes { get; set; }
    public DbSet<Overtime> Overtimes { get; set; }
    public DbSet<OvertimeType> OvertimesTypes { get; set; }
    public DbSet<Sanction> Sanctions { get; set;}
    public DbSet<SanctionType> SanctionTypes { get; set; }
    public DbSet<Doctor> Doctors { get; set; }

}
