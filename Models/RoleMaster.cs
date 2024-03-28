using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EmloyeeManagementSystem.Models
{
    public class RoleMaster
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }   
}
