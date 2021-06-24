using System.Collections.Generic;
using System.Threading.Tasks;
using API.BAL;
using API.Domain;
using API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/employee")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeBAL _employeeBAL;

        public EmployeeController(IEmployeeBAL employeeBAL)
        {
            _employeeBAL = employeeBAL;
        }

        /// <summary>
        /// To add new a new Employee
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            var employee = await _employeeBAL.AddEmployeeAsync(employeeDTO);

            if (employee != null)
            {
                return CreatedAtRoute(nameof(GetEmployee), new { id = employee.Id }, employee);
            }

            return BadRequest("Unable to add Employee");
        }

        /// <summary>
        /// To get the Employee detail based on Employee ID
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}", Name = nameof(GetEmployee))]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _employeeBAL.GetEmployeeAsync(id);

            if (employee != null)
            {
                return Ok(employee);
            }

            return NotFound("Employee not found");
        }

        /// <summary>
        /// To update the Employee detail based on Employee ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeDTO"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDTO employeeDTO)
        {
            var employee = await _employeeBAL.UpdateEmployeeAsync(id, employeeDTO);

            if (employee != null)
            {
                return CreatedAtRoute(nameof(GetEmployee), new { id = employee.Id }, employee);
            }

            return BadRequest("Unable to update Employee details");
        }

        /// <summary>
        /// To delete the Employee data based on Employee ID
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<bool> DeleteEmployee(int id)
        {
            return await _employeeBAL.DeleteEmployeeAsync(id);
        }

        /// <summary>
        /// To delete the Employee data based on Employee ID
        /// </summary>
        [HttpGet]
        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _employeeBAL.GetAllEmployees();
        }
    }
}