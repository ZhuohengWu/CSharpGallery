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

public class DoctorRepository(StaffTrackDb dbContext) : IGenericRepository<Doctor>
{
    public static GeneralResponse Success() => new(true, "Doctor process complete");
    public static GeneralResponse NotFound() => new(false, "Doctor not found");
    public static GeneralResponse AlreadyAdded() => new(false, "Doctor already added");

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var dbItem = await dbContext.Doctors.FirstOrDefaultAsync(d => d.EmployeeId == id);
        if (dbItem is null) return NotFound();

        dbContext.Doctors.Remove(dbItem);
        await Commit();
        return Success();
    }

    public async Task<List<Doctor>> GetAll()
     => await dbContext.Doctors.AsNoTracking().ToListAsync();

    public async Task<Doctor> GetById(int id)
    {
        return (await dbContext.Doctors.FirstOrDefaultAsync(d => d.EmployeeId == id))!;
    }

    public async Task<GeneralResponse> Insert(Doctor item)
    {
        dbContext.Doctors.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(Doctor item)
    {
        var dbItem = await dbContext.Doctors.FirstOrDefaultAsync(d => d.EmployeeId == item.EmployeeId);
        if (dbItem is null) return NotFound();

        dbItem.Date = item.Date;
        dbItem.Recommondation = item.Recommondation;
        dbItem.Diagnose = item.Diagnose;
        await Commit();
        return Success();
    }

    private async Task Commit() => await dbContext.SaveChangesAsync();

}
