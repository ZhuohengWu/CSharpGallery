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

public class BranchRepository(StaffTrackDb dbContext) : IGenericRepository<Branch>
{
    public static GeneralResponse Success() => new(true, "Branch process complete");
    public static GeneralResponse NotFound() => new(false, "Branch not found");
    public static GeneralResponse AlreadyAdded() => new(false, "Branch already added");

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var dep = await dbContext.Branches.FindAsync(id);
        if (dep is null) return NotFound();

        dbContext.Branches.Remove(dep);
        await Commit();
        return Success();
    }

    public async Task<List<Branch>> GetAll()
    => await dbContext.Branches.ToListAsync();

    public async Task<Branch> GetById(int id)
    => await dbContext.Branches.FindAsync(id) ?? new();

    public async Task<GeneralResponse> Insert(Branch item)
    {
        if (await CheckNameExist(item.Name)) return AlreadyAdded();

        dbContext.Branches.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Branch item)
    {
        var dep = await dbContext.Branches.FindAsync(item.Id);
        if (dep is null) return NotFound();

        dep.Name = item.Name;
        await Commit();
        return Success();
    }

    private async Task Commit() => await dbContext.SaveChangesAsync();
    private async Task<bool> CheckNameExist(string name)
    {
        var result = await dbContext.Branches.FirstOrDefaultAsync(_ => _.Name.ToLower() == name.ToLower());
        return result is not null;
    }
}
