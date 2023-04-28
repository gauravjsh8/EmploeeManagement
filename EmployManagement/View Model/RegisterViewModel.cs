using System.ComponentModel.DataAnnotations;

namespace EmployManagement.View_Model
{
    public class RegisterViewModel
    {
        [Required]  
        public string Email { get; set; }
        [Required]  
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; } 
        public string RoleName { get; set; }    
    }
}
