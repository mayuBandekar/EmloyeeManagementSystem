using EmloyeeManagementSystem.Areas.Identity.Data;
using EmloyeeManagementSystem.Data;
using EmloyeeManagementSystem.Models;
using EmloyeeManagementSystem.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmloyeeManagementSystem.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly EMSContext _context;
        public readonly Timesheet AddTimesheet;
        private readonly UserManager<EMSUser> _userManager;
        public TimesheetController(EMSContext context, UserManager<EMSUser> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }       
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FetchData(DateTime selectedDate)
        {
            var userId = _userManager.GetUserId(this.User);

            var isPresent = _context.TimesheetMaster
                .Where(t => t.TimesheetDate.Date == selectedDate.Date && t.EmployeeID == userId)
                .FirstOrDefault();

            if (isPresent == null)
            {
                var addTimesheetdata = new Timesheet
                {
                    TimesheetDate = selectedDate,
                    EmployeeID = userId
                };

                _context.TimesheetMaster.Add(addTimesheetdata);
                _context.SaveChanges();
            }

            var DailyWorkingHrs = _context.TimesheetMaster
                .Where(w => w.TimesheetDate == selectedDate && w.EmployeeID == userId)
                .Select(s => s.ActualWorkingHours).FirstOrDefault();

            var MonthlyWorkingHrs = _context.TimesheetMaster
                .Where(w => w.TimesheetDate.Month == selectedDate.Month
                && w.TimesheetDate.Year == selectedDate.Year && w.EmployeeID == userId)
                .Sum(s => s.ActualWorkingHours);

            var TimesheetData = _context.TimesheetMaster
               .Include(t => t.Tasks)
               .Include(t => t.Breaks)
               .Where(t => t.TimesheetDate.Date == selectedDate.Date && t.EmployeeID == userId)
               .ToList();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            object res = new
            {
                TimesheetData = TimesheetData,
                DailyWorkingHrs = DailyWorkingHrs,
                MonthlyWorkingHrs = MonthlyWorkingHrs
            };
            if ( TimesheetData.Any())
            {

                return Json(res, options);
            }
            else
            {
                return BadRequest();
            }
        }

        //public IActionResult CountTotalHrs([FromBody] int TimesheetId)
        //{

        //    var TimesheetData = _context.TaskMaster
        //        .Where(t => t.TimesheetID == TimesheetId)
        //        .ToList();
        //    float totalHrs = 0.0f;
        //    foreach (var data in TimesheetData)
        //    {
        //        totalHrs += data.Hours;
        //    }

        //    if (totalHrs != null)
        //    {
        //        return Json(TimesheetData);
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        public IActionResult AddTasks([FromBody] Tasks task)
        {
           
            try
            {
                var addTaskData = new Tasks
                {
                    TaskName = task.TaskName,
                    TimesheetID = task.TimesheetID,
                    Hours= task.Hours
                };

                _context.TaskMaster.Add(addTaskData);
                _context.SaveChanges();


                var fetchTimesheetData = _context.TimesheetMaster.FirstOrDefault(w => w.Id == task.TimesheetID);

                if (fetchTimesheetData != null)
                {
                    fetchTimesheetData.ActualWorkingHours += task.Hours;

                    _context.TimesheetMaster.Update(fetchTimesheetData);
                    _context.SaveChanges();
                }
                return Ok(new { message = "Task added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        public IActionResult EditTasks(int id, [FromBody] Tasks task)
        {

            try
            {
                
                var existingTask = _context.TaskMaster.FirstOrDefault(t => t.Id == id);
                var previousWorkingHrs = existingTask.Hours;
                if (existingTask == null)
                {
                    return NotFound(new { error = "Task not found" });
                }

                existingTask.TaskName = task.TaskName;
                existingTask.Hours = task.Hours;

                _context.SaveChanges();

                var fetchTimesheetData = _context.TimesheetMaster.FirstOrDefault(w => w.Id == task.TimesheetID);

                if (fetchTimesheetData != null)
                {
                    fetchTimesheetData.ActualWorkingHours = fetchTimesheetData.ActualWorkingHours - previousWorkingHrs + task.Hours;

                    _context.TimesheetMaster.Update(fetchTimesheetData);
                    _context.SaveChanges();
                }

                return Ok(new { message = "Task updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }


        public IActionResult AddBreaks([FromBody] BreakViewModel breaks)
        {
            
            try
            {
                TimeSpan BreakStart = TimeSpan.Parse(breaks.BreakStart);
                TimeSpan BreakEnd = TimeSpan.Parse(breaks.BreakEnd);

                var addBreakData = new Breaks
                {
                    BreakStart = BreakStart,
                    TimesheetID = breaks.TimesheetID,
                    BreakEnd = BreakEnd,
                    Reason = breaks.Reason
                };
                _context.BreakMaster.Add(addBreakData);
                _context.SaveChanges();
                return Ok(new { message = "Break added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        public IActionResult EditBreaks(int id, [FromBody] BreakViewModel breaks)
        {
            try
            {
                var existingBreak = _context.BreakMaster.FirstOrDefault(t => t.Id == id);
                if (existingBreak == null)
                {
                    return NotFound(new { error = "Break not found" });
                }

                TimeSpan BreakStart = TimeSpan.Parse(breaks.BreakStart);
                TimeSpan BreakEnd = TimeSpan.Parse(breaks.BreakEnd);

                existingBreak.BreakStart = BreakStart;
                existingBreak.BreakEnd = BreakEnd;
                existingBreak.Reason = breaks.Reason;

                _context.SaveChanges();

                return Ok(new { message = "break updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        public IActionResult DeleteT([FromBody] Tasks task)
        {
            return Ok(new { message = "Task deleted successfully" });
        }


        public IActionResult DeleteTask([FromQuery] int id)
        {
            try
            {
                var taskToDelete = _context.TaskMaster.FirstOrDefault(t => t.Id == id);
                var TimesheetId = taskToDelete.TimesheetID;
                decimal ActualWorkingHoursDl = taskToDelete.Hours;

                if (taskToDelete == null)
                {
                    return NotFound(new { message = "Task not found" });
                }

                _context.TaskMaster.Remove(taskToDelete);
                _context.SaveChanges();

                var fetchTimesheetData = _context.TimesheetMaster.FirstOrDefault(w => w.Id == TimesheetId);
                if (fetchTimesheetData != null)
                {
                    fetchTimesheetData.ActualWorkingHours -=  ActualWorkingHoursDl;

                    _context.TimesheetMaster.Update(fetchTimesheetData);
                    _context.SaveChanges();
                }

                return Ok(new { message = "Task deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        public IActionResult DeleteBreak(int id )
        {
            try
            {
                var breakToDelete = _context.BreakMaster.FirstOrDefault(t => t.Id == id);
                if (breakToDelete == null)
                {
                    return NotFound(new { message = "break not found" });
                }

                _context.BreakMaster.Remove(breakToDelete);
                _context.SaveChanges();
                return Ok(new { message = "break deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
    }

   
}
