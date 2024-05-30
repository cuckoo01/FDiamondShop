﻿using System.ComponentModel.DataAnnotations;

namespace FDiamondShop.API.Models.DTO
{
    public class RegistrationRequestDTO
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } =null!;
        [Required]
        public string ConfirmPassWord { get; set; } =null!;
    }
}
