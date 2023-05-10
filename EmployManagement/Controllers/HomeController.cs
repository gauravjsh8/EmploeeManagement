using EmployManagement.DTO;
using EmployManagement.Models;
using EmployManagement.NewFolder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics;

namespace EmployManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger,
        ApplicationDbContext applicationDbContext,
        SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var employeeDepartment =_applicationDbContext.EmployeeDepartment.ToList();
            return View();
        }
        public IActionResult AddEmployee()
        {
            ViewBag.DepartmentList = _applicationDbContext.Department.ToList();
            ViewBag.UserList = _applicationDbContext.Users.ToList();
            return View();



        }
        //async method,await is mandatory for async otherewise it acts as a Sync
        public async Task<IActionResult> GetAllEmployee()
        {
            //get current loged in user
            var logedInUser = await _userManager.GetUserAsync(User);
            //get role of loged in user
            var userRole = await _userManager.GetRolesAsync(logedInUser);
            List<Employee> employees = new List<Employee>();
            if (userRole != null)
            {
                //staff can see only their details
                if (userRole.FirstOrDefault() == "Staff")
                {
                    employees = await _applicationDbContext.Employee
                         .Where(x => x.IdentityUserId == logedInUser.Id)
                         .Include(x => x.Department).ToListAsync();
                }
                // admin can see all the employees
                else if (userRole.FirstOrDefault() == "Admin")
                {
                    employees = await _applicationDbContext.Employee
                       .Include(x => x.Department).ToListAsync();
                }
            }

            return View(employees);
        }
        public IActionResult RemoveEmployeeById(Employee employee)
        {
            _applicationDbContext.Employee.Remove(employee);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("GetAllEmployee");
        }
       
        public IActionResult UpdateEmployeeById(Employee employee)
        {
            

            _applicationDbContext.Employee.Update(employee);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("GetAllEmployee");
            
        }


        public IActionResult GetEmployeeById(int id)
        {
            ViewBag.DepartmentList = _applicationDbContext.Department.ToList();
            Employee employee = _applicationDbContext.Employee.
                Include(x => x.Department).
                FirstOrDefault(x => x.Id == id);
            return View(employee);
        }
        public async Task<IActionResult> AddEmployeeDetails(Employee employee)
        {
            //save the data
             await _applicationDbContext.Employee.AddAsync(employee); //async
            await _applicationDbContext.SaveChangesAsync(); //async
            return RedirectToAction("AddEmployee");

        }
        public IActionResult GetEmployeeInformation()
        {
            // get employee and department info using linq query
            //dynamic object
            var employeeInformationTest = from emp in _applicationDbContext.Employee
                                          join dep in _applicationDbContext.Department on
                                          emp.DepartmentId equals dep.Id
                                          select new
                                          {
                                              EmployeeName = emp.Name,
                                              EmployeeAddress = emp.Address,
                                              DepartmentName = dep.Name,
                                              DepartmentBranch = dep.Branch
                                              //DepartmentDescription ="Kalanki"

                                          };
            //object mapped to DTo
            var employeeInformation = from emp in _applicationDbContext.Employee
                                      join dep in _applicationDbContext.Department on
                                      emp.Name equals dep.Name into empDep
                                      from empDetails in empDep.DefaultIfEmpty()
                                      select new EmployeeInformationDTO(emp.Name, emp.Address,
                                      empDetails.Name, empDetails.Branch);
            //right join
            var employeeInformationRight = from dep in _applicationDbContext.Department
                                           join emp in _applicationDbContext.Employee on
                                           dep.Name equals emp.Name into empDep
                                           from empDetails in empDep.DefaultIfEmpty()
                                           select new EmployeeInformationDTO(empDetails.Name, empDetails.Address,
                                           dep.Name, dep.Branch);
            //right added to employee information
            ViewBag.EmployeeInformation = employeeInformationRight;
            ViewBag.CountryName = "Nepal";
            return View();

        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

