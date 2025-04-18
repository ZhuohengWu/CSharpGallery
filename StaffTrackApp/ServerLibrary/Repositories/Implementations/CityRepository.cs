﻿using BaseLibrary.Entities;
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

public class CityRepository(StaffTrackDb dbContext) : IGenericRepository<City>
{
    public static GeneralResponse Success() => new(true, "City process complete");
    public static GeneralResponse NotFound() => new(false, "City not found");
    public static GeneralResponse AlreadyAdded() => new(false, "City already added");

    public async Task<GeneralResponse> DeleteById(int id)
    {
        var dbItem = await dbContext.Citys.FindAsync(id);
        if (dbItem is null) return NotFound();

        dbContext.Citys.Remove(dbItem);
        await Commit();
        return Success();
    }

    public async Task<List<City>> GetAll()
    => await dbContext.Citys
        .AsNoTracking().Include(c => c.Country)
        .ToListAsync();

    public async Task<City> GetById(int id)
    => await dbContext.Citys.FindAsync(id) ?? new();

    public async Task<GeneralResponse> Insert(City item)
    {
        if (await CheckNameExist(item.Name)) return AlreadyAdded();

        dbContext.Citys.Add(item);
        await Commit();
        return Success();
    }

    public async Task<GeneralResponse> Update(City item)
    {
        var dbItem = await dbContext.Citys.FindAsync(item.Id);
        if (dbItem is null) return NotFound();

        dbItem.Name = item.Name;
        dbItem.CountryId = item.CountryId;
        await Commit();
        return Success();
    }

    private async Task Commit() => await dbContext.SaveChangesAsync();
    private async Task<bool> CheckNameExist(string name)
    {
        var result = await dbContext.Citys.FirstOrDefaultAsync(_ => _.Name.ToLower() == name.ToLower());
        return result is not null;
    }
}
