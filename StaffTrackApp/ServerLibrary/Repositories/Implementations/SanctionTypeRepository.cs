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

public class SanctionTypeRepository(StaffTrackDb dbContext) : IGenericRepository<SanctionType>
{
    public static GeneralResponse Success() => new(true, "Sanction Type process complete");
    public static GeneralResponse NotFound() => new(false, "Sanction Type not found");
    public static GeneralResponse AlreadyAdded() => new(false, "Sanction Type already added");

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var dbItem = await dbContext.SanctionTypes.FirstOrDefaultAsync(d => d.Id == id);
        if (dbItem is null) return NotFound();

        dbContext.SanctionTypes.Remove(dbItem);
        await Commit();
        return Success();
    }

    public async Task<List<SanctionType>> GetAll()
     => await dbContext.SanctionTypes.AsNoTracking()
        .ToListAsync();

    public async Task<SanctionType> GetById(int id)
    {
        return (await dbContext.SanctionTypes.FirstOrDefaultAsync(d => d.Id == id))!;
    }

    public async Task<GeneralResponse> Insert(SanctionType item)
    {
        if (await CheckNameExist(item.Name)) return AlreadyAdded();

        dbContext.SanctionTypes.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(SanctionType item)
    {
        var dbItem = await dbContext.SanctionTypes.FirstOrDefaultAsync(d => d.Id == item.Id);
        if (dbItem is null) return NotFound();

        dbItem.Name = item.Name;
        await Commit();
        return Success();
    }

    private async Task Commit() => await dbContext.SaveChangesAsync();

    private async Task<bool> CheckNameExist(string name)
    {
        var result = await dbContext.SanctionTypes.FirstOrDefaultAsync(_ => _.Name.ToLower() == name.ToLower());
        return result is not null;
    }

}
