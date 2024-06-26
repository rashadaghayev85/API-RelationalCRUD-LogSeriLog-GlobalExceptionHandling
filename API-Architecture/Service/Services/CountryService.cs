using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Countries;
using Service.Services.Interfaces;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Countries;
using Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Service.Services
{
    public class CountryService:ICountryService
    {
        private readonly ICountryRepository _countryRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<CountryService> _logger;

        public CountryService(IMapper mapper,
                              ICountryRepository countryRepo,
                              ILogger<CountryService> logger)
        {

            _mapper = mapper;
            _countryRepo = countryRepo;
            _logger =logger;
        }
        public async Task CreateAsync(CountryCreateDto model)
        {
            if (model == null) throw new ArgumentNullException();
            await _countryRepo.CreateAsync(_mapper.Map<Country>(model));
           
        }

        public async Task DeleteAsync(int? id)
        {
            if(id is null)
            {
                _logger.LogWarning("Id is null");
                throw new ArgumentNullException();
            }
            var data = await _countryRepo.GetById((int)id);
            await _countryRepo.DeleteAsync(data);
        }

        public async Task EditAsync(int id, CountryEditDto model)
        {
            if (model == null) throw new ArgumentNullException();
            var data = await _countryRepo.GetById(id);

            if (data is null) throw new ArgumentNullException();

            var editData = _mapper.Map(model, data);
             await _countryRepo.EditAsync(editData);
        } 

        public async Task<IEnumerable<CountryDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<CountryDto>>(await _countryRepo.GetAllAsync());
        }

        public async Task<CountryDto> GetByIdAsync(int id)
        {
            return _mapper.Map<CountryDto>(await _countryRepo.GetById(id));
        }
    }
}
