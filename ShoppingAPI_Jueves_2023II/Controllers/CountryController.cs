using Microsoft.AspNetCore.Mvc;
using ShoppingAPI_Jueves_2023II.DAL.Entities;
using ShoppingAPI_Jueves_2023II.Domain.Interfaces;

namespace ShoppingAPI_Jueves_2023II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet, ActionName("Get")]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountriesAsync()
        {
            var countries = await _countryService.GetCountriesAsync();

            if (countries == null || !countries.Any()) return NotFound();

            return Ok(countries);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateCountryAsync(Country country)
        {
            try
            {
                var createdCountry = await _countryService.CreateCountryAsync(country);
                return Ok(createdCountry);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", country.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")] //URL: api/countries/get
        public async Task<ActionResult<Country>> GetCountryByIdAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var country = await _countryService.GetCountryByIdAsync(id);

            if (country == null) return NotFound(); // 404

            return Ok(country); // 200
        }

        [HttpGet, ActionName("Get")]
        [Route("GetByName/{name}")] //URL: api/countries/get
        public async Task<ActionResult<Country>> GetCountryByNameAsync(string name)
        {
            if (name == null) return BadRequest("Nombre del país requerido!");

            var country = await _countryService.GetCountryByNameAsync(name);

            if (country == null) return NotFound(); // 404

            return Ok(country); // 200
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult<Country>> EditCountryAsync(Country country)
        {
            try
            {
                var editedCountry = await _countryService.EditCountryAsync(country);
                return Ok(editedCountry);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", country.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<Country>> DeleteCountryAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var deletedCountry = await _countryService.DeleteCountryAsync(id);

            if (deletedCountry == null) return NotFound("País no encontrado!");

            return Ok(deletedCountry);
        }
    }
}