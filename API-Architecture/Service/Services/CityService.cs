using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Cities;
using Service.DTOs.Admin.Countries;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepo;
        private readonly ICountryRepository _countryRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<CityService> _logger;

        public CityService(IMapper mapper, 
                           ICityRepository cityRepo,
                           ICountryRepository countryRepo,
                           ILogger<CityService>logger)

        {

            _mapper = mapper;
            _cityRepo = cityRepo;
            _countryRepo = countryRepo;
            _logger = logger;
        }

        public async Task CreateAsync(CityCreateDto model)
        {
            var existCountry= await _countryRepo.GetById(model.CountryId);
            if (existCountry is null)
            {
                _logger.LogWarning($"Country is not found -{model.CountryId+"-"+DateTime.Now.ToString()}");
                throw new NotFoundException($"id-{model.CountryId} Country not found");
            }
                if (model == null) throw new ArgumentNullException();
            await _cityRepo.CreateAsync(_mapper.Map<City>(model));
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _cityRepo.GetById(id);
            await _cityRepo.DeleteAsync(data);
        }

        public async Task EditAsync(int id, CityEditDto model)
        {
            if (model == null) throw new ArgumentNullException();
            var data = await _cityRepo.GetByIdWithCountryAsync(id);

            if (data is null) throw new ArgumentNullException();

            var editData = _mapper.Map(model, data);
            await _cityRepo.EditAsync(editData);
        }

       

        public async Task<IEnumerable<CityDto>> GetAllWithCountryAsync()
        {
            return _mapper.Map<IEnumerable<CityDto>>(await _cityRepo.GetAllWithCountryAsync());
        }

        public async Task<CityDto> GetByIdAsync(int id)
        {
            return _mapper.Map<CityDto>(await _cityRepo.GetByIdWithCountryAsync(id));
        }

        public async Task<CityDto> GetByNameAsync(string name)
        {
            var data = _cityRepo.FindBy(m => m.Name == name, m => m.Country);
            return _mapper.Map<CityDto>(data.FirstOrDefault());
        }
    }
}
