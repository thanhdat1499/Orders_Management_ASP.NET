using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using DemoIndentityCore.Entities;
using Microsoft.AspNetCore.Identity;

namespace DemoIndentityCore.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the DemoIndentityCoreUser class
    public class DemoIndentityCoreUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Firstname { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
