using EmloyeeManagementSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmloyeeManagementSystem.ViewModel
{
    public class BreakViewModel
    {
       
        public string BreakStart { get; set; }
       
        public string BreakEnd { get; set; }
       
        public int TimesheetID { get; set; }

        public string Reason { get; set; }

    }
}
