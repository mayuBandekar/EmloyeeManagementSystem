using EmloyeeManagementSystem.Areas.Identity.Data;
using EmloyeeManagementSystem.Data;
using EmloyeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;
using EmloyeeManagementSystem.ViewModel;

namespace EmloyeeManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
       
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<EMSUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EMSContext _context;

        public HomeController(ILogger<HomeController> logger,UserManager<EMSUser> userManager, RoleManager<IdentityRole> roleManager, EMSContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var days = Enumerable.Range(1, 31)
                             .Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() });

            ViewBag.Days = days;

            var months = Enumerable.Range(1, 12)
                             .Select(x => new SelectListItem { Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x), Value = x.ToString() });

            ViewBag.Months = months;

            var years = Enumerable.Range(DateTime.Now.Year - 10, 21)
                             .Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() });

            ViewBag.Years = years;



            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Calculations()
        {
            var months = Enumerable.Range(1, 12)
                             .Select(x => new SelectListItem { Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x), Value = x.ToString() });

            ViewBag.Months = months;

            var years = Enumerable.Range(DateTime.Now.Year - 10, 21)
                           .Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() });

            ViewBag.Years = years;

            return View(); 
        }

        public IActionResult ViewTimesheetDetails(string itemId)
        {
            ViewBag.itemId = itemId;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetEmployees()
        {  
            try { 
                var getEmployees = _context.Users.Where(w => w.UserRoleId == 2)
                   .Select(s => new { EmpoyeeId = s.Id, EmployeeName = s.FirstName + ' ' + s.LastName })
                   .ToList();
                return Ok(getEmployees);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        public IActionResult GetUserTimesheetInfo([FromBody] AdminViewModel vm)
        {
            try
            {
                var TimesheetData = _context.TimesheetMaster
                //.Include(t => t.Tasks)
                //.Include(t => t.Breaks)
                .Where(t => t.EmployeeID == vm.UserId &&
                (vm.Day != 0 ? t.TimesheetDate.Day == vm.Day : true) &&
                (vm.Month != 0 ? t.TimesheetDate.Month == vm.Month : true) &&
                (vm.Year != 0 ? t.TimesheetDate.Year == vm.Year : true)
                )
                .Select(t => new
                {
                    //t.Tasks,
                    //t.Breaks,
                    t.TimesheetDate,
                    t.Id
                })
                .ToList();

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                if (TimesheetData != null)
                {
                    return Json(TimesheetData, options);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        public IActionResult GetCalulatedWorkingHours([FromBody] WorkingHours cal)
        {
            try
            {
                decimal WeeklyWorkingHrs = 0;

                if (cal.StartDate != null && cal.EndDate != null)
                {
                    WeeklyWorkingHrs = _context.TimesheetMaster
                   .Where(w => w.TimesheetDate >= cal.StartDate && w.TimesheetDate <= cal.EndDate && w.EmployeeID == cal.UserId)
                   .Sum(s => s.ActualWorkingHours);

                }
               

                var MonthlyWorkingHrs = _context.TimesheetMaster
                    .Where(w => w.TimesheetDate.Month == cal.Month &&
                    w.TimesheetDate.Year == cal.Year &&
                    w.EmployeeID == cal.UserId)
                    .Sum(s => s.ActualWorkingHours);

                object res = new
                {
                    WeeklyWorkingHrs = WeeklyWorkingHrs,
                    MonthlyWorkingHrs = MonthlyWorkingHrs
                };

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                if (WeeklyWorkingHrs != 0 || MonthlyWorkingHrs != 0)
                {
                    return Json(res, options);
                }
                else
                {
                    return Ok(new {res=res, message = "No data found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        public IActionResult GetEmployeeTimesheetDetails([FromQuery] int TimesheetId)
        {
            try
            {
                var TimesheetData = _context.TimesheetMaster
                .Include(t => t.Tasks)
                .Include(t => t.Breaks)
                .Where(t => t.Id == TimesheetId)
                .Select(t => new
                {
                    t.Tasks,
                    t.Breaks
                })
                .ToList();

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                if (TimesheetData != null)
                {
                    return Json(TimesheetData, options);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        public IActionResult GetEmployee()
        {
            var employeeId = _roleManager.Roles.Where(w => w.Name == "EMPLOYEE").Select(s=>s.Id).FirstOrDefault();

            var users = _userManager.Users
                .Join(_context.UserRoles, u=>u.Id, ur => ur.UserId, (u, ur) => new {u,ur})
                .Where(w=>w.ur.RoleId == employeeId)
                .ToList();

            var userData = users.Select(s => new UserDTO
            {
                UserId = s.u.Id,
                FirstName = s.u.FirstName,
                LastName = s.u.LastName
            });


            return Ok(userData);
        }
    }
}
