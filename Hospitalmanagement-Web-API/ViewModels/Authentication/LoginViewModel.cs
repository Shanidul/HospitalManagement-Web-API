﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospitalmanagement_Web_API.ViewModels.Authentication
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is mandatory")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is mandatory")]
        public string Password { get; set; }
    }
}
