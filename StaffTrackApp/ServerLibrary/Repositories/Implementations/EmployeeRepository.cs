using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations;

public class EmployeeRepository(StaffTrackDb dbContext) : IGenericRepository<Employee>
{
    public static GeneralResponse Success() => new(true, "Success operation for Employee");
    public static GeneralResponse NotFound() => new(false, "Employee not found");
    public static GeneralResponse AlreadyAdded() => new(false, "Employee already added");

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var dbItem = await dbContext.Employees.FindAsync(id);
        if (dbItem is null) return NotFound();

        dbContext.Employees.Remove(dbItem);
        await Commit();
        return Success();
    }

    public async Task<List<Employee>> GetAll()
    {
        return await dbContext.Employees
            .AsNoTracking()
            .Include(_ => _.Town)
            .ThenInclude(_ => _!.City)
            .ThenInclude(_ => _!.Country)

            .Include(_ => _.Branch)
            .ThenInclude(_ => _!.Department)
            .ThenInclude(_ => _!.GeneralDepartment)
            .ToListAsync();
    }

    public async Task<Employee> GetById(int id)
    {
        var employee = await dbContext.Employees
            .AsNoTracking()
            .Include(_ => _.Town)
            .ThenInclude(_ => _!.City)
            .ThenInclude(_ => _!.Country)

            .Include(_ => _.Branch)
            .ThenInclude(_ => _!.Department)
            .ThenInclude(_ => _!.GeneralDepartment)
            .FirstOrDefaultAsync(_ =>_.Id == id);

        return employee!;
    }

    public async Task<GeneralResponse> Insert(Employee item)
    {
        if (await CheckNameExist(item.Name)) return AlreadyAdded();

        dbContext.Employees.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Employee item)
    {
        var dbItem = await dbContext.Employees.FindAsync(item.Id);
        if (dbItem is null) return NotFound();

        dbItem.Name = item.Name;
        dbItem.Other = item.Other;
        dbItem.Address = item.Address;
        dbItem.PhoneNumber = item.PhoneNumber;
        dbItem.BranchId = item.BranchId;
        dbItem.TownId = item.TownId;
        dbItem.CivilId = item.CivilId;
        dbItem.FileNumber = item.FileNumber;
        dbItem.JobName = item.JobName;
        dbItem.Photo = item.Photo;

        await Commit();
        return Success();
    }

    private async Task Commit() => await dbContext.SaveChangesAsync();
    private async Task<bool> CheckNameExist(string name)
    {
        var result = await dbContext.Employees.FirstOrDefaultAsync(_ => _.Name.ToLower() == name.ToLower());
        return result is not null;
    }
}
