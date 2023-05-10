using System.ComponentModel.DataAnnotations;

namespace EmployManagement.Models
{
    public class EmployeeShift
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public decimal TotalDuration { get; set; }

        public Employee Employee { get; set; }
        public List<EmployeeShiftLog> EmployeeShiftLogs { get; set; }

    }
}

    
