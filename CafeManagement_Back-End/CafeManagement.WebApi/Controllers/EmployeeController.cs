using CafeManagement.Application.Cafes.Commands.DeleteCafe;
using CafeManagement.Application.Cafes.Queries.GetAllCafes;
using CafeManagement.Application.Employees.Commands.AddEmployee;
using CafeManagement.Application.Employees.Commands.DeleteEmployee;
using CafeManagement.Application.Employees.Commands.UpdateEmployee;
using CafeManagement.Application.Employees.Queries.GetAllEmployees;
using CafeManagement.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ApiControllerBase
    {

        [HttpGet("employees")]
        public async Task<IActionResult> GetEmployees([FromQuery] string? cafe = null)
        {
            var employees = await Mediator.Send(new GetAllEmployeesQuery() { CafeId = cafe});
            return Ok(employees);
        }

        [HttpPost("employee")]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployees), result);
        }

        [HttpPut("employee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("employee/{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            await Mediator.Send(new DeleteEmployeeCommand() { Id = id});
            return NoContent(); ;
        }
    }
}
