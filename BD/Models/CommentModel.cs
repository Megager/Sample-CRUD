using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BD.Models
{
    public class CommentModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
    }
}