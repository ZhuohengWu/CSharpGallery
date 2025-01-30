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

public class VacationTypeRepository(StaffTrackDb dbContext) : IGenericRepository<VacationType>
{
    public static GeneralResponse Success() => new(true, "Vacation Type process complete");
    public static GeneralResponse NotFound() => new(false, "Vacation Type not found");
    public static GeneralResponse AlreadyAdded() => new(false, "Vacation Type already added");

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var dbItem = await dbContext.Vacations.FirstOrDefaultAsync(d => d.Id == id);
        if (dbItem is null) return NotFound();

        dbContext.Vacations.Remove(dbItem);
        await Commit();
        return Success();
    }

    public async Task<List<VacationType>> GetAll()
     => await dbContext.VacationTypes.AsNoTracking()
        .ToListAsync();

    public async Task<VacationType> GetById(int id)
    {
        return (await dbContext.VacationTypes.FirstOrDefaultAsync(d => d.Id == id))!;
    }

    public async Task<GeneralResponse> Insert(VacationType item)
    {
        if (await CheckNameExist(item.Name)) return AlreadyAdded();

        dbContext.VacationTypes.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(VacationType item)
    {
        var dbItem = await dbContext.VacationTypes.FirstOrDefaultAsync(d => d.Id == item.Id);
        if (dbItem is null) return NotFound();

        dbItem.Name = item.Name;
        await Commit();
        return Success();
    }

    private async Task Commit() => await dbContext.SaveChangesAsync();

    private async Task<bool> CheckNameExist(string name)
    {
        var result = await dbContext.VacationTypes.FirstOrDefaultAsync(_ => _.Name.ToLower() == name.ToLower());
        return result is not null;
    }

}
