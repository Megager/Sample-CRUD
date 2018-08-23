﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Entities
{
    public class Login : IdentityUser
    {
        public ICollection<View> Views { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
