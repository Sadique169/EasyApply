using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyApply.Core.Domian
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(255)]
        public string FullName { get; set; }
        public string ProfilePicture { get; set; }
    }
}
