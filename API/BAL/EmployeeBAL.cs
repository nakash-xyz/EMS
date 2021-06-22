using System.Threading.Tasks;
using API.DAL;
using API.Domain;
using API.DTO;

namespace API.BAL
{
    public class EmployeeBAL : IEmployeeBAL
    {
        private readonly DataContext _context;

        public EmployeeBAL(DataContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployeeAsync(EmployeeDTO employeeDto)
        {
            var employee = new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Department = employeeDto.Department,
                Designation = employeeDto.Designation
            };

            _context.Employees.Add(employee);
            if (await _context.SaveChangesAsync() > 0)
            {
                return employee;
            }

            return null;
        }

        public async Task<EmployeeDTO> GetEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee != null)
            {
                return new EmployeeDTO
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Department = employee.Department,
                    Designation = employee.Designation,
                };
            }

            return null;
        }

        public async Task<Employee> UpdateEmployeeAsync(int id, EmployeeDTO employeeDTO)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee != null)
            {
                employee.FirstName = employeeDTO.FirstName;
                employee.LastName = employeeDTO.LastName;
                employee.Department = employeeDTO.Department;
                employee.Designation = employeeDTO.Designation;

                _context.Employees.Update(employee);
                return await _context.SaveChangesAsync() > 0 ? employee : null;
            }

            return null;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}