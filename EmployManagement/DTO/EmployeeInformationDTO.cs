namespace EmployManagement.DTO
{
    public class EmployeeInformationDTO
    {
        public EmployeeInformationDTO(string employeeName, string employeeAddress, string departmentName, string departmentBranch)
        {
            EmployeeName = employeeName;
            EmployeeAddress = employeeAddress;
            DepartmentName = departmentName;
            DepartmentBranch = departmentBranch;
        }

        public string EmployeeName { get; set; }    
        public string EmployeeAddress { get; set; }
        public string DepartmentName { get; set; }  
        public string DepartmentBranch { get; set;}
    }
}
