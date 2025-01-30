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

public class SanctionRepository(StaffTrackDb dbContext) : IGenericRepository<Sanction>
{
    public static GeneralResponse Success() => new(true, "Sanction process complete");
    public static GeneralResponse NotFound() => new(false, "Sanction not found");
    public static GeneralResponse AlreadyAdded() => new(false, "Sanction already added");

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var dbItem = await dbContext.Sanctions.FirstOrDefaultAsync(d => d.EmployeeId == id);
        if (dbItem is null) return NotFound();

        dbContext.Sanctions.Remove(dbItem);
        await Commit();
        return Success();
    }

    public async Task<List<Sanction>> GetAll()
     => await dbContext.Sanctions.AsNoTracking()
        .Include(o => o.SanctionTypeId).ToListAsync();

    public async Task<Sanction> GetById(int id)
    {
        return (await dbContext.Sanctions.FirstOrDefaultAsync(d => d.EmployeeId == id))!;
    }

    public async Task<GeneralResponse> Insert(Sanction item)
    {
        dbContext.Sanctions.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Sanction item)
    {
        var dbItem = await dbContext.Sanctions.FirstOrDefaultAsync(d => d.EmployeeId == item.EmployeeId);
        if (dbItem is null) return NotFound();

        dbItem.Date = item.Date;
        dbItem.Punishment = item.Punishment;
        dbItem.PunishmentDate = item.PunishmentDate;
        dbItem.SanctionTypeId = item.SanctionTypeId;
        await Commit();
        return Success();
    }

    private async Task Commit() => await dbContext.SaveChangesAsync();

}
