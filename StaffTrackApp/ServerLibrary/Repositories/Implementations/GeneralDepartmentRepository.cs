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

public class GeneralDepartmentRepository(StaffTrackDb dbContext) : IGenericRepository<GeneralDepartment>
{
    public static GeneralResponse Success() => new(true, "Success operation for General Department");
    public static GeneralResponse NotFound() => new(false, "General Department not found");
    public static GeneralResponse AlreadyAdded() => new(false, "General Department already added");
   
    public async Task<GeneralResponse> DeleteById(int id)
    {
        var dep = await dbContext.GeneralDepartments.FindAsync(id);
        if (dep is null) return NotFound();

        dbContext.GeneralDepartments.Remove(dep);
        await Commit();
        return Success();
    }

    public async Task<List<GeneralDepartment>> GetAll()
    => await dbContext.GeneralDepartments.ToListAsync();

    public async Task<GeneralDepartment> GetById(int id)
    => await dbContext.GeneralDepartments.FindAsync(id) ?? new();

    public async Task<GeneralResponse> Insert(GeneralDepartment item)
    {
        if (await CheckNameExist(item.Name)) return AlreadyAdded(); 

        dbContext.GeneralDepartments.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(GeneralDepartment item)
    {
        var dep = await dbContext.GeneralDepartments.FindAsync(item.Id);
        if (dep is null) return NotFound();

        dep.Name = item.Name;
        await Commit();
        return Success();
    }

    private async Task Commit() => await dbContext.SaveChangesAsync();
    private async Task<bool> CheckNameExist(string name)
    {
        var result = await dbContext.GeneralDepartments.FirstOrDefaultAsync(_ => _.Name.ToLower() == name.ToLower());
        return result is not null;
    }
}
