using System.Threading.Tasks;
using API.BAL;
using API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeBAL _employeeBAL;

        public EmployeeController(IEmployeeBAL employeeBAL)
        {
            _employeeBAL = employeeBAL;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody]EmployeeDTO employeeDTO)
        {
            var employee = await _employeeBAL.AddEmployeeAsync(employeeDTO);

            if (employee != null)
            {
                return CreatedAtRoute(nameof(GetEmployee), new { id = employee.Id}, employee);
            }

            return BadRequest();
        }

        [HttpGet("{id}", Name = nameof(GetEmployee))]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _employeeBAL.GetEmployeeAsync(id);

            if (employee != null)
            {
                return Ok(employee);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody]EmployeeDTO employeeDTO)
        {
            var employee = await _employeeBAL.UpdateEmployeeAsync(id, employeeDTO);

            if (employee != null)
            {
                return CreatedAtRoute(nameof(GetEmployee), new { id = employee.Id}, employee);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteEmployee(int id)
        {
            return await _employeeBAL.DeleteEmployeeAsync(id);
        }
    }
}