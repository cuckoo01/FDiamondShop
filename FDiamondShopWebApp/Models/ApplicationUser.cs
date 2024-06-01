﻿using Microsoft.AspNetCore.Identity;

namespace FDiamondShop.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
