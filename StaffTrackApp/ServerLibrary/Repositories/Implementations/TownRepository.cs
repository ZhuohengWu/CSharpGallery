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

public class TownRepository(StaffTrackDb dbContext) : IGenericRepository<Town>
{
    public static GeneralResponse Success() => new(true, "Town process complete");
    public static GeneralResponse NotFound() => new(false, "Town not found");
    public static GeneralResponse AlreadyAdded() => new(false, "Town already added");

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var dep = await dbContext.Towns.FindAsync(id);
        if (dep is null) return NotFound();

        dbContext.Towns.Remove(dep);
        await Commit();
        return Success();
    }

    public async Task<List<Town>> GetAll()
    => await dbContext.Towns.ToListAsync();

    public async Task<Town> GetById(int id)
    => await dbContext.Towns.FindAsync(id) ?? new();

    public async Task<GeneralResponse> Insert(Town item)
    {
        if (await CheckNameExist(item.Name)) return AlreadyAdded();

        dbContext.Towns.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Town item)
    {
        var dep = await dbContext.Towns.FindAsync(item.Id);
        if (dep is null) return NotFound();

        dep.Name = item.Name;
        await Commit();
        return Success();
    }

    private async Task Commit() => await dbContext.SaveChangesAsync();
    private async Task<bool> CheckNameExist(string name)
    {
        var result = await dbContext.Towns.FirstOrDefaultAsync(_ => _.Name.ToLower() == name.ToLower());
        return result is not null;
    }
}
