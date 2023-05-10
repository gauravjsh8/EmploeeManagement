namespace EmployManagement.Models
{
    public class EmployeeShiftLog
    {
        public int Id { get; set; } 
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set;}
        public int EmployeeId { get; set; } 
        public int EmployeeShiftId { get; set; }    
        public Employee Employee { get; set; }  
        public EmployeeShift EmployeeShift { get; set; }    
    }
}
