﻿using System.ComponentModel.DataAnnotations;

namespace Americans_Image_Capture.Server.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email Address is invalid")]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
