using EmloyeeManagementSystem.Areas.Identity.Data;
using EmloyeeManagementSystem.Data;
using EmloyeeManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmloyeeManagementSystem.Controllers
{
    [Route("Task/[controller]")]
    public class TaskController : Controller
    {

        private readonly Tasks addTask;
        private readonly EMSContext _context;
        private readonly UserManager<EMSUser> _userManager;

        public TaskController(EMSContext context, UserManager<EMSUser> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }


      
       

    }
}
