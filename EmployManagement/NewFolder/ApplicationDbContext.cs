
using EmployManagement.Models;
using EmployManagement.View_Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployManagement.NewFolder
{
    //Used to Communicate with sql database and model class
    public class ApplicationDbContext : IdentityDbContext //added for user register/login
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        //Links a Model class Employee with database table  employee
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }

        public DbSet<EmployeeShift> EmployeeShift { get; set; } // for 1 to 1 mapping
        
        public  DbSet<EmployeeDepartment> EmployeeDepartment { get; set; }
        public DbSet<EmployeeShiftLog> EmployeeShiftLog { get; set; }  

        public DbSet<LateInLateOut> Sp_LateInEarlyOut { get; set; } 

        //Fake Entity to call stored pro edure
        
        // OnModelCreating gets called parallelly when DbContext Get Initialized
        protected override void OnModelCreating(ModelBuilder builder)        {

            base.OnModelCreating(builder);
            builder.Entity<EmployeeDepartment>().HasNoKey();
            builder.Entity<LateInLateOut>().HasNoKey();
             
           
        }
    }
}
