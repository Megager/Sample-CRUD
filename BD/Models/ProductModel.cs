using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Entities;
using DAL.Entities.Interfaces;
using DAL.Entities.Mongo;

namespace BD.Models
{
    public class ProductModel<TProduct>
        where TProduct : class, IProduct
    {
        public Product Product { get; set; }
        public TProduct ProductInfo { get; set; }
        public double Mark { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}