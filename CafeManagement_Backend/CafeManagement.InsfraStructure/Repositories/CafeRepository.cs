using CafeManagement.Application.DTOs;
using CafeManagement.Domain.Entity;
using CafeManagement.Domain.IRepository;
using CafeManagement.Domain.Models;
using CafeManagement.Insfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace CafeManagement.Insfrastructure.Repositories
{
    public class CafeRepository : ICafeRepository
    {
        private readonly CafeManagementContext dbContext;
        public CafeRepository(CafeManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<CafeViewModel>> GetAllCafesAsync()
        {
            var cafes = await (from c in dbContext.Cafes
                               join ec in dbContext.Employees
                                   on c.Id equals ec.CafeId into employeeGroup
                               select new CafeViewModel
                               {
                                   Name = c.Name,
                                   Description = c.Description,
                                   Logo = c.Logo != null ? Convert.ToBase64String(c.Logo) : null,
                                   Employees = employeeGroup.Count(),
                                   Location = c.Location,
                                   Id = c.Id
                               }).ToListAsync();

            return cafes;
        }

        public async Task<IEnumerable<CafeViewModel>> GetCafesbylocationAsync(string location)
        {
            var cafes = await (from c in dbContext.Cafes
                               where c.Location == location
                               join ec in dbContext.Employees
                                   on c.Id equals ec.CafeId into employeeGroup
                               select new CafeViewModel
                               {
                                   Name = c.Name,
                                   Description = c.Description,
                                   Logo = c.Logo != null ? Convert.ToBase64String(c.Logo) : null,
                                   Employees = employeeGroup.Count(),
                                   Location = c.Location,
                                   Id = c.Id
                               }).ToListAsync();

            return cafes;
        }


        public async Task AddCafeAsync(Cafe cafeDto)
        {
            await dbContext.Cafes.AddAsync(cafeDto);
            await dbContext.SaveChangesAsync();
        }
        public async Task UpdateCafeAsync(Cafe cafeDto)
        {
            var updCafe = await dbContext.Cafes.FirstOrDefaultAsync(x => x.Id == cafeDto.Id); 
            if (updCafe != null) { 
                updCafe.Name = cafeDto.Name;
                updCafe.Description = cafeDto.Description;
                updCafe.Location = cafeDto.Location;
                updCafe.Logo = cafeDto.Logo;
                
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteCafeAsync(Guid cafeId)
        {
            var cafe = await dbContext.Cafes.FirstOrDefaultAsync(x => x.Id == cafeId);

            if (cafe != null)
            {
                dbContext.Cafes.Remove(cafe);

                await dbContext.SaveChangesAsync();
            }
        }

    }
}
