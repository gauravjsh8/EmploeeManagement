using Microsoft.AspNetCore.Identity;

namespace EmployManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; } 
       
        public string Name { get; set; }
        public string Address { get; set; } 
        public double PhoneNumber { get; set; }   
        public DateTime Birthdate { get; set; } 
        public string Email { get; set; }   
        public double Salary { get; set; }  
        // Many to One mappingID
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public string IdentityUserId { get; set; }  
       
        public IdentityUser IdentityUser { get; set; }  
        public List<EmployeeShiftLog> EmployeeShiftLogs { get; set; }   
    }
}
