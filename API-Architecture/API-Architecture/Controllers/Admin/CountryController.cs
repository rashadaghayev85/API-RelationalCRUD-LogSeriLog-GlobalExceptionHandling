using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Countries;
using Service.Services.Interfaces;

namespace API_Architecture.Controllers.Admin
{
    public class CountryController : BaseController
    {
        private readonly ICountryService _countryservice;
        private readonly ILogger<CountryController> _logger;    
        public CountryController(ICountryService countryService, ILogger<CountryController> logger)
        {
            _countryservice = countryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           
                _logger.LogInformation("Country Get All is working");
                return Ok(await _countryservice.GetAll());
           
            
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CountryCreateDto request)
        {
          
                await _countryservice.CreateAsync(request);

                return CreatedAtAction(nameof(Create), new { response = "Data successfully created" });
          
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
           

                return Ok(await _countryservice.GetByIdAsync(id));
            
           
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id == null) return BadRequest();
            await _countryservice.DeleteAsync((int)id);

            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute]int id,[FromBody] CountryEditDto request)
        {
            await _countryservice.EditAsync(id, request);
            return Ok();
        }
    }
}
