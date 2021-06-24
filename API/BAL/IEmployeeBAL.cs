using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain;
using API.DTO;

namespace API.BAL
{
    public interface IEmployeeBAL
    {
        Task<Employee> AddEmployeeAsync(EmployeeDTO employeeDto);
        Task<EmployeeDTO> GetEmployeeAsync(int id);
        Task<Employee> UpdateEmployeeAsync(int id, EmployeeDTO employeeDTO);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<List<Employee>> GetAllEmployees();
    }
}