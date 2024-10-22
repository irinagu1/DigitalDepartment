using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Auth
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public bool isActive { get; set; }

        public virtual ICollection<UserRole>? UserRoles { get; set; }

        [ForeignKey(nameof(Position))]
        public int? PositionId { get; set; }
        public Position? Position{ get; set; }


    }
}
