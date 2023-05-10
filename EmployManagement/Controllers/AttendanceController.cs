using EmployManagement.Models;
using EmployManagement.NewFolder;
using EmployManagement.View_Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace EmployManagement.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AttendanceController(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Attendance()
        {
            //get logged in user
            var logedInUser = await _userManager.GetUserAsync(User);
            ViewBag.CheckInCheck = "Check In";
            ViewBag.AttendanceId = null;
            ViewBag.EmployeeShiftLogs = new List<EmployeeShiftLogViewModel>();
            if (logedInUser != null)
            {
                //get employee from loged in user id
                var EmployeeDetails = _applicationDbContext.Employee.Where(x => x.IdentityUserId == logedInUser.Id).FirstOrDefault();
                if (EmployeeDetails != null)
                {
                    //added for getting all the checkin details
                    var employeeShiftId = _applicationDbContext.EmployeeShift
                        .Where(x => x.EmployeeId == EmployeeDetails.Id).Select(x => x.Id).FirstOrDefault();
                    if (employeeShiftId != null)
                    {
                        ViewBag.EmployeeShiftLogs = _applicationDbContext.EmployeeShiftLog.
                            Where(x => x.EmployeeId == EmployeeDetails.Id && x.EmployeeShiftId == employeeShiftId)
                            .Select(x => new EmployeeShiftLogViewModel
                            {
                                EmployeeName = EmployeeDetails.Name,
                                CheckInTime = x.CheckInTime.HasValue ? x.CheckInTime.Value.ToString("hh:mm tt") : null,
                                CheckOutTime = x.CheckOutTime.HasValue ? x.CheckOutTime.Value.ToString("hh:mm tt") : null,

                            })
                            .ToList();
                    }
                    // take last shift data
                    var employeeShiftLog = _applicationDbContext.EmployeeShiftLog
                           .Where(es => es.EmployeeId == EmployeeDetails.Id && es.CheckInTime.Value.Date == DateTime.Today.Date)
                           .OrderByDescending(es => es.Id).FirstOrDefault();
                    if (employeeShiftLog != null)
                    {
                        if (employeeShiftLog.CheckInTime != null &&
                            employeeShiftLog.CheckOutTime == null)
                        {
                            ViewBag.CheckInCheck = "Check Out";
                            ViewBag.AttendanceId = employeeShiftLog.Id;
                        }
                    }
                }
            }
            return View();
        }
        public async Task<IActionResult> LateInLateOut()
        {
            var logedInUser = await _userManager.GetUserAsync(User);
           
            ViewBag.EmployeeShiftLogs = new List<LateInLateOut>();
            if (logedInUser != null)
            {
                var EmployeeDetails = _applicationDbContext.Employee
                .Where(x => x.IdentityUserId == logedInUser.Id).FirstOrDefault();
                var employeeShift = _applicationDbContext.EmployeeShift
                          .Where(x => x.EmployeeId == EmployeeDetails.Id).FirstOrDefault();
                if (employeeShift.Id != null)
                {
                    ViewBag.EmployeeShiftLogs = _applicationDbContext.EmployeeShiftLog.
                        Where(x => x.EmployeeId == EmployeeDetails.Id && x.EmployeeShiftId == employeeShift.Id)
                        .Select(x => new LateInLateOut
                        {
                            EmployeeName = EmployeeDetails.Name,
                            CheckInTime = x.CheckInTime.HasValue ? x.CheckInTime.Value.ToString("hh:mm tt") : null,
                            CheckOutTime = x.CheckOutTime.HasValue ? x.CheckOutTime.Value.ToString("hh:mm tt") : null,
                            AttendanceDate = x.CheckInTime.HasValue ? x.CheckInTime.Value.ToString("MM/dd/yyyy") : null,
                            Shift = employeeShift.CheckInTime.HasValue && employeeShift.CheckOutTime.HasValue ?
                            employeeShift.CheckInTime.Value.ToString("hh:mm tt") + "-" + employeeShift.CheckOutTime.Value.ToString("hh:mm tt") : null,
                            Status = x.CheckInTime.HasValue && x.CheckOutTime.HasValue?( x.CheckInTime.Value > employeeShift.CheckInTime.Value.AddMinutes(15)
                            && x.CheckOutTime.Value < employeeShift.CheckOutTime.Value.AddMinutes(-15) ?
                            "Late In Early Out" : x.CheckInTime.Value > employeeShift.CheckInTime.Value.AddMinutes(15)? "Late In":
                            x.CheckOutTime.Value < employeeShift.CheckOutTime.Value.AddMinutes(-15)? "Early Out":"On Time") :"No Attendance"
                        })
                        .ToList();
                }
            }
            return View();
        }

        public async Task<IActionResult> CheckInEmployee(int? attendanceId)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser != null)
            {
                //get employee from logged in User Id
                var employeeDetails = _applicationDbContext.Employee
                    .Where(emp => emp.IdentityUserId == loggedInUser.Id).FirstOrDefault();
                if (employeeDetails != null)
                {
                    var employeeShiftId = _applicationDbContext.EmployeeShift
                        .Where(x => x.EmployeeId == employeeDetails.Id).Select(x => x.Id).FirstOrDefault();
                    if (attendanceId == null)
                    {
                        EmployeeShiftLog employeeShiftLog = new EmployeeShiftLog();
                        employeeShiftLog.CheckInTime = DateTime.Now;
                        employeeShiftLog.EmployeeId = employeeDetails.Id;
                        employeeShiftLog.EmployeeShiftId = employeeShiftId;
                        _applicationDbContext.EmployeeShiftLog.Add(employeeShiftLog);
                        _applicationDbContext.SaveChanges();

                    }
                    else
                    {
                        EmployeeShiftLog? employeeShiftLog = _applicationDbContext.EmployeeShiftLog
                        .Where(es => es.Id == attendanceId).FirstOrDefault();
                        if (employeeShiftLog != null)
                        {
                            employeeShiftLog.CheckOutTime = DateTime.Now;
                            _applicationDbContext.EmployeeShiftLog.Update(employeeShiftLog);
                            _applicationDbContext.SaveChanges();
                        }
                    }

                }
            }
            return RedirectToAction("Attendance");

        }
    }
}
  

