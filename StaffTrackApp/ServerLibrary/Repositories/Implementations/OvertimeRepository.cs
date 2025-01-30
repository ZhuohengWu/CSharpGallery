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

public class OvertimeRepository(StaffTrackDb dbContext) : IGenericRepository<Overtime>
{
    public static GeneralResponse Success() => new(true, "Overtime process complete");
    public static GeneralResponse NotFound() => new(false, "Overtime not found");
    public static GeneralResponse AlreadyAdded() => new(false, "Overtime already added");

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var dbItem = await dbContext.Overtimes.FirstOrDefaultAsync(d => d.EmployeeId == id);
        if (dbItem is null) return NotFound();

        dbContext.Overtimes.Remove(dbItem);
        await Commit();
        return Success();
    }

    public async Task<List<Overtime>> GetAll()
     => await dbContext.Overtimes.AsNoTracking()
        .Include(o => o.OvertimeType).ToListAsync();

    public async Task<Overtime> GetById(int id)
    {
        return (await dbContext.Overtimes.FirstOrDefaultAsync(d => d.EmployeeId == id))!;
    }

    public async Task<GeneralResponse> Insert(Overtime item)
    {
        dbContext.Overtimes.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Overtime item)
    {
        var dbItem = await dbContext.Overtimes.FirstOrDefaultAsync(d => d.EmployeeId == item.EmployeeId);
        if (dbItem is null) return NotFound();

        dbItem.StartDate = item.StartDate;
        dbItem.EndDate = item.EndDate;
        dbItem.OvertimeTypeId = item.OvertimeTypeId;
        await Commit();
        return Success();
    }

    private async Task Commit() => await dbContext.SaveChangesAsync();

}
