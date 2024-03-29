﻿using System.ComponentModel.DataAnnotations;

namespace Edukate.ViewModels.AuthVM
{
    public class LoginVM
    {
        public string UsernameOrEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
