using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations;

public class DepartmentRepository(StaffTrackDb dbContext) : IGenericRepository<Department>
{
    public static GeneralResponse Success() => new(true, "Department process complete");
    public static GeneralResponse NotFound() => new(false, "Department not found");
    public static GeneralResponse AlreadyAdded() => new(false, "Department already added");
   
    public async Task<GeneralResponse> DeleteById(int id)
    {
        var dep = await dbContext.Departments.FindAsync(id);
        if (dep is null) return NotFound();

        dbContext.Departments.Remove(dep);
        await Commit();
        return Success();
    }

    public async Task<List<Department>> GetAll()
    => await dbContext.Departments.ToListAsync();

    public async Task<Department> GetById(int id)
    => await dbContext.Departments.FindAsync(id) ?? new();

    public async Task<GeneralResponse> Insert(Department item)
    {
        if (await CheckNameExist(item.Name)) return AlreadyAdded(); 

        dbContext.Departments.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Department item)
    {
        var dep = await dbContext.Departments.FindAsync(item.Id);
        if (dep is null) return NotFound();

        dep.Name = item.Name;
        await Commit();
        return Success();
    }

    private async Task Commit() => await dbContext.SaveChangesAsync();
    private async Task<bool> CheckNameExist(string name)
    {
        var result = await dbContext.Departments.FirstOrDefaultAsync(_ => _.Name.ToLower() == name.ToLower());
        return result is not null;
    }
}
