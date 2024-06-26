using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<City>> GetAllWithCountryAsync()
        {
            return await _context.Cities.AsNoTracking().Include(m=>m.Country).ToListAsync();
        }

        public async Task<City> GetByIdWithCountryAsync(int id)
        {
            return await _context.Cities.AsNoTracking().Include(m => m.Country).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
