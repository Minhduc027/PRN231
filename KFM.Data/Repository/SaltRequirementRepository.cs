﻿using KFM.Data.Base;
using KFM.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFM.Data.Repository;

public class SaltRequirementRepository: GenericRepository<SaltRequirement>
{
    public SaltRequirementRepository()
    {
    }
    public SaltRequirementRepository(FA24_SE1720_PRN231_G4_KFMContext context) => _context = context;

    public async Task<SaltRequirement?> GetByIdAsNotracking(int id)
    {
        var saltRequirement = await _context.SaltRequirements.Include(s => s.Pond).AsNoTracking().FirstOrDefaultAsync(p => p.SaltId == id);
        return saltRequirement != null ? saltRequirement : null;
    }
    public async Task<List<SaltRequirement>?> GetAllSalt(int pageNo, int pageSize)
    {
        var saltRequirement = await _context.SaltRequirements.Include(s => s.Pond).AsNoTracking()
            .Skip(pageNo - 1).Take(pageSize)
            .ToListAsync();
        return saltRequirement != null ? saltRequirement : null;
    }
    public async Task<List<SaltRequirement>> searchSalt(double? RequiredSaltAmount, int pageNo, int pageSize)
    {
        return await _context.SaltRequirements.Include(s => s.Pond)
            .Where(s => s.RequiredSaltAmount >= RequiredSaltAmount)
            .Skip(pageNo - 1).Take(pageSize).ToListAsync();
    }
    public async Task<int> totalRecord()
    {
        return await _context.SaltRequirements.CountAsync();
    }
}
