using EmployManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployManagement.NewFolder
{
    //Used to Communicate with sql database and model class
    public class ApplicationDbContext : IdentityDbContext //added for user regisrer/login
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        //Links a Model class Employee with database table  employee
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
        
    }
}
