using EmloyeeManagementSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmloyeeManagementSystem.Models
{
    public class UserMaster
    {
        [Key]
        public int Id { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        [ForeignKey("UserId")]
        public string IdentityUserId { get; set; }

        [ForeignKey("Id")]
        public int UserRoleId { get; set; }


        public EMSUser EMSUser { get; set; }
        public RoleMaster RoleMaster { get; set; }


    }
}
