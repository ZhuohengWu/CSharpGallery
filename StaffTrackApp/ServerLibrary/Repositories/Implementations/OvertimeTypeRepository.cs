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

public class OvertimeTypeRepository(StaffTrackDb dbContext) : IGenericRepository<OvertimeType>
{
    public static GeneralResponse Success() => new(true, "Overtime Type process complete");
    public static GeneralResponse NotFound() => new(false, "Overtime Type not found");
    public static GeneralResponse AlreadyAdded() => new(false, "Overtime Type already added");

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var dbItem = await dbContext.OvertimesTypes.FirstOrDefaultAsync(d => d.Id == id);
        if (dbItem is null) return NotFound();

        dbContext.OvertimesTypes.Remove(dbItem);
        await Commit();
        return Success();
    }

    public async Task<List<OvertimeType>> GetAll()
     => await dbContext.OvertimesTypes.AsNoTracking()
        .ToListAsync();

    public async Task<OvertimeType> GetById(int id)
    {
        return (await dbContext.OvertimesTypes.FirstOrDefaultAsync(d => d.Id == id))!;
    }

    public async Task<GeneralResponse> Insert(OvertimeType item)
    {
        if (await CheckNameExist(item.Name)) return AlreadyAdded();

        dbContext.OvertimesTypes.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(OvertimeType item)
    {
        var dbItem = await dbContext.OvertimesTypes.FirstOrDefaultAsync(d => d.Id == item.Id);
        if (dbItem is null) return NotFound();

        dbItem.Name = item.Name;
        await Commit();
        return Success();
    }

    private async Task Commit() => await dbContext.SaveChangesAsync();

    private async Task<bool> CheckNameExist(string name)
    {
        var result = await dbContext.OvertimesTypes.FirstOrDefaultAsync(_ => _.Name.ToLower() == name.ToLower());
        return result is not null;
    }

}
