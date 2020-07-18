using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.ViewModels.System.Users
{
    public class RegisterRequest
    {
        public string FullName { get; set; }

        public int WardId { get; set; }

        public string Gender { get; set; }

        public string IdentityNumber { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
