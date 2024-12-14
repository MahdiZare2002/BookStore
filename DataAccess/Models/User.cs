﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class User : IdentityUser<int>
    {
        public string UserName { get; set; }
        public int Mobile { get; set; }
    }
}
