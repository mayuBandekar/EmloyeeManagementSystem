using EmloyeeManagementSystem.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmloyeeManagementSystem.Models
{
    public class Breaks
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan BreakStart { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan BreakEnd { get; set; }

        [Column(TypeName = "int")]
        [ForeignKey("Timesheet")]
        public int TimesheetID { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string Reason { get; set; }

        public Timesheet Timesheet { get; set; }
    }
}
