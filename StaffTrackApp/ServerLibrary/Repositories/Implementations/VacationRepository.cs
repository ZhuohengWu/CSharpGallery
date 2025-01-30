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

public class VacationRepository(StaffTrackDb dbContext) : IGenericRepository<Vacation>
{
    public static GeneralResponse Success() => new(true, "Vacation process complete");
    public static GeneralResponse NotFound() => new(false, "Vacation not found");
    public static GeneralResponse AlreadyAdded() => new(false, "Vacation already added");

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var dbItem = await dbContext.Vacations.FirstOrDefaultAsync(d => d.EmployeeId == id);
        if (dbItem is null) return NotFound();

        dbContext.Vacations.Remove(dbItem);
        await Commit();
        return Success();
    }

    public async Task<List<Vacation>> GetAll()
     => await dbContext.Vacations.AsNoTracking()
        .Include(o => o.VacationType).ToListAsync();

    public async Task<Vacation> GetById(int id)
    {
        return (await dbContext.Vacations.FirstOrDefaultAsync(d => d.EmployeeId == id))!;
    }

    public async Task<GeneralResponse> Insert(Vacation item)
    {
        dbContext.Vacations.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Vacation item)
    {
        var dbItem = await dbContext.Vacations.FirstOrDefaultAsync(d => d.EmployeeId == item.EmployeeId);
        if (dbItem is null) return NotFound();

        dbItem.StartDate = item.StartDate;
        dbItem.NumOfDays = item.NumOfDays;
        dbItem.VacationTypeId = item.VacationTypeId;
        await Commit();
        return Success();
    }

    private async Task Commit() => await dbContext.SaveChangesAsync();

}
