using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CafeManagement.Domain.IRepository;
using CafeManagement.Application.Cafes.Queries.GetAllCafes;
using MediatR;
using CafeManagement.Application.Cafes.Commands.AddCafe;
using CafeManagement.Application.DTOs;
using CafeManagement.Application.Cafes.Commands.UpdateCafe;
using CafeManagement.Application.Cafes.Commands.DeleteCafe;

namespace CafeManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafeController : ApiControllerBase
    {

        [HttpGet("cafes")]
        public async Task<IActionResult> GetCafes([FromQuery] string? location)
        {
            var cafes = await Mediator.Send(new GetAllCafesQuery() { Location = location});
            return Ok(cafes);
        }

        [HttpPost("cafe")]
        public async Task<IActionResult> AddCafe([FromBody] AddCafeCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetCafes), result);
        }

        [HttpPut("cafe")]
        public async Task<IActionResult> UpdateCafe([FromBody] UpdateCafeCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("cafe/{id}")]
        public async Task<IActionResult> DeleteCafe(Guid id)
        {
            await Mediator.Send(new DeleteCafeCommand() { Id = id });
            return NoContent();
        }

    }
}
