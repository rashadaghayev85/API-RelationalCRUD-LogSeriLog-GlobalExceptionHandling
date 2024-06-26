using AutoMapper;
using Domain.Entities;
using Service.DTOs.Admin.Cities;
using Service.DTOs.Admin.Countries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<CountryCreateDto, Country>();
            CreateMap<CountryEditDto, Country>();


            CreateMap<City, CityDto>().ForMember(dest=>dest.CountryName,opt=>opt.MapFrom(src=>src.Country.Name));
            CreateMap<CityCreateDto, City>();
            CreateMap<CityEditDto, City>();
        }
       
    }
}
