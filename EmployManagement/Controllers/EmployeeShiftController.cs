using EmployManagement.Migrations;
using EmployManagement.Models;
using EmployManagement.NewFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
 

namespace EmployManagement.Controllers
{
    public class EmployeeShiftController : Controller
    {
        private readonly ILogger<EmployeeShiftController> _logger;
        private readonly ApplicationDbContext _applicationDbContext;

        public EmployeeShiftController(ILogger<EmployeeShiftController> logger,
        ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EmployeeShift()
        {
            ViewBag.EmployeeList = _applicationDbContext.Employee.ToList();
            return View();
        }
        public IActionResult GetAllEmployeeShift()
        {
            List<EmployeeShift> employeeShifts = _applicationDbContext.EmployeeShift.ToList();
            employeeShifts =   _applicationDbContext.EmployeeShift
                       .Include(x => x.Employee).ToList();


            return View(employeeShifts);
        }
        public IActionResult GetEmployeeShiftById(int id)
        {
            ViewBag.EmployeeList = _applicationDbContext.Employee.ToList();
            EmployeeShift employeeShift = _applicationDbContext.EmployeeShift.
                Include(x => x.Employee).
                FirstOrDefault(x => x.Id == id);
            return View(employeeShift);
            
        }
        public IActionResult EmployeeShiftDetails(EmployeeShift employeeShift)
        {
            //save the data
            _applicationDbContext.EmployeeShift.Add(employeeShift);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("EmployeeShift");
        }
        public IActionResult UpdateEmployeeShiftById(EmployeeShift employeeShift)
        {
            _applicationDbContext.EmployeeShift.Update(employeeShift);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("GetAllEmployeeShift");

        }
        public IActionResult RemoveEmployeeShiftById(EmployeeShift employeeShift)
        {
            _applicationDbContext.EmployeeShift.Remove(employeeShift);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("GetAllEmployeeShift");

        }

    }
}

