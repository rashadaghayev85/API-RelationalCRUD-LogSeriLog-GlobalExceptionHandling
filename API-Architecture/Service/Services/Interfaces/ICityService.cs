using Service.DTOs.Admin.Cities;
using Service.DTOs.Admin.Countries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityDto>> GetAllWithCountryAsync();
        Task CreateAsync(CityCreateDto model);
        Task<CityDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);

        Task EditAsync(int id, CityEditDto model);
        Task<CityDto> GetByNameAsync(string name);
    }
}
