using Microsoft.AspNetCore.Mvc;
using EmloyeeManagementSystem.Areas.Identity.Data;
using EmloyeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace EmloyeeManagementSystem.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        public IActionResult EmployeeIndex()
        {
            return View();
        }
        public IActionResult EmployeeTimesheet()
        {
            return View();
        }
    }
}
