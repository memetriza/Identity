﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Identity.Infrastructure;

namespace Identity.ViewModels
{
    public class UserLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}