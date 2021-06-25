using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class EmployeeDTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "First name should not exceed 100 characters")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Last name should not exceed 100 characters")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Designation should not exceed 100 characters")]
        public string Designation { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Department should not exceed 100 characters")]
        public string Department { get; set; }
    }
}