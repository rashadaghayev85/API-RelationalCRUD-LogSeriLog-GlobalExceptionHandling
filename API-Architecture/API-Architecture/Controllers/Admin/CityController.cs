using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Cities;
using Service.DTOs.Admin.Countries;
using Service.Services.Interfaces;

namespace API_Architecture.Controllers.Admin
{
   
    public class CityController : BaseController
    {
        private readonly ICityService _cityService;
        private readonly ILogger<CityController> _logger;

        public CityController(ICityService cityService,
                              ILogger<CityController> logger)
        {
            _cityService = cityService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get All method is working");
           
                return Ok(await _cityService.GetAllWithCountryAsync());
           
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CityCreateDto request)
        {
            
                await _cityService.CreateAsync(request);

                return CreatedAtAction(nameof(Create), new { response = "Data successfully created" });
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
                return Ok(await _cityService.GetByIdAsync(id));
         
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id == null) return BadRequest();
            await _cityService.DeleteAsync((int)id);

            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] CityEditDto request)
        {
            await _cityService.EditAsync(id, request);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            return Ok(await _cityService.GetByNameAsync(name));    
        }


    }
}
