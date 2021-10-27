using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace async_inn.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        // TODO: think of other things that we'll want to store about a user
        [Column(TypeName = "DATE")]
        public DateTime? DateOfBirth { get; set; }
    }
}
