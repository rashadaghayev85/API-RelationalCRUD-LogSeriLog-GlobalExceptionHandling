using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Service.DTOs.Admin.Countries;
using Service.Helpers;
using Service.Services.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Service.DTOs.Admin.Countries.CountryCreateDto;
using FluentValidation.AspNetCore;
using Repository.Repositories.Interfaces;
using Repository.Repositories;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddFluentValidationAutoValidation(config =>
            {
                config.DisableDataAnnotationsValidation = true;
            });

            services.AddScoped<IValidator<CountryCreateDto>, CountryCreateDtoValidator>();

            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();




            return services;

        }
    }
}
