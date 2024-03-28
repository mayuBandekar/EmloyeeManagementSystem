using EmloyeeManagementSystem.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmloyeeManagementSystem.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string TaskName { get; set; }

        [Column(TypeName = "int")]
        [ForeignKey("Timesheet")]
        public int TimesheetID { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Hours { get; set; }

        public Timesheet Timesheet { get; set; }
    }
}
