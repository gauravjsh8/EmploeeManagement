using System.ComponentModel.DataAnnotations;

namespace EmployManagement.View_Model
{
    public class LoginViewModel
    {
        [Required] 
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }    
    }
}
