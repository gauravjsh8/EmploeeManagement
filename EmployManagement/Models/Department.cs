namespace EmployManagement.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Branch { get; set; }
        public int TotalEmployee { get; set; }
        // one to many mapping
        public List<Employee> Employees { get; set; }
    }

}
