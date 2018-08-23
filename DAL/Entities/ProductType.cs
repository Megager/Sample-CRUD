﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ProductType : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
