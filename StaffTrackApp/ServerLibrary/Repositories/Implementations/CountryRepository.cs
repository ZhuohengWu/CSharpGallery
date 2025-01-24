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

public class CountryRepository(StaffTrackDb dbContext) : IGenericRepository<Country>
{
    public static GeneralResponse Success() => new(true, "Country process complete");
    public static GeneralResponse NotFound() => new(false, "Country not found");
    public static GeneralResponse AlreadyAdded() => new(false, "Country already added");

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var dep = await dbContext.Countrys.FindAsync(id);
        if (dep is null) return NotFound();

        dbContext.Countrys.Remove(dep);
        await Commit();
        return Success();
    }

    public async Task<List<Country>> GetAll()
    => await dbContext.Countrys.ToListAsync();

    public async Task<Country> GetById(int id)
    => await dbContext.Countrys.FindAsync(id) ?? new();

    public async Task<GeneralResponse> Insert(Country item)
    {
        if (await CheckNameExist(item.Name)) return AlreadyAdded();

        dbContext.Countrys.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Country item)
    {
        var dep = await dbContext.Countrys.FindAsync(item.Id);
        if (dep is null) return NotFound();

        dep.Name = item.Name;
        await Commit();
        return Success();
    }

    private async Task Commit() => await dbContext.SaveChangesAsync();
    private async Task<bool> CheckNameExist(string name)
    {
        var result = await dbContext.Countrys.FirstOrDefaultAsync(_ => _.Name.ToLower() == name.ToLower());
        return result is not null;
    }
}
