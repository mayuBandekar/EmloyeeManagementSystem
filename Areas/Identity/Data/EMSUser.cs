using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using EmloyeeManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace EmloyeeManagementSystem.Areas.Identity.Data;

// Add profile data for application users by adding properties to the EMSUser class
public class EMSUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName ="nvarchar(100)")]
    public string FirstName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }


}



