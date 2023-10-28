using ShoppingAPI_Jueves_2023II.DAL.Entities;
using ShoppingAPI_Jueves_2023II.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingAPI_Jueves_2023II.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatesController : Controller
    {
        private readonly IStateService _stateService;
        public StatesController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<State>>> GetStatesByCountryIdAsync(Guid countryId)
        {
            var states = await _stateService.GetStatesByCountryIdAsync(countryId);
            if (states == null || !states.Any()) return NotFound();

            return Ok(states);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateStateAsync(State state, Guid countryId)
        {
            try
            {
                var createdState = await _stateService.CreateStateAsync(state, countryId);

                if (createdState == null) return NotFound();

                return Ok(createdState);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(String.Format("El estado/dpto {0} ya existe.", state.Name));
                }

                return Conflict(ex.Message);
            }
        }

        [HttpGet, ActionName("Get")]
        [Route("GetById/{id}")]
        public async Task<ActionResult<State>> GetStateByIdAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var state = await _stateService.GetStateByIdAsync(id);

            if (state == null) return NotFound();

            return Ok(state);
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult<State>> EditStateAsync(State state, Guid id)
        {
            try
            {
                var editedState = await _stateService.EditStateAsync(state, id);
                return Ok(editedState);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", state.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult<State>> DeleteStateAsync(Guid id)
        {
            if (id == null) return BadRequest("Id es requerido!");

            var deletedState = await _stateService.DeleteStateAsync(id);

            if (deletedState == null) return NotFound("País no encontrado!");

            return Ok("State Deleted"); //in Ok() method you can send a message in swagger instead send the object
        }
    }
}
