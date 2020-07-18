using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Entity
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Avatar { get; set; }
        public string FullName { get; set; }

        public int? WardId { get; set; }
        public string Gender { get; set; }
        public string IdentityNumber { get; set; }

        public Ward Ward { get; set; }

        public List<Property> Properties { get; set; }
        public List<Favorite> Favorites { get; set; }
    }
}
