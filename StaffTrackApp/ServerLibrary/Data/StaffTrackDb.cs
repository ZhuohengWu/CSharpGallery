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
    public DbSet<Sanction> Sanctions { get; set; }
    public DbSet<SanctionType> SanctionTypes { get; set; }
    public DbSet<Doctor> Doctors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Country -> City
        modelBuilder.Entity<City>()
            .HasOne(c => c.Country)
            .WithMany(co => co.Cities)
            .HasForeignKey(c => c.CountryId)
            .OnDelete(DeleteBehavior.Cascade);

        // City -> Town
        modelBuilder.Entity<Town>()
            .HasOne(t => t.City)
            .WithMany(c => c.Towns)
            .HasForeignKey(t => t.CityId)
            .OnDelete(DeleteBehavior.Cascade);

        // Town -> Employee
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Town)
            .WithMany(t => t.Employees)
            .HasForeignKey(e => e.TownId)
            .OnDelete(DeleteBehavior.SetNull); // when town deleted,set TownId in Employee to null
             

        // Branch -> Department
        modelBuilder.Entity<Branch>()
            .HasOne(b => b.Department)
            .WithMany(d => d.Branches)
            .HasForeignKey(b => b.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Department -> General Department
        modelBuilder.Entity<Department>()
            .HasOne(d => d.GeneralDepartment)
            .WithMany(g => g.Departments)
            .HasForeignKey(d => d.GeneralDepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Employee -> Branch
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Branch)
            .WithMany(b => b.Employees)
            .HasForeignKey(e => e.BranchId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
