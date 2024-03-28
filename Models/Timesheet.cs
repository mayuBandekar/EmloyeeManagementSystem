using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using EmloyeeManagementSystem.Areas.Identity.Data;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace EmloyeeManagementSystem.Models
{
    public class Timesheet
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime TimesheetDate { get; set; }

        [Column(TypeName = "varchar(200)")]
        [ForeignKey("Id")]
        public string EmployeeID { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal ActualWorkingHours { get; set; }

        public EMSUser EMSUser { get; set; }

        public ICollection<Tasks> Tasks { get; set; }

        public ICollection<Breaks> Breaks { get; set; }
    }
}
