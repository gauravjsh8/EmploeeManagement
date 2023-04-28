using EmployManagement.Models;
using EmployManagement.NewFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployManagement.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly ApplicationDbContext _applicationDbContext;

        public DepartmentController(ILogger<DepartmentController> logger,
        ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddDepartment()
        {
            return View();

        }
        public IActionResult GetAllDepartment()
        {
            List<Department> departments = _applicationDbContext.Department.ToList();
            return View(departments);
        }
        public IActionResult RemoveDepartmentById(Department department)
        {
            _applicationDbContext.Department.Remove(department);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("GetAllDepartment");
        }
        public IActionResult UpdateDepartmentById(Department department)
        {

            _applicationDbContext.Department.Update(department);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("GetAllDepartment");

        }
        public IActionResult GetDepartmentById(int id)
        {
            //can use first or default
            //get department by id and also include the employee list associated with the department
            Department department = _applicationDbContext.Department.Include(x => x.Employees).
                FirstOrDefault(x => x.Id == id);
            return View(department);
        }
        public IActionResult GetDepartment()
        {
            List<Department> departments = _applicationDbContext.Department.ToList();
            return View();
        }

        public IActionResult AddDepartmentDetails(Department department)
        {
            //save the data
            _applicationDbContext.Department.Add(department);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("AddDepartment");

        }

    }
}
