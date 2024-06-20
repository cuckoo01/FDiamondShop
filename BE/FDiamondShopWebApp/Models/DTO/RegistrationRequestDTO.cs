﻿using System.ComponentModel.DataAnnotations;

namespace FDiamondShop.API.Models.DTO
{
    public class RegistrationRequestDTO
    {      
        [Required]
        [RegularExpression(pattern: @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",ErrorMessage ="Email is not valid !")]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(pattern: @"^[a-zA-Z\s]*$", ErrorMessage = "Only characters")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(pattern: @"^[a-zA-Z\s]*$", ErrorMessage = "Only characters")]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [RegularExpression(pattern: @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Password must be at least 6 characters  and contain at least 1 uppercase letter, 1 number, and 1 special character.")]
        public string Password { get; set; }
        public string Role { get; set; }
        [Required]
        [RegularExpression(pattern: @"^0\d{9}$", ErrorMessage = "Phone number must contain 10 digits and start with 0")]
        public string PhoneNumber { get; set; }
    }
}
